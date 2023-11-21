using System.Diagnostics;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        string[] specialistRoles = { "woltUser", "woltSpecialist" };
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("isWoltSupplier", a =>
                a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "woltSupplier"));
            options.AddPolicy("isWoltUser", a =>
                a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "woltUser"));
            options.AddPolicy("isWoltSpecialist", a =>
                a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, specialistRoles));
            options.AddPolicy("isAdmin", a =>
                a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "admin"));
        });
    }
}