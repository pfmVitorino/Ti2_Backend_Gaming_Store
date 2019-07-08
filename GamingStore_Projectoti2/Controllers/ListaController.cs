using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingStore_Projectoti2.Models;

namespace GamingStore_Projectoti2.Controllers
{
  
    public class ListaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Index que mostra toda a lista dos jogos
        /// </summary>
        /// <param name="SearchBy"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        // GET: Lista
        public ActionResult Index(string SearchBy, string search)
        {
            // filtrar os jogo pelo seu nome e pela sua plataforma
            if (SearchBy == "Plataforma")


                return View(db.Jogos.Where(x => x.Plataforma == search || search == null).ToList());
            else
                return View(db.Jogos.Where(x => x.Nome.StartsWith(search) || search == null).ToList());

        }
          
        
    
        
        /// <summary>
        /// Mostra os detalhes do jogo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Lista/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Jogos jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return RedirectToAction("Index");
            }
           
          

            Session["Metodo"] = "";
            return View(jogo);
        }



        /// <summary>
        /// Antes do cliente mandar o jogo para o carrinho, os detalhes do mesmo são mostrados
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Lista/buy/5
       [Authorize(Roles = "Cliente")] // apenas os clientes têm acesso a esta página
        public ActionResult Buy(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Jogos jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return RedirectToAction("Index");
            }

           
          
            Session["Metodo"] = "";
           
            return View(jogo);
        }

      



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
