using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        // options.addPolicy adds a new policy
        services.AddAuthorizationCore(options =>
        {
            // RequireClaim("Domain", "via"), meaning the user must have a claim, where the type is "Domain", and the value is "via"
            options.AddPolicy("MustBeVia", a =>
                a.RequireAuthenticatedUser().RequireClaim("Domain", "via"));
            options.AddPolicy("SecurityLevel4", a =>
                a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4", "5"));
            options.AddPolicy("MustBeTeacher", a =>
                a.RequireAuthenticatedUser().RequireClaim("Role", "Teacher"));

            // RequireAssertion() adds an AssertionRequirement to the current instance. It is used to add a custom requirement
            // to an authorization policy.
            options.AddPolicy("SecurityLevel2orAbove", a =>
                a.RequireAuthenticatedUser().RequireAssertion(context =>
                {
                    // context contains a user property of type ClaimsPrincipal. It contains information about the current user
                    //FindFirst() is used to find the first Claim with the Type of "SecurityLevel". If no such claim exists, the
                    //user has no security level, and we return "false". If the claim is found, we check if the security level is
                    //above a certain value, in this case level 2
                    Claim? levelClaim = context.User.FindFirst(claim => claim.Type.Equals("SecurityLevel"));
                    if (levelClaim == null) return false;
                    return int.Parse(levelClaim.Value) >= 2;
                }));
        });
    }
}