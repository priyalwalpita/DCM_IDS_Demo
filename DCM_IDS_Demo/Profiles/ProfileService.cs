using System.Security.Claims;
using DCM_IDS_Demo.Configuration;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Identity;

namespace DCM_IDS_Demo.Profiles;

public class ProfileService : IProfileService
{
    public ProfileService()
    {
    }

    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        // This is where you would add your custom claims.
        string userId = context.Subject.GetSubjectId();
        var user = InMemoryConfig.GetUsers().FirstOrDefault(u => u.SubjectId == userId);
       

        context.IssuedClaims.AddRange(user.Claims);

        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context)
    {
        // In an in-memory user scenario, all users are typically active.
        context.IsActive = true;

        return Task.CompletedTask;
    }
}
