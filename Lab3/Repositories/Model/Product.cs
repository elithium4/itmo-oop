﻿using System.ComponentModel.DataAnnotations;

namespace Lab3.Repositories.Model
{
    public class Product
    {
        [Key]
        public string Name { get; set; }
        public List<StoreProduct> StoreProducts { get; set; }
    }
}