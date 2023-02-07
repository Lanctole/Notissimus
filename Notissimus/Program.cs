using Microsoft.EntityFrameworkCore;
using Notissimus.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

string? connection = builder.Configuration.GetConnectionString("NotissimusContext");
builder.Services.AddDbContext<NotissimusDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapDefaultControllerRoute();
app.Run();
