
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

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
    .AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
