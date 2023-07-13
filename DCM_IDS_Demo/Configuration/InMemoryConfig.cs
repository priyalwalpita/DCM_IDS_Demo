using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace DCM_IDS_Demo.Configuration;

public class InMemoryConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("state","DCM User State",new List<string>(){"state"})
        };
    
    public static List<TestUser> GetUsers() =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "a83717d3-1800-d3a2-0a95-a8e69fab46f7",
                Username = "priyal",
                Password = "P@@$$W0rd",
                Claims = new List<Claim>
                {
                    new Claim("full_name", "Priyal Aruna"),
                    new Claim("state", "nsw"),
                    new Claim("position", "doctor")
                }
            },
            new TestUser
            {
                SubjectId = "0960b3cc-ee94-7929-93a4-b6c1a9f13137",
                Username = "gayan",
                Password = "P@@$$W0rd",
                Claims = new List<Claim>
                {
                    new Claim("full_name", "Gayan Gayan"),
                    new Claim("state", "vic"),
                    new Claim("position", "doctor")
                }
            },
            new TestUser
            {
                SubjectId = "0960b3cc-ee94-7929-93a4-b6c1a9f13137",
                Username = "oshadha",
                Password = "P@@$$W0rd",
                Claims = new List<Claim>
                {
                    new Claim("full_name", "Oshadha Oshadha"),
                    new Claim("state", "vic"),
                    new Claim("position", "manager")
                }
            },
            new TestUser
            {
                SubjectId = "0960b3cc-ee94-7929-93a4-b6c1a9f13137",
                Username = "piyumi",
                Password = "P@@$$W0rd",
                Claims = new List<Claim>
                {
                    new Claim("full_name", "Piyumi Piyumi"),
                    new Claim("state", "nsw"),
                    new Claim("position", "nurse")
                }
            }
        };
    
    
    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "dcm-example",
                ClientSecrets = new [] { new Secret("dcmsecret".Sha512()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,  "dcmapi" , "full_name", "state", "position" }
            }
        };
    
    
    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope> { new ApiScope("dcmapi", "DCM API") };
    
    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource> 
        { 
            new ApiResource("dcmapi", "DCM API") 
            { 
                Scopes = { "dcmapi" } 
            } 
        };
}