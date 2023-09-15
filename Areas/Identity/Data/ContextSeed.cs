using Microsoft.AspNetCore.Identity;
namespace UserHelpPageTemplate.Areas.Identity.Data
{

    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            // Seed Roles with additional information
            var supervisorRole = new ApplicationRole(Enums.Roles.Supervisor.ToString())
            {
                RoleName = Enums.Roles.Supervisor.ToString(),
                Cloak = false,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedOn = DateTime.UtcNow
            };
            await roleManager.CreateAsync(supervisorRole);

            var adminRole = new ApplicationRole(Enums.Roles.Admin.ToString())
            {
                RoleName = Enums.Roles.Admin.ToString(),
                Cloak = false,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedOn = DateTime.UtcNow
            };
            await roleManager.CreateAsync(adminRole);

            var userRole = new ApplicationRole(Enums.Roles.User.ToString())
            {
                RoleName = Enums.Roles.User.ToString(),
                Cloak = false,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedOn = DateTime.UtcNow
            };
            await roleManager.CreateAsync(userRole);
        }

        public static async Task SeedSupervisorAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Supervisor",
                Email = "superadmin@gmail.com",                
                FirstName = "Mukesh",
                LastName = "Murugan",
                EmailConfirmed = true,
                Cloak = false,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedOn = DateTime.UtcNow,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Holtec123!");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Supervisor.ToString());
                    
                }

            }
        }
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "Admin@gmail.com",
                EmailConfirmed = true,
                Cloak = false,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedOn = DateTime.UtcNow,
                PhoneNumberConfirmed = true

            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Holtec123!");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                }

            }
        }

        public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "User",
                FirstName = "User",
                LastName = "User",
                Email = "User@gmail.com",
                EmailConfirmed = true,
                Cloak = false,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = "System",
                UpdatedOn = DateTime.UtcNow,
                PhoneNumberConfirmed = true

            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Holtec123!");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.User.ToString());

                }

            }
        }
    }
}
