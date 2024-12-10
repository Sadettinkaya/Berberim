using Microsoft.AspNetCore.Identity;

namespace Berberim.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string adSoyad { get; set; }

        public ICollection<Randevu>? Randevus { get; set; }       // Müşteri ile ilişkilendirilmiş randevular
    }
}
