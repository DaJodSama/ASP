using NguyenVanThienDao_2121110390.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NguyenVanThienDao_2121110390.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}