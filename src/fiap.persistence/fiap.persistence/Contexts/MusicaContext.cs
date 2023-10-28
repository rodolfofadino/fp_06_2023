using fiap.domain.Models;
using Microsoft.EntityFrameworkCore;

namespace fiap.persistence.Contexts
{
    public class MusicaContext : DbContext
    {
        public MusicaContext(DbContextOptions<MusicaContext> options) : base(options)
        {
        }

        public DbSet<Musica> Musicas { get; set; }
    }
}
