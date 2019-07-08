using GamingStore_Projectoti2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GamingStore_Projectoti2.Startup))]
namespace GamingStore_Projectoti2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // executar o método para configurar a aplicação
            iniciaAplicacao();

        }




        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Agente, Funcionario, Condutor
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void iniciaAplicacao()
        {

            // identifica a base de dados de serviço à aplicação
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Cliente'
            if (!roleManager.RoleExists("Cliente"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Cliente";
                roleManager.Create(role);
            }



            // criar a Role 'Gestor'
            if (!roleManager.RoleExists("Gestor"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Gestor";
                roleManager.Create(role);
            }

            // criar a Role 'admin'
            if (!roleManager.RoleExists("Admin"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }




            // criar um utilizador 'Cliente'
            var user = new ApplicationUser();

            user.UserName = "Pedro@mail.pt";
            user.Email = "Pedro@mail.pt";
          
            string userPWD = "123_Asd";
            var chkUser = userManager.Create(user, userPWD);

            //Adicionar o Utilizador à respetiva Role-Cliente
            if (chkUser.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, "Cliente");
            }

        
        // criar um utilizador 'Admin'
        var user1 = new ApplicationUser();

        user1.UserName = "admin@mail.pt";
            user1.Email = "admin@mail.pt";
            //  user.Nome = "Luís Freitas";
            string userPWD1 = "123_Asd";
        var chkUser1 = userManager.Create(user1, userPWD1);

            //Adicionar o Utilizador à respetiva Role-Agente
            if (chkUser1.Succeeded)
            {
                var result = userManager.AddToRole(user1.Id, "Admin");
                 }
            // criar um utilizador 'Gestor'
            var user2 = new ApplicationUser();

            user2.UserName = "gestor@mail.pt";
            user2.Email = "gestor@mail.pt";
           
            string userPWD2 = "123_Asd";
            var chkUser2 = userManager.Create(user2, userPWD2);

            //Adicionar o Utilizador à respetiva Role-Gestor
            if (chkUser2.Succeeded)
            {
                var result = userManager.AddToRole(user2.Id, "Gestor");
            }
        }

        // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
    }
}
