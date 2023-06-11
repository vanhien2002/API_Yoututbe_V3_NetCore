using BlueTube.NETCore.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
*/
//change router
/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "SeachVideo",
        pattern: "Search/{productId}",
        defaults: new { controller = "Products", action = "Details" });
});
*/
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
       name: "Contact",
       pattern: "{l}",
       new { Controller = "Home", action = "Index" }
     );
    endpoints.MapControllerRoute(
       name: "Contact",
       pattern: "Contact",
       new { Controller = "Home", action = "Contact" }
     );
    endpoints.MapControllerRoute(
        name: "About",
        pattern: "About",
        new { Controller = "Home", action = "About" }
      );
    endpoints.MapControllerRoute(
       name: "TimKiem",
      pattern: "search/{id}/{l}",
       new { Controller = "Home", action = "search" }
       );
    endpoints.MapControllerRoute(
      name: "Xem",
      pattern: "Watch/{id}",
      new { Controller = "Home", action = "search" }
      );
    endpoints.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.UseExceptionHandler("/Error");
app.Run();