using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApniShop.Utalities
{
    public static class Helper
    {
        public static string Admin = "admin";
        public static string User = "user";


        public static List<SelectListItem> GetRolesForDropDown() {

            return new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.Admin,Text=Helper.Admin},
                new SelectListItem{Value=Helper.User,Text=Helper.User},
            };

        }


    }
}
