﻿using Shared.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shared.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<TEntity> Where<TEntity, TField>(
            this IQueryable<TEntity> entities, string field, string methodName, params object[] args)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity));
            var member = param.BuildMemberExpression(field);
            var propertyType = member.Type;
            if (propertyType != typeof(TField))
                throw new ArgumentException(
                    $"Type of {field} is {propertyType.FullName} does not match TField," +
                    $" TField is {typeof(TField).FullName}");
            Type[] types = args.Select(arg => arg.GetType()).ToArray();
            MethodInfo method = typeof(TField).GetMethod(methodName, types);
            if (method == null)
                throw new ArgumentException(
                    $"Method {methodName} does not found in class {typeof(TField).FullName}" +
                    $" with arguments types [{string.Join(",", types.Select(type => type.FullName))}]"
                );
            ConstantExpression[] constants = args.Select(arg => Expression.Constant(arg)).ToArray();
            Expression call = Expression.Call(member, method, constants);
            var whereClause = Expression.Lambda<Func<TEntity, bool>>(call, param);
            return entities.Where(whereClause);
        }

        public static IQueryable<TEntity> Where<TEntity>(
            this IQueryable<TEntity> entities, string field, Operator @operator, object arg, Type type)
        {
            ParameterExpression param = Expression.Parameter(typeof(TEntity));
            MemberExpression member = param.BuildMemberExpression(field);
            var propertyType = member.Type;
            if (propertyType != type)
                throw new ArgumentException(
                    $"Type of {field} is {propertyType.FullName} does not match provided type," +
                    $" provided type is {type.FullName}");
            if (arg.GetType() != type)
                throw new ArgumentException(
                    $"Type of arg is {arg.GetType().FullName} does not match provided type," +
                    $" arg type is {type.FullName}");
            ConstantExpression constant = Expression.Constant(arg);
            Expression equalExpression = @operator switch
            {
                Operator.Equal => Expression.Equal(member, constant),
                Operator.LessThan => Expression.LessThan(member, constant),
                Operator.GreaterThan => Expression.GreaterThan(member, constant),
                Operator.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
                Operator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
                Operator.NotEqual => Expression.NotEqual(member, constant),
                _ => throw new NotImplementedException()
            };
            var whereClause = Expression.Lambda<Func<TEntity, bool>>(equalExpression, param);
            return entities.Where(whereClause);
        }

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(
            this IQueryable<TEntity> entities, string field, OrderByDirection direction = OrderByDirection.Ascending)
        {
            if (direction == OrderByDirection.Unspecified)
                throw new ArgumentException(field, new NotSupportedException());
            ParameterExpression param = Expression.Parameter(typeof(TEntity));
            MemberExpression member = param.BuildMemberExpression(field);
            var command = direction switch
            {
                OrderByDirection.Ascending => "OrderBy",
                OrderByDirection.Descending => "OrderByDescending",
                _ => throw new NotImplementedException()
            };
            var lambda = Expression.Lambda(member, param);
            var expression = Expression.Call(
                    typeof(Queryable), command,
                    new Type[] { typeof(TEntity), member.Type },
                    entities.Expression, Expression.Quote(lambda));
            return (IOrderedQueryable<TEntity>)entities.Provider.CreateQuery<TEntity>(expression);
        }

        public static IOrderedQueryable<TEntity> ThenBy<TEntity>(
            this IOrderedQueryable<TEntity> entities, string field, OrderByDirection direction = OrderByDirection.Ascending)
        {
            if (direction == OrderByDirection.Unspecified)
                throw new ArgumentException(field, new NotSupportedException());
            ParameterExpression param = Expression.Parameter(typeof(TEntity));
            MemberExpression member = param.BuildMemberExpression(field);
            var command = direction switch
            {
                OrderByDirection.Ascending => "ThenBy",
                OrderByDirection.Descending => "ThenByDescending",
                _ => throw new NotImplementedException()
            };
            var lambda = Expression.Lambda(member, param);
            var expression = Expression.Call(
                    typeof(Queryable), command,
                    new Type[] { typeof(TEntity), member.Type },
                    entities.Expression, Expression.Quote(lambda));
            return (IOrderedQueryable<TEntity>)entities.Provider.CreateQuery<TEntity>(expression);
        }

        private static MemberExpression BuildMemberExpression(this Expression expression, string field)
        {
            if (!IsValidField(field))
                throw new ArgumentException($"\"{field}\" is not a valid field");
            var fieldNames = !field.Contains('.') ? new[] { field } : field.Split(".");
            MemberExpression member = Expression.Property(expression, fieldNames[0]);
            for (int i = 1; i < fieldNames.Length; i++)
                member = Expression.Property(member, fieldNames[i]);
            return member;
        }

        private static bool IsValidField(string field)
        {
            return Regex.IsMatch(field, @"^([a-zA-Z_]+\d*\.?)+([a-zA-Z_]\d*)+$");
        }
    }

    public enum Operator
    {
        Equal, LessThan, GreaterThan, LessThanOrEqual, GreaterThanOrEqual, NotEqual
    }
}
