using CoreContato.Models;
using Microsoft.EntityFrameworkCore;

namespace APITesteConhecimento.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Contato> Contatos { get; set; }
    }
}
