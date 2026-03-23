using Microsoft.AspNetCore.Identity;

namespace FoodSafety.MVC.Data;

public class DbSeeder
{
    public static async Task seedRoles(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        // Admin 
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var adminEmail = "admin@gmail.com";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var user = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Admin1234!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
        
        // Inspector
        if (!await roleManager.RoleExistsAsync("Inspector"))
        {
            await roleManager.CreateAsync(new IdentityRole("Inspector"));
        }

        var inspectorEmail = "inspector@gmail.com";
        if (await userManager.FindByEmailAsync(inspectorEmail) == null)
        {
            var user = new IdentityUser
            {
                UserName = inspectorEmail,
                Email = inspectorEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Inspector1234!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Inspector");
            }
        }
        
        // Viewer
        if (!await roleManager.RoleExistsAsync("Viewer"))
        {
            await roleManager.CreateAsync(new IdentityRole("Viewer"));
        }

        var ViewerEmail = "viewer@gmail.com";
        if (await userManager.FindByEmailAsync(ViewerEmail) == null)
        {
            var user = new IdentityUser
            {
                UserName = ViewerEmail,
                Email = ViewerEmail,
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(user, "Viewer1234!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Viewer");
            }
        }

    }
}