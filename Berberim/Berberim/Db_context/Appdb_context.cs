using Berberim.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Berberim.Db_context
{
    public class Appdb_context : IdentityDbContext<AppUser,Role,int>
    {
        public Appdb_context(DbContextOptions<Appdb_context> options) : base(options) { }

    }
}
