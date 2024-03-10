﻿using System.Web.Mvc;

namespace NguyenVanThienDao_2121110390.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "NguyenVanThienDao_2121110390.Areas.Admin.Controllers" }
            );
        }
    }
}