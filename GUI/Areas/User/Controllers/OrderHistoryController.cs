﻿using GUI.Abtractions;
using GUI.Clients;
using GUI.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Net;
using System.Threading.Tasks;

namespace GUI.Areas.User.Controllers
{
    [Authorize]
    [Route("/order-history")]
    public class OrderHistoryController : BaseUserController
    {
        private readonly IInvoiceClient _orderHistoryClient;

        public OrderHistoryController(IInvoiceClient orderHistoryClient)
        {
            _orderHistoryClient = orderHistoryClient;
        }

        public async Task<IActionResult> Index()
        {
            Request.QueryString = new QueryString();
            var token = await HttpContext.GetTokenAsync(SystemConstant.Authentication.ACCESS_TOKEN_KEY);
            var orderHistoryRespone = await _orderHistoryClient.GetOrderHistoryOfUser(token, User.GetUserId().ToString());
            if (!orderHistoryRespone.IsSuccessStatusCode)
            {
                if (orderHistoryRespone.StatusCode == HttpStatusCode.Unauthorized
                    || orderHistoryRespone.StatusCode == HttpStatusCode.Forbidden)
                    return Redirect("/Authentication/SignOut?redirectUrl=/order-history");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return View(orderHistoryRespone.Content.Data);
        }
    }
}
