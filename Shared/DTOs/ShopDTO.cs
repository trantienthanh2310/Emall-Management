﻿using System.Text.Json.Serialization;

namespace Shared.DTOs
{
    public class ShopDTO
    {
        public int Id { get; set; }

        [JsonPropertyName("shop_Name")]
        public string ShopName { get; set; }

        [JsonPropertyName("user_Id")]
        public string UserId { get; set; }

        [JsonPropertyName("status")]
        public bool IsAvailable { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string Avatar { get; set; }

        public string[] Images { get; set; }

        public string Floor { get; set; }

        public string Position { get; set; }
    }
}
