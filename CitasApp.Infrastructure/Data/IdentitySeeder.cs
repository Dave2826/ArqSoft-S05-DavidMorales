using Microsoft.AspNetCore.Identity;

namespace CitasApp.Infrastructure.Data
{
    public class IdentitySeeder
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeder(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await SeedRoleAsync();
            await SeedAdminUserAsync();
        }

        private async Task SeedRoleAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }
        }

        private async Task SeedAdminUserAsync()
        {
            var adminEmail = "admin@citasapp.com";
            var existingUser = await _userManager.FindByEmailAsync(adminEmail);

            if (existingUser != null)
                return;

            var adminUser = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin_2026!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Administrador");
            }
        }
    }
}
