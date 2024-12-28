using System.ComponentModel.DataAnnotations;

namespace Berberim.Entities
{
    public class Randevu
    {
        public int randevuID { get; set; } // Primary Key

        [Required(ErrorMessage = "Randevu tarihi gereklidir.")]
        [DataType(DataType.Date)]
        public DateTime randevuTarih { get; set; }     

        [Required(ErrorMessage = "Randevu saati gereklidir.")]
        [DataType(DataType.Time)]
        public TimeSpan randevuSaat { get; set; }    

        public int MusteriID { get; set; } // fk

        [Required(ErrorMessage = "Müsteri adı tarihi gereklidir.")]
        public string MusteriName { get; set; } = string.Empty;
        public AppUser? Musteri { get; set; } 

        [Required(ErrorMessage = "Personel seçimi gereklidir.")]
        public int personelID { get; set; }    // fk
        public Personel? personel { get; set; }

        [Required(ErrorMessage = "Hizmet seçimi gereklidir.")]
        public int hizmetID { get; set; }   // fk

        public string hizmetName { get; set; }
        public Hizmet? hizmet { get; set; }

        public bool onaylandimi { get; set; }   
        public string notes { get; set; }

        [Required(ErrorMessage = "Telefon numarası gereklidir.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string tel { get; set; }

    }
}
