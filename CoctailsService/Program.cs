using CoctailsService;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddLogging();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "GitHub";
})
               .AddCookie()
               .AddOAuth("GitHub", options =>
               {
                   options.ClientId = "33000cc3c74424facb36";
                   options.ClientSecret = "ab3b90613ac14b3642484d8b3e8844a758f74a81";
                   options.CallbackPath = new PathString("/Veryfie/Google");
                   options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                   options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                   options.UserInformationEndpoint = "https://api.github.com/user";
                   options.ClaimsIssuer = "OAuth2-Github";
                   options.SaveTokens = true;
                   options.Events = new OAuthEvents
                   {
                       OnCreatingTicket = async context => 
                       {
                           var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                           request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                           request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                           var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                           response.EnsureSuccessStatusCode();
                           var user = JObject.Parse(await response.Content.ReadAsStringAsync());
                           AddClaims(context, user);
                       }
                   };
               });

static void AddClaims(OAuthCreatingTicketContext context, JObject user)
{
    var identifier = user.Value<string>("id");
    if (!string.IsNullOrEmpty(identifier))
    {
        context.Identity.AddClaim(new Claim(
            ClaimTypes.NameIdentifier, identifier,
            ClaimValueTypes.String, context.Options.ClaimsIssuer));
    }

    var userName = user.Value<string>("login");
    if (!string.IsNullOrEmpty(userName))
    {
        context.Identity.AddClaim(new Claim(
            ClaimsIdentity.DefaultNameClaimType, userName,
            ClaimValueTypes.String, context.Options.ClaimsIssuer));
    }

    var name = user.Value<string>("name");
    if (!string.IsNullOrEmpty(name))
    {
        context.Identity.AddClaim(new Claim(
            "urn:github:name", name,
            ClaimValueTypes.String, context.Options.ClaimsIssuer));
    }

    var link = user.Value<string>("url");
    if (!string.IsNullOrEmpty(link))
    {
        context.Identity.AddClaim(new Claim(
            "urn:github:url", link,
            ClaimValueTypes.String, context.Options.ClaimsIssuer));
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CoctailsContext>();
    db.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); 

var loggerFactory = app.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

app.Run();
