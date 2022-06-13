﻿using CheckoutService.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;
using Shared.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckoutService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/checkout")]
    public class CheckOutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CheckOutController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResult> CheckOut([FromForm(Name = "requestModel")] CheckOutRequestModel requestModel)
        {
            Guid userId = Guid.Empty;
            var productIds = new List<Guid>();
            var shippingAddress = requestModel.ShippingAddress;
            try
            {
                userId = Guid.Parse(requestModel.UserId);
                productIds = requestModel.ProductIds.Split(",").Select(id => Guid.Parse(id)).ToList();
            } catch (Exception e)
            {
                return ApiResult.CreateErrorResult(400, e.Message);
            }
            var result = await _mediator.Send(new CheckOutCommand(userId, productIds, requestModel.ShippingName,
                requestModel.ShippingPhone, shippingAddress, requestModel.OrderNotes, requestModel.PaymentMethod));
            if (result.IsSuccess)
                return ApiResult<string>.CreateSucceedResult(result.Response);
            return ApiResult.CreateErrorResult(500, result.ErrorMessage);
        }
    }
}
