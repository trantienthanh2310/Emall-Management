﻿namespace Shared.DTOs
{
    public class ProductDTO : MinimalProductDTO
    {
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public bool IsNewProduct { get; set; }

        public double AverageRating { get; set; }
    }
}