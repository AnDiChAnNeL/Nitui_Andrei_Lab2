using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books");
    options.Conventions.AllowAnonymousToPage("/Books/Index");
    options.Conventions.AllowAnonymousToPage("/Books/Details");

    options.Conventions.AuthorizeFolder("/Members", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Publishers", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/CategoryPages", "AdminPolicy");
});

builder.Services.AddDbContext<Nitui_Andrei_Lab2Context>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Nitui_Andrei_Lab2Context")
    )
);

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Nitui_Andrei_Lab2Context")
    )
);

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();