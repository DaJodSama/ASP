using NguyenVanThienDao_2121110390.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NguyenVanThienDao_2121110390.Models
{
    public class HomeModel
    {
        public List<Product> ListProducts { get; set; }
        public List<Category> ListCategories { get; set; }
    }
}