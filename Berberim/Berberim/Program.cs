using Berberim.Db_context;
using Berberim.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();





// PostgreSQL ba�lant� dizesi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  // appsettings.json'dan al�nabilir

// DbContext'i PostgreSQL ile yap�land�rma
builder.Services.AddDbContext<Appdb_context>(options =>
    options.UseNpgsql(connectionString));  // PostgreSQL kullan�yorsan�z, Npgsql'u burada belirtiyoruz



builder.Services.AddIdentity<AppUser, Role>().AddEntityFrameworkStores<Appdb_context>().AddDefaultTokenProviders();



var app = builder.Build();

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
