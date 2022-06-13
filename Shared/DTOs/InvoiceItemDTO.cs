﻿namespace Shared.DTOs
{
    public class InvoiceItemDTO
    {
        public string ProductId { get; set; }

        public string Image { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public bool CanBeRating { get; set; }
    }
}
