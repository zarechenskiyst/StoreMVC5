﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_store.Models.Data.ViewModels.Cart
{
    public class CartVM
    {
        public int ProductId { get; set; }

        public string ProductName{get; set;}

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total
        {
            get { return Quantity * Price; }
        }
    }
}