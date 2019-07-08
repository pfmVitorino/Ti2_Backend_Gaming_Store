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
    [Authorize(Roles = "Cliente")] // apenas os clientes têm acesso a esta página
    public class CarrinhoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carrinho
        public ActionResult Index()
        {
            // cada cliente tem acesso ao seu carrinho
            var carrinhoes = db.Carrinhoes.Include(c => c.Jogos).Where(c => c.ClienteUserName == User.Identity.Name);
            return View(carrinhoes.ToList());
        }

        // GET: Detalhes_Compra
        public ActionResult Saudacao()
        {
            // quando a compra tiver sucesso , o cliente vai para esta view
            return View("Saudacao");
        }

        // GET: Carrinho/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Carrinho carrinho = db.Carrinhoes.Find(id);
            if (carrinho == null)
            {
                return RedirectToAction("Index");
            }
            return View(carrinho);
        }

       /// <summary>
       /// Função que vai adicionar um novo jogo ao carrinho ou
       /// editar a quantidade de um jogo que já lá esteja
       /// </summary>
       /// <param name="jogoId"></param>
       /// <returns></returns>
        // POST: Carrinho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (int jogoId)
        {
            // variável que vai procurar no BD  os clientes e o id associado ao jogo

            var itemcarrinho = db.Carrinhoes.Where(c=>c.ClienteUserName==User.Identity.Name && c.JogoFk==jogoId).FirstOrDefault();


            if (ModelState.IsValid)
            {
                // se o carrinho estiver vazio
                if (itemcarrinho == null)
                {

                    // vai adicionar um novo jogo ao carrinho
                    Carrinho car = new Carrinho();
                    car.ClienteUserName = User.Identity.Name;
                    car.JogoFk = jogoId;
                    car.Quantidade = 1;


                    db.Carrinhoes.Add(car);
                    db.SaveChanges();


                }
                // caso o jogo já exista no carrinho, a sua quantidade vai aumentar "+1"
                else
                {
                    itemcarrinho.Quantidade = itemcarrinho.Quantidade + 1;

                    //db.Carrinhoes.Add(itemcarrinho);
                    db.Entry(itemcarrinho).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
           
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Aqui é onde se processa toda a compra após
        /// os jogos estare, no carrinho.
        /// O cliente confirma a compra, e ela é guardada na bd das compras
        /// </summary>
        /// <returns></returns>
      // função para finalizar a compra
        public ActionResult Finish()
        {
            

           if (ModelState.IsValid)
            {
                var itemcarrinho = db.Carrinhoes.Where(c => c.ClienteUserName == User.Identity.Name).ToList();
                // se o carrinho não estiver vazio estiver vazio
                if (itemcarrinho != null)
                {
                    decimal preco = 0;
                    // percorre os itens do carrinho e multiplica o preço do jogo pela quantidade
                    foreach (var item in itemcarrinho)
                        preco += (item.Jogos.Preco * item.Quantidade);

                    var user = db.Users.Where(u => u.UserName == User.Identity.Name).SingleOrDefault();
                    // vai criar uma compra com o preço, data e o cliente associado a ela
                    var compra = new Compras() { Preco = preco, Data = DateTime.Now, Clientes = user, ClientesFK = user.Id };
                    // adiciona uma compra na bd
                    db.Compras.Add(compra);

                    
                    user.ListaDeCompras.Add(compra);
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    // após a compra ser efectuada o carrinho vai ficar vazio
                    foreach (var item in itemcarrinho) 
                    db.Carrinhoes.Remove(item);
                    db.SaveChanges();
                   
                  }
               
                  return RedirectToAction("Saudacao","Carrinho");
            }
            return RedirectToAction("Saudacao","Carrinho");
        }

      

        // GET: Carrinho/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Carrinho carrinho = db.Carrinhoes.Find(id);
            if (carrinho == null)
            {
                return RedirectToAction("Index");
            }
            return View(carrinho);
        }
        /// <summary>
        /// Apagar a compra do carrinho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Carrinho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrinho carrinho = db.Carrinhoes.Find(id);
            db.Carrinhoes.Remove(carrinho);
            db.SaveChanges();
            return RedirectToAction("Index");
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
