﻿using System.Collections.Generic;
using System.Linq;

namespace Shared.Validations
{
    public static class FileValidationLinqHelper
    {
        public static IEnumerable<FileValidationRuleName> Except(this FileValidationRuleSet validationRules,
            IEnumerable<FileValidationRuleName> ruleNames)
        {
            return validationRules.Select(validationRule => validationRule.RuleName).Except(ruleNames);
        }

        public static bool Contains(this FileValidationRuleSet validationRules, FileValidationRuleName ruleName)
        {
            return validationRules.Any(rule => rule.RuleName == ruleName);
        }
    }
}
