namespace Berberim.Entities
{
    public class Personel
    {
        public int personelID { get; set; } // Primary Key
        public string? personelName { get; set; }

        public string? personelPassword { get; set; }
        public string? personelEmail { get; set; }
        public string? musaitSaat { get; set; } 
        public int salonID { get; set; } // Foreign Key
        public Salon? salon { get; set; }  // Navigation property (Salon ile ilişki kurma)

        public int UzmanlikID { get; set; } // Foreign Key
        public Uzmanlik? Uzmanliks { get; set; } // Çalışanın uzmanlık alanları

        public ICollection<Randevu>? Randevus { get; set; }   // Çalışan ile ilişkilendirilmiş randevular
    }
}
