using Microsoft.EntityFrameworkCore;
using Nitui_Andrei_Lab2.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<Nitui_Andrei_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Nitui_Andrei_Lab2Context")));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
