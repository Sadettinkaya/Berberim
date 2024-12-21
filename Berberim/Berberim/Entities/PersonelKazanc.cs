using Berberim.Entities;
using System.ComponentModel.DataAnnotations;

namespace Berberim.Entities
{ 

public class PersonelKazanc
{
    [Key]
    public int PersonelKazancID { get; set; } // Primary Key
    public int PersonnelID { get; set; } // Foreign Key to Personnel

    [DataType(DataType.Date)]
    public DateTime Date { get; set; } // Kazanç Tarihi
    public decimal TotalPersonelKazanc { get; set; } // Günlük Kazanç


    public Personel Personnel { get; set; }
}


}