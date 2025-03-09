using LoadFit.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadFit.Repository.Data.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    DisplayName = "Mina Hany",
                    Email = "mina.hany@gmail.com",
                    UserName = "mina.hany",
                    PhoneNumber = "01212121212",
                };

                await _userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
