namespace Berberim.Entities
{
    public class Salon
    {
        public int salonID { get; set; }  // Pk
        public string? salonName { get; set; }
        public string? salonType { get; set; } 
        public string? workingHours { get; set; } 
        public string? salonAddress { get; set; }

        // Salon ile ilişkilendirilmiş çalışanlar
        public ICollection<Personel>? Personels { get; set; }

        // Salon ile ilişkilendirilmiş hizmetler
        public ICollection<Hizmet>? Hizmets { get; set; }
    }
}
