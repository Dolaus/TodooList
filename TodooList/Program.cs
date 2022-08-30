using Microsoft.EntityFrameworkCore;
using TodooList.Data;
using TodooList.Models;
using TodooList.Services.Class;
using TodooList.Services.Interface;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDBContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IFiltrator<User>,Filtrator>();
builder.Services.AddScoped<IPaginator<User>,Paginator>();

var app = builder.Build();

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
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
