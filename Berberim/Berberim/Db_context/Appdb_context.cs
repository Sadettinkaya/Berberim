using Berberim.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Berberim.Db_context
{
    public class Appdb_context : IdentityDbContext<AppUser,Role,int>
    {
        public Appdb_context(DbContextOptions<Appdb_context> options) : base(options) { }

		public DbSet<Salon>? salons { get; set; }
		public DbSet<Personel>? personnels { get; set; }
		public DbSet<Uzmanlik>? uzmanlıks { get; set; }
		public DbSet<Hizmet>? hizmets { get; set; }
		public DbSet<Randevu>? randevus { get; set; }

		public DbSet<PersonelKazanc> kazancs { get; set; }
	}
}
