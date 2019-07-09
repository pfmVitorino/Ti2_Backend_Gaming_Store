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
    [Authorize(Roles = "Admin")] // apenas o admin tem acesso a esta página
    public class ComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// aqui é onde o administrador pode ver todas as compras feitas na loja
        /// </summary>
        /// <returns></returns>
        // GET: Compras
        public ActionResult Index()
        {
            var compras = db.Compras.Include(c => c.Clientes);
            return View(compras.ToList());
        }
        /// <summary>
        /// Todos os detalhes de uma compra estão qui presentes
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return RedirectToAction("Index");
            }
            return View(compras);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.ClientesFK = new SelectList(db.Users, "Id", "Nome");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Data,Preco,ClientesFK")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Compras.Add(compras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientesFK = new SelectList(db.Users, "Id", "Nome", compras.ClientesFK);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ClientesFK = new SelectList(db.Clientes, "Id", "Nome", compras.ClientesFK);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,Preco,ClientesFK")] Compras compras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientesFK = new SelectList(db.Users, "Id", "Nome", compras.ClientesFK);
            return View(compras);
        }
        /// <summary>
        /// Caso o admin pretenda remover alguma compra
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Compras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Compras compras = db.Compras.Find(id);
            if (compras == null)
            {
                return RedirectToAction("Index");
            }
            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compras compras = db.Compras.Find(id);
            db.Compras.Remove(compras);
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
