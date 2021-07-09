using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Halloween.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public string productName { get; set; }

        public float Price { get; set; }
        public int Quantity { get; set; }
        public float Bill { get; set; }
    }
}