namespace Berberim.Entities
{
    public class Hizmet
    {
        public int hizmetID { get; set; } // Primary Key
        public string? hizmetName { get; set; } // Saç Kesimi, Saç Boyama, vb.
        public Double hizmetDuration { get; set; } // Dakika cinsinden
        public decimal hizmetPrice { get; set; } // Fiyat

        public int salonID { get; set; }  // Foreign Key (Hizmet salonuna bağlı)
        public Salon? salon { get; set; }  // Navigation property (Salon ile ilişki kurma)


        public ICollection<Randevu>? Randevus { get; set; }    // Hizmet ile ilişkilendirilmiş randevular

    }
}
