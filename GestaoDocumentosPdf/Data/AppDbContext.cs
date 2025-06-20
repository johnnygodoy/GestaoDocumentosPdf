using Microsoft.EntityFrameworkCore;

namespace GestaoDocumentosPdf.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Documento> Documentos => Set<Documento>();        

    }
}
