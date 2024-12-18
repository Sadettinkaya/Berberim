using Berberim.Db_context;
using Berberim.Entities;
using Berberim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// PostgreSQL baðlantý dizesi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext'i PostgreSQL ile yapýlandýrma
builder.Services.AddDbContext<Appdb_context>(options =>
	options.UseNpgsql(connectionString)); // PostgreSQL için Npgsql kullanýmý

// Identity konfigürasyonu
builder.Services.AddIdentity<AppUser, Role>(options =>
{
	// Þifre ayarlarý
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 3; // Minimum þifre uzunluðu
})
.AddEntityFrameworkStores<Appdb_context>()
.AddDefaultTokenProviders();

// Cookie konfigürasyonu (Login ve AccessDenied yönlendirmeleri)
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Giris/Index"; // Giriþ yapma sayfasý
	options.AccessDeniedPath = "/Giris/AccessDenied"; // Yetki reddedilirse
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Identity kullanýmý
app.UseAuthentication(); // Kimlik doðrulama
app.UseAuthorization();  // Yetkilendirme

app.UseEndpoints(endpoints =>
{
    // REST API Controller'larý eklemek için
    endpoints.MapControllers();

    endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Rolleri ve varsayýlan Admin kullanýcýyý oluþturma
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

	// Rolleri oluþtur
	var roles = new[] { "Admin", "User" };
	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new Role { Name = role });
		}
	}

	// Admin kullanýcý oluþtur
	var adminEmail = "G221210006@sakarya.edu.tr";
	var adminPassword = "sau"; // Þifrenizi daha karmaþýk yapabilirsiniz
	if (await userManager.FindByEmailAsync(adminEmail) == null)
	{
		var adminUser = new AppUser
		{
			UserName = adminEmail,
			Email = adminEmail,
			adSoyad = "Sistem Yöneticisi",
			EmailConfirmed = true
		};

		var result = await userManager.CreateAsync(adminUser, adminPassword);
		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(adminUser, "Admin");
		}
	}
}
await HizmetSeedData.SeedHizmets(app);
app.Run();
