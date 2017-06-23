using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AICOM.AcessoDados;
using AICOM.Models;
using System.Linq.Dynamic;
using AICOM.ViewModels;

namespace AICOM.Controllers
{
    public class LivrosController : Controller
    {
        private LivroContexto db = new LivroContexto();

        // GET: Livros
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar(ParametrosPaginacao parametrosPaginacao)
        {
            DadosFiltrados dadosFiltrados = FiltrarEPaginar(parametrosPaginacao);
            return Json(dadosFiltrados, JsonRequestBehavior.AllowGet);
        }

        //Metodo para filtrar dados e exibir tabela
        private DadosFiltrados FiltrarEPaginar(ParametrosPaginacao parametrosPaginacao)
        {
            var livros = db.Livros.Include(l => l.Genero);
            int total = livros.Count();
            if (!String.IsNullOrWhiteSpace(parametrosPaginacao.SearchPhrase))
            {
                int ano = 0;
                int.TryParse(parametrosPaginacao.SearchPhrase, out ano);
                decimal valor = 0;
                decimal.TryParse(parametrosPaginacao.SearchPhrase, out valor);

                livros = livros.Where("Titulo.Contains(@0) OR Autor.Contains(@0) OR AnoEdicao == (@1) OR Valor == (@2)", parametrosPaginacao.SearchPhrase, ano, valor);
            }

            //skip(5) - pular os primeiros(5) registros e Take(5) - mostrar os proximos (10) 
            var livrosPaginados = livros.OrderBy(parametrosPaginacao.CampoOrdenado).Skip((parametrosPaginacao.Current - 1) * parametrosPaginacao.RowCount).Take(parametrosPaginacao.RowCount);
            DadosFiltrados dadosFiltrados = new DadosFiltrados(parametrosPaginacao)
            {
                rows = livrosPaginados.ToList(),
                total = total
            };
            return dadosFiltrados;
        }
            

            
    
        /*
        if (!String.IsNullOrWhiteSpace(livro.Titulo))
        {
            livros = livros.Where(l => l.Titulo.Contains(livro.Titulo));
        }
        if (!String.IsNullOrWhiteSpace(livro.Autor))
        {
            livros = livros.Where(l => l.Titulo.Contains(livro.Titulo));
        }
        if (livro.AnoEdicao != 0)
        {
            livros = livros.Where(l => l.AnoEdicao == livro.AnoEdicao);
        }
        if (livro.Valor != decimal.Zero)
        {
            livros = livros.Where(l => l.Valor == livro.Valor);
        }*/


        /* Exemplo de forma mais trabalhosa para ordenar titulo
        if(campo== "Titulo")
        {
            if (ordenacao == "asc")
                livros.OrderBy(l=>l.Titulo);
        }
        else
        {
            livros.OrderByDescending(l => l.Titulo);
        }

        */
        //Take(5) - mostrar apenas 5 e skip(5) - pular os primeiros(5) registros


        /* Metodo anterior chamando action _Listar
        public PartialViewResult Listar(Livro livro, int pagina=1, int registros=5)
        {
            var livros = db.Livros.Include(l => l.Genero);

            if (!String.IsNullOrWhiteSpace(livro.Titulo))
            {
                livros = livros.Where(l => l.Titulo.Contains(livro.Titulo));
            }
            if (!String.IsNullOrWhiteSpace(livro.Autor))
            {
                livros = livros.Where(l => l.Titulo.Contains(livro.Titulo));
            }
            if (livro.AnoEdicao!=0)
            {
                livros = livros.Where(l => l.AnoEdicao == livro.AnoEdicao);
            }
            if (livro.Valor != decimal.Zero)
            {
                livros = livros.Where(l => l.Valor == livro.Valor);
            }
            //Take(5) - mostrar apenas 5 e skip(5) - pular os primeiros(5) registros

            var livrosPaginados = livros.OrderBy(l => l.Titulo).Skip((pagina-1)* registros).Take(registros);
            return PartialView("_Listar", livrosPaginados.ToList());
        }
        */

        // GET: Livros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return PartialView(livro);
        }

        // GET: Livros/Create
        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome");
            return PartialView();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Id,Titulo,AnoEdicao,Valor,Autor,GeneroId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return Json(new {resultado = true, mensagem ="Livro Cadastrado com Sucesso"});
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(itens => itens.Errors);
                return Json(new { resultado = false, mensagem = erros });
            }
        }

        // GET: Livros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "Id", "Nome", livro.GeneroId);
            return PartialView(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit([Bind(Include = "Id,Titulo,AnoEdicao,Valor,Autor,GeneroId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { resultado = true, mensagem = "Livro editado com Sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(itens => itens.Errors);
                return Json(new { resultado = false, mensagem = erros });
            }
        }

        // GET: Livros/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return PartialView(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                Livro livro = db.Livros.Find(id);
                db.Livros.Remove(livro);
                db.SaveChanges();
                return Json(new { resultado = true, mensagem = "Livro excluido com sucesso." });
            }
           catch(Exception e)
            {
                return Json(new { resultado=false, mensagem="O Livro não foi excluído. Erro:"+e.Message});
            }
            
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
