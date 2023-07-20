using DCM_IDS_Demo.Configuration;
using DCM_IDS_Demo.Profiles;
using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddTransient<IProfileService, ProfileService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentityServer()
    .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
    .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
    .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
    .AddTestUsers(InMemoryConfig.GetUsers())
    .AddInMemoryClients(InMemoryConfig.GetClients())
    .AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential();

builder.Services.AddAuthentication()
    .AddOpenIdConnect("AzureAD", "Azure AD", options =>
    {
        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        options.SignOutScheme = IdentityServerConstants.SignoutScheme;

        // Replace these with your Azure AD App Registration values
        options.Authority = "https://login.microsoftonline.com/d87f4e34-7b77-4afe-9a46-c8aaecd7f486";
        options.ClientId = "";
        options.ClientSecret = "";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            NameClaimType = "name"
        };

        options.GetClaimsFromUserInfoEndpoint = true;
        options.ResponseType = "code";
        options.SaveTokens = true;
    });

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.None
});

app.UseCookiePolicy();
app.MapControllers();

app.Run();