﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseAccessor.Models
{
    [Table("ProductComments", Schema = "dbo")]
    public class ProductComment
    {
        public int Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public string Message { get; set; }

        public int? Star { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        
        public virtual ShopProduct Product { get; set; }

        public virtual User User { get; set; }
    }
}
