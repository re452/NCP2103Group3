using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PawGress;
using PawGress.Data;
using PawGress.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=pawgress.db")
           // Suppress the PendingModelChangesWarning so the app can call Migrate() at startup
           .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PetService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<ChallengeService>();

// Add authentication (cookie) so we can sign users in/out
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// --- Initialize DB ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Authentication & Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

// --- Minimal auth endpoints (form posts) ---
app.MapPost("/api/auth/register", async (HttpContext http, UserService userService) =>
{
    var form = await http.Request.ReadFormAsync();
    var username = form["username"].ToString();
    var password = form["password"].ToString();

    if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
    {
        http.Response.Redirect("/signup?error=empty");
        return;
    }

    var created = await userService.CreateUserAsync(username, password);
    if (created == null)
    {
        // user exists
        http.Response.Redirect("/signup?error=exists");
        return;
    }

    // sign in newly created user
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, created.Username), new Claim(ClaimTypes.NameIdentifier, created.Id.ToString()) };
    var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(ci);
    await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    http.Response.Redirect("/dashboard");
});

app.MapPost("/api/auth/login", async (HttpContext http, UserService userService) =>
{
    var form = await http.Request.ReadFormAsync();
    var username = form["username"].ToString();
    var password = form["password"].ToString();

    var user = await userService.AuthenticateAsync(username, password);
    if (user == null)
    {
        http.Response.Redirect("/?error=invalid");
        return;
    }

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username), new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
    var ci = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(ci);
    await http.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    http.Response.Redirect("/dashboard");
});

app.MapPost("/api/auth/logout", async (HttpContext http) =>
{
    await http.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    http.Response.Redirect("/");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
