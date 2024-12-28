namespace Berberim.Entities
{
    public class Personel
    {
        public int personelID { get; set; } // Primary Key
        public string? personelName { get; set; }

        public string? personelPassword { get; set; }
        public string? personelEmail { get; set; }
        public string? musaitSaat { get; set; } 
        public int salonID { get; set; } // fk
        public Salon? salon { get; set; } 

        public int UzmanlikID { get; set; } // fk
        public Uzmanlik? Uzmanliks { get; set; } 

        public ICollection<Randevu>? Randevus { get; set; }   
    }
}
