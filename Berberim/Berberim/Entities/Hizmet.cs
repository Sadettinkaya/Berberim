namespace Berberim.Entities
{
    public class Hizmet
    {
        public int hizmetID { get; set; } // Primary Key
        public string? hizmetName { get; set; } 
        public Double hizmetDuration { get; set; } // Dakika cinsinden
        public decimal hizmetPrice { get; set; } 

        public int salonID { get; set; }  //fk
        public Salon? salon { get; set; }  


        public ICollection<Randevu>? Randevus { get; set; }   

    }
}
