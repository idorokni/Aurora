using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Server.Database.Models;

namespace Aurora.Server.Database.Data
{
    public class AuroraDB : DbContext
    {
        public DbSet<User> USER_LOGIN { get; set; } = null!;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
