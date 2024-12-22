using Microsoft.EntityFrameworkCore;
using API_CSHARP.Estudantes;

namespace API_CSHARP.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Estudante> Estudantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Banco.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
    }
}