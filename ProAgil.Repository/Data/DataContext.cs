using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProaAgil.Repository.Data
{
   public class DataContext : IdentityDbContext<User, Role, int,
            IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
            IdentityRoleClaim<int>, IdentityUserToken<int>>
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
            base.OnModelCreating(builder);
            builder.Entity<UserRole>(UserRole =>
            {
                UserRole.HasKey(ur => new{ur.UserId, ur.RoleId});
                UserRole.HasOne(ur => ur.Role)
                    .WithMany( r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                    
                 UserRole.HasOne(ur => ur.User)
                    .WithMany( r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            //N:N
            builder.Entity<PalestranteEvento>()
                .HasKey( x => new {x.EventoId, x.PalestranteId});
        }
    }
}