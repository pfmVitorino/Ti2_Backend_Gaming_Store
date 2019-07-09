using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GamingStore_Projectoti2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // atributos dos clientes
        public string Nome { get; set; }


        public string Morada { get; set; }

        public string NIF { get; set; }

        [Display(Name = "Código Postal")]
        public string CodPostal { get; set; }

        // Um  Cliente  pode comprar 1 ou mais Jogos
        public ICollection<Compras> ListaDeCompras { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
          
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("JogosDB", throwIfV1Schema: false)
        {
            
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // representa a Base de Dados
        // descrever todas as tabelas
      
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Jogos> Jogos { get; set; }
        public DbSet<Plataformas> Plataformas { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Detalhes_Compra> Detalhes_Compra { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<GamingStore_Projectoti2.Models.Carrinho> Carrinhoes { get; set; }
    }
}