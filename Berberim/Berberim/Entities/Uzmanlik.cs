namespace Berberim.Entities
{
    public class Uzmanlik
    {
        public int UzmanlikID { get; set; } // Primary Key
        public string? UzmanlikName { get; set; }  // Saç Kesimi, Saç Boyama vb.

        public ICollection<Personel>? personels { get; set; }          // Uzmanlık alanı ile ilişkilendirilmiş çalışanlar
    }
}
