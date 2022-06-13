﻿using Refit;
using Shared.DTOs;
using Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUI.Clients
{
    public interface IInvoiceClient
    {
        [Get("/invoices/user/{userId}")]
        Task<ApiResponse<ApiResult<Dictionary<string, InvoiceWithItemDTO[]>>>> GetOrderHistoryOfUser([Authorize("Bearer")] string token, string userId);

        [Get("/invoices/{invoiceCode}")]
        Task<ApiResponse<ApiResult<FullInvoiceDTO>>> GetInvoiceDetail([Authorize("Bearer")] string token, string invoiceCode);

        [Get("/invoices/ref/{refId}")]
        Task<ApiResponse<ApiResult<InvoiceWithItemDTO[]>>> GetInvoiceDetailByRefId([Authorize("Bearer")] string token, string refId);

        [Post("/invoices/post-payment/{refId}")]
        Task<ApiResponse<ApiResult>> AfterMomoPaymentProcessing([Authorize("Bearer")] string token, string refId, [Body] AfterPaymentProcessingRequest requestModel);
    }
}
