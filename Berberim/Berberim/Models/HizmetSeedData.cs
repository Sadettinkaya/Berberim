using Berberim.Db_context;
using Berberim.Entities;
using Microsoft.EntityFrameworkCore;

namespace Berberim.Models
{
    public static class HizmetSeedData
    {
        public static async Task SeedHizmets(IApplicationBuilder app)
        {
            // Database context'i al
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<Appdb_context>();

            // Eğer migration varsa uygulansın
            if (context.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("Applying pending migrations...");
                await context.Database.MigrateAsync();
            }

            // Eklenmesi gereken hizmetler
            var hizmetListesi = new List<Hizmet>
            {
                new Hizmet { hizmetName = "Bıyık Bakımı", hizmetDuration = 30, hizmetPrice = 100, salonID = 1 },
                new Hizmet { hizmetName = "Peruk Takma", hizmetDuration = 10, hizmetPrice = 50, salonID = 1 },
                new Hizmet { hizmetName = "Saç Boyama", hizmetDuration = 45, hizmetPrice = 400, salonID = 1 },
                new Hizmet { hizmetName = "Cilt Bakımı", hizmetDuration = 60, hizmetPrice = 500, salonID = 1 }
            };

            // Her bir hizmeti tek tek kontrol edip ekle
            foreach (var hizmet in hizmetListesi)
            {
                var hizmetVarMi = await context.hizmets.AnyAsync(h => h.hizmetName == hizmet.hizmetName);
                if (!hizmetVarMi)
                {
                    await context.hizmets.AddAsync(hizmet);
                    Console.WriteLine($"'{hizmet.hizmetName}' eklendi.");
                }
                else
                {
                    Console.WriteLine($"'{hizmet.hizmetName}' zaten mevcut.");
                }
            }

            // Değişiklikleri kaydet
            await context.SaveChangesAsync();
            Console.WriteLine("Seed data kontrolü tamamlandı.");
        }
    }
}
