using Microsoft.AspNetCore.Identity;

namespace SistemaTelefonico.Data;

public static class DbInitializer
{
    public static async Task SeedRolesAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Administrador", "Lectura" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        await EnsureUserWithRoleAsync(
            userManager,
            email: "admin@telefonos.com",
            password: "Admin123*",
            role: "Administrador");

        await EnsureUserWithRoleAsync(
            userManager,
            email: "lectura@telefonos.com",
            password: "Lectura123*",
            role: "Lectura");
    }

    private static async Task EnsureUserWithRoleAsync(
        UserManager<IdentityUser> userManager,
        string email,
        string password,
        string role)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"No se pudo crear el usuario {email}: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            var roleResult = await userManager.AddToRoleAsync(user, role);
            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"No se pudo asignar el rol {role} a {email}: {errors}");
            }
        }
    }
}