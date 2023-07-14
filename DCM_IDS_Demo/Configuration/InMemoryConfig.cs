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
            new IdentityResource("state","DCM User State",new List<string>(){"state"}),
            new IdentityResource("role","DCM User Role",new List<string>(){"role"}),
            new IdentityResource("position","DCM User Position",new List<string>(){"position"}),
            new IdentityResource("full_name","DCM User Name",new List<string>(){"full_name"}),
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
                    new Claim("position", "doctor"),
                    new Claim("role", "Admin")
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
                    new Claim("position", "doctor"),
                    new Claim("role", "SiteAdmin")
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
                    new Claim("position", "manager"),
                    new Claim("role", "User")
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
                    new Claim("position", "nurse"),
                    new Claim("role", "User")
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
                AllowOfflineAccess = true,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,  "dcmapi" , "full_name", "state", "position", "role" }
            },
            new Client
            {
                ClientName = "DCM MVC Client",
                ClientId = "dcm-mvc-client",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RedirectUris = new List<string>{ "http://localhost:5213/signin-oidc" },
                RequirePkce = false,         
                AllowOfflineAccess = true,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile,"full_name", "state", "position" , "role" , "dcmapi", "offline_access"},
                ClientSecrets = { new Secret("MVCSecret".Sha512()) },
                PostLogoutRedirectUris = new List<string> { "http://localhost:5213/signout-callback-oidc" },
                RequireConsent = true
            }
        };

     
    
    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope> { new ApiScope("dcmapi", "DCM API" , new List<string>{"full_name", "state", "position", "role"})  };
    
    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource> 
        { 
            new ApiResource("dcmapi", "DCM API") 
            { 
                Scopes = { "dcmapi" , "full_name", "state", "position", "role"} 
            } 
        };
}