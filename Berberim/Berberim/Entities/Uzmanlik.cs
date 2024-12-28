namespace Berberim.Entities
{
    public class Uzmanlik
    {
        public int UzmanlikID { get; set; } // pk
        public string? UzmanlikName { get; set; }  

        public ICollection<Personel>? personels { get; set; }          
    }
}
