using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identities;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextDataSeeding
    {
        public static async Task UserAddAsync(UserManager<AppUser> _userManager)
        {
            if(_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisPlayName = "Ahmed Samir",
                    Email = "Ahmed.Samir@Yahoo.Com",
                    UserName = "Ahmed.Samir",
                    PhoneNumber = "01100165898"

                };
                await _userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
