using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingStore_Projectoti2.Models;

namespace GamingStore_Projectoti2.Controllers
{
    [Authorize(Roles = "Gestor")] // apenas os gestores têm acesso a esta página
    public class JogosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jogos
        public ActionResult Index(string SearchBy, string search)
        {


            // filtar os jogo pelo seu nome e pela sua plataforma
            if (SearchBy == "Plataforma")


                return View(db.Jogos.Where(x => x.Plataforma == search || search == null).ToList());
            else

                return View(db.Jogos.Where(x => x.Nome.StartsWith(search) || search == null).ToList());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Jogos/Details/5
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
            Session["Id"] = jogo.Id;
            Session["Metodo"] = "Jogos/Details";

            return View(jogo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Jogos/Create
        public ActionResult Create()
        {
            ViewBag.Plataformas = db.Plataformas;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jogos"></param>
        /// <param name="uploadFotografia"></param>
        /// <returns></returns>
        // POST: Jogos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Preco,Plataforma")] Jogos jogo, HttpPostedFileBase uploadFotografia)
        {

            //Escreve a fotografia que foi carregada - O save é efetuado na pasta das imagens do disco rigido
            //Especificar id do jogo
            //testar se há registos na tabela dos jogo

            // vars aux

            string caminho = "";
            bool ficheiroValido = false;




            /// 1º será que foi enviado um ficheiro?
            if (uploadFotografia == null)

            {   // volta ao index porque tem de adicionar foto
                return RedirectToAction("Index");

            }
            else
            {


                /// 2º será que o ficheiro, se foi fornecido, é do tipo correto?

                string mimeType = uploadFotografia.ContentType;
                if (mimeType == "image/jpeg" || mimeType == "image/png")

                {
                    // o ficheiro é do tipo correto

                    /// 3º qual o nome que devo dar ao ficheiro?
                    Guid g;
                    g = Guid.NewGuid(); // obtem os dados para o nome do ficheiro
                    // e qual a extensão do ficheiro?
                    string extensao = Path.GetExtension(uploadFotografia.FileName).ToLower();
                    // montar novo nome
                    string nomeFicheiro = g.ToString() + extensao;
                    // onde guardar o ficheiro?
                    caminho = Path.Combine(Server.MapPath("~/fotografias/"), nomeFicheiro);
                    /// 4º como o associar ao novo Jogo?
                    jogo.Fotografia = nomeFicheiro;

                    // marcar o ficheiro como válido
                    ficheiroValido = true;




                }
                else
                {
                    // o ficheiro fornecido nao é válido 
                    // atributo por defeito ao jogo
                    return RedirectToAction("Index");
                    // jogo.Fotografia = "no-user.jpg";
                }
            }



            // confronta os dados que vem da view com a forma que os dados devem  ter .
            // ie, valida os dados com o modelo
            if (ModelState.IsValid)
            {
                try
                {
                    db.Jogos.Add(jogo);
                    db.SaveChanges();


                    /// 5º como o guardar no disco rígido?
                    if (ficheiroValido)
                    {
                        uploadFotografia.SaveAs(caminho);
                    }
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {


                }
                Session["Id"] = jogo.Id;
                Session["Metodo"] = "Jogos/Create";
                ViewBag.Plataformas = db.Plataformas;

            }

            return View(jogo);



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Jogos/Edit/5
        public ActionResult Edit(int? id)
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

            Session["Id"] = jogo.Id;
            Session["Metodo"] = "Jogos/Edit";
            ViewBag.Plataformas = db.Plataformas;
            return View(jogo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jogos"></param>
        /// <returns></returns>
        // POST: Jogos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Preco,Fotografia,Plataforma")] Jogos jogo, HttpPostedFileBase uploadFotografia)
        {
            string caminho = "";



            if (uploadFotografia == null)
            {

                return RedirectToAction("Index");
            }
            else
            {
                db.Entry(jogo).State = EntityState.Modified;
                string mimeType = uploadFotografia.ContentType;
                if (mimeType == "image/jpeg" || mimeType == "image/png")

                {
                    // o ficheiro é do tipo correto

                    /// 3º qual o nome que devo dar ao ficheiro?
                    Guid g;
                    g = Guid.NewGuid(); // obtem os dados para o nome do ficheiro
                    // e qual a extensão do ficheiro?
                    string extensao = Path.GetExtension(uploadFotografia.FileName).ToLower();
                    // montar novo nome
                    string nomeFicheiro = g.ToString() + extensao;
                    // onde guardar o ficheiro?
                    caminho = Path.Combine(Server.MapPath("~/fotografias/"), nomeFicheiro);
                    /// 4º como o associar ao novo Jogo?
                    jogo.Fotografia = nomeFicheiro;





                }
                else
                {
                    // o ficheiro fornecido nao é válido 
                    // atributo por defeito ao jogo
                    return RedirectToAction("Index");
                    // jogo.Fotografia = "no-user.jpg";
                }
            }


            if (ModelState.IsValid)
            {

                uploadFotografia.SaveAs(caminho);
                db.Entry(jogo).State = EntityState.Modified;

                db.SaveChanges();
                ViewBag.Plataformas = db.Plataformas;
                return View(jogo);

            }
            ViewBag.Plataformas = db.Plataformas;
            return View(jogo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Jogos/Delete/5
        public ActionResult Delete(int? id)
        {
            // o ID do jogo não foi fornecido
            // não é possível procurar o jogo
            // o que devo fazer?
            if (id == null)
            {
                ///  opção por defeito do 'template'
                ///  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                /// e não há ID do Jogo, uma de duas coisas aconteceu:
                ///   - há um erro nos links da aplicação
                ///   - Alguem a fazer asneiras no URL

                /// redireciono o utilzador para o ecrã incial
                return RedirectToAction("Index");
            }


            // procura os dados do Jogo, cujo ID é fornecido
           Jogos jogo = db.Jogos.Find(id);

            /// se o jogo não fôr encontrado
            if (jogo == null)
            {
                // ou há um erro,
                // ou há um alguem a tentar mexer no url
                //   return HttpNotFound();

                /// redireciono o utilzador para o ecrã incial
                return RedirectToAction("Index");
            }

            /// para o caso do utilizador alterar, de forma fraudulenta,
            /// os dados do Jogo, vamos guardá-los internamente
            /// Para isso, vou guardar o valor do ID do Jogo
            /// - guardar o ID do Jogo num cookie cifrado
            /// - guardar o ID numa var. de sessão 
           
            Session["IdJogo"] = jogo.Id;
            Session["Metodo"] = "Jogos/Delete";

            // envia para a View os dados do Jogo encontrado
            ViewBag.Plataformas = db.Plataformas;
            return View(jogo);
        }
   
    
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                // se entrei aqui,é porque é pq há um erro
                // nao se sabe o ID do jogo a remover
                return RedirectToAction("Index");
            }
            // avaliar se o ID do jogo que é fornecido
            // é o mesmo ID do jogo que foi apresentado no ecrã
            if (id != (int)Session["IdJogo"])
            {
                // há um ataque!
                // redirecionar para a página de Index
                return RedirectToAction("Index");
            }

            // avaliar se o método é o que é esperado
            string operacao = "Jogos/Delete";
            if (operacao != (string)Session["Metodo"])
            {
                // há um ataque!
                // redirecionar para a página de Index
                return RedirectToAction("Index");
            }
           // procura os dados do Jogo, na BD
            Jogos jogo = db.Jogos.Find(id);

            if (jogo == null)
            {
                // não foi possível encontrar o Jogo
                return RedirectToAction("Index");
            }

            try
            {
                db.Jogos.Remove(jogo);
                db.SaveChanges();
            }
            catch (Exception)
            {
                // captura a excessão e processa o código para resolver o problema
                // pode haver mais do que um 'catch' associado a um 'try'

                // enviar mensagem de erro para o utilizador
                ModelState.AddModelError("", "Ocorreu um erro com a eliminação do Jogo"
                                            + jogo.Nome +
                                            ". Provavelmente relacionado com o facto do " +
                                            "jogo ter sido comprado...");
                // devolver os dados do Jogo à View
                ViewBag.Plataformas = db.Plataformas;
                return View(jogo);
            }
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