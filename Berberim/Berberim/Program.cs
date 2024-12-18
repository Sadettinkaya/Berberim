using Berberim.Db_context;
using Berberim.Entities;
using Berberim.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// PostgreSQL ba�lant� dizesi
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// DbContext'i PostgreSQL ile yap�land�rma
builder.Services.AddDbContext<Appdb_context>(options =>
	options.UseNpgsql(connectionString)); // PostgreSQL i�in Npgsql kullan�m�

// Identity konfig�rasyonu
builder.Services.AddIdentity<AppUser, Role>(options =>
{
	// �ifre ayarlar�
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 3; // Minimum �ifre uzunlu�u
})
.AddEntityFrameworkStores<Appdb_context>()
.AddDefaultTokenProviders();

// Cookie konfig�rasyonu (Login ve AccessDenied y�nlendirmeleri)
builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = "/Giris/Index"; // Giri� yapma sayfas�
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

// Identity kullan�m�
app.UseAuthentication(); // Kimlik do�rulama
app.UseAuthorization();  // Yetkilendirme

app.UseEndpoints(endpoints =>
{
    // REST API Controller'lar� eklemek i�in
    endpoints.MapControllers();

    endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);

	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
});

// Rolleri ve varsay�lan Admin kullan�c�y� olu�turma
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

	// Rolleri olu�tur
	var roles = new[] { "Admin", "User" };
	foreach (var role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new Role { Name = role });
		}
	}

	// Admin kullan�c� olu�tur
	var adminEmail = "G221210006@sakarya.edu.tr";
	var adminPassword = "sau"; // �ifrenizi daha karma��k yapabilirsiniz
	if (await userManager.FindByEmailAsync(adminEmail) == null)
	{
		var adminUser = new AppUser
		{
			UserName = adminEmail,
			Email = adminEmail,
			adSoyad = "Sistem Y�neticisi",
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
