namespace Berberim.Models
{
    public class Account
    {
        public string Cloud { get; set; } // Bu alan appsettings.json'daki "CloudName" ile eşleşmelidir
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

}
