using System.Data.Entity;
using Aurora.Server.Database.Models;

namespace Aurora.Server.Database.Data
{
    public class AuroraDB : DbContext
    {
        public AuroraDB() : base("Server=MAG-N6N0CX06Y64;Database=Aurora.Server.Database;Trusted_Connection=True;") { }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Username); // Assuming Username is the primary key

            base.OnModelCreating(modelBuilder);
        }
    }
}
