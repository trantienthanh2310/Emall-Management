﻿namespace Shared.DTOs
{
    public class MinimalProductDTO
    {
        public string Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Discount { get; set; }

        public int Quantity { get; set; }

        public int ShopId { get; set; }

        public bool IsAvailable { get; set; }

        public string[] Images { get; set; }
    }
}
