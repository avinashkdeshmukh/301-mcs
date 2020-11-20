using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MT.OnlineRestaurant.Utility
{
    public class UserDetails
    {
        public static int GetUserId(HttpContext context)
        {
            return context.User.GetLoggedInUserId<int>();
            
        }

        public static int GetUserIdByName(string name)
        {
            return 1;
        }
    }
}
