using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcFlight.Data;
using MvcFlight.Models;

var builder = WebApplication.CreateBuilder(args);


// SQL BUILDER
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MvcFlightContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("MvcFlightContext")));
}
else
{
    builder.Services.AddDbContext<MvcFlightContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionMvcFlightContext")));
}


// builder.Services.AddDbContext<MvcFlightContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("MvcFlightContext") ?? throw new InvalidOperationException("Connection string 'MvcFlightContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



// SEED DATA
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
