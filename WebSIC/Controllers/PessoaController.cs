using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity.Entities;
using Repository.Context;

namespace WebSIC.Controllers
{
    public class PessoaController : Controller
    {
        private WebSICContext db = new WebSICContext();

        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoas = db.Pessoas.Include(p => p.Usuario);
            return View(pessoas.ToList());
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            ViewBag.IdPessoa = new SelectList(db.Usuarios, "IdUsuario", "Login");
            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPessoa,Nome,Apelido,DataNascimento,NomePai,NomeMae,Endereco,Numero,Complemento,Bairro,Cidade,UF,CEP,TelefoneEmergencia,TelefoneResidencial,TelefoneCelular,RNE,CPF,RG,OrgaoExpeditor,UFOrgaoExpeditor,Genero,Observacao,FlgCVE,Email,CNH,CategoriaCNH,DataValidadeCNH,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Pessoas.Add(pessoa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPessoa = new SelectList(db.Usuarios, "IdUsuario", "Login", pessoa.IdPessoa);
            return View(pessoa);
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPessoa = new SelectList(db.Usuarios, "IdUsuario", "Login", pessoa.IdPessoa);
            return View(pessoa);
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPessoa,Nome,Apelido,DataNascimento,NomePai,NomeMae,Endereco,Numero,Complemento,Bairro,Cidade,UF,CEP,TelefoneEmergencia,TelefoneResidencial,TelefoneCelular,RNE,CPF,RG,OrgaoExpeditor,UFOrgaoExpeditor,Genero,Observacao,FlgCVE,Email,CNH,CategoriaCNH,DataValidadeCNH,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPessoa = new SelectList(db.Usuarios, "IdUsuario", "Login", pessoa.IdPessoa);
            return View(pessoa);
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoa);
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
