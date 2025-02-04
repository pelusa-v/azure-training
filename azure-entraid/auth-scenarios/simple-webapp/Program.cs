using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration, "AzureAd")
    // this is required to call Microsoft Graph or other downstream APIs
    .EnableTokenAcquisitionToCallDownstreamApi(options => builder.Configuration.Bind("AzureAd", options))
    .AddInMemoryTokenCaches();

// STATIC USER CONSENT VS INCREMENTAL USER CONSENT
// We're using static user consent here!
// If you want to use incremental user consent, you have to request minimal and basic scopes here (initial login)
// and athen use AcquireTokenInteractive (from Microsoft Identity library) to request additional scopes when the user accesses a featuer that requires them.
builder.Services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    options.Scope.Add("User.Read"); // Add Microsoft Graph User.Read scope
    options.Scope.Add("Chat.Read"); // Add Microsoft Graph Chat.Read scope
    options.Scope.Add("email");     // Add email scope
    options.Scope.Add("profile");
});

// This configuration is more flexible and allows you to protect specific controllers or actions
// by adding the [Authorize] attribute to them. The [Authorize] attribute is added to all controllers and actions by default.
// AddMicrosoftIdentityUI provides the default sign-in and sign-out actions.
builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

// The options parameters enforce authentication and authorization on all controllers and actions.
// You can exclude specific controllers or actions by adding the [AllowAnonymous] attribute.
// builder.Services.AddControllersWithViews(options =>
// {
//     var policy = new AuthorizationPolicyBuilder()
//         .RequireAuthenticatedUser()
//         .Build();
//     options.Filters.Add(new AuthorizeFilter(policy));
// }).AddMicrosoftIdentityUI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
