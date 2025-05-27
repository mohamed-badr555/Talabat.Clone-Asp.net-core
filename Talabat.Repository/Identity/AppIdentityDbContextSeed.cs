using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public  static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() ==0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Mohamed Adel",
                    Email = "mohamedb.555dr@gmail.com",
                    UserName = "Mohamed.Adel",
                    PhoneNumber = "01012345678",
                };
                await _userManager.CreateAsync(user,"Pa$$w0rd");
            }
        }
    }
}
