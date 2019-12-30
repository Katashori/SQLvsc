using Microsoft.EntityFrameworkCore;

namespace SQLvcs.Models
{
    public class SQLvcsContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<Database> Databases { get; set; }
        public DbSet<Dacpac> Dacpacs { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=SQLvcs.db");
        }
    }
}
