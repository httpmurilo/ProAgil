using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProaAgil.Repository.Data
{
   public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Evento> Eventos {get;set;}
        public DbSet<Lote> Lotes{get;set;}
        public DbSet<Palestrante> Palestrantes {get;set;}
        public DbSet<RedeSocial> RedeSociais {get;set;}
        public DbSet<PalestranteEvento> PalestranteEventos{get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //N:N
            builder.Entity<PalestranteEvento>()
                .HasKey( x => new {x.EventoId, x.PalestranteId});
        }
    }
}