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
    public class TipoSolicitacaoController : Controller
    {
        private WebSICContext db = new WebSICContext();

        // GET: TipoSolicitacao
        public ActionResult Index()
        {
            return View(db.TiposSolicitacao.ToList());
        }

        // GET: TipoSolicitacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitacao tipoSolicitacao = db.TiposSolicitacao.Find(id);
            if (tipoSolicitacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoSolicitacao);
        }

        // GET: TipoSolicitacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSolicitacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoSolicitacao,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoSolicitacao tipoSolicitacao)
        {
            if (ModelState.IsValid)
            {
                db.TiposSolicitacao.Add(tipoSolicitacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoSolicitacao);
        }

        // GET: TipoSolicitacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitacao tipoSolicitacao = db.TiposSolicitacao.Find(id);
            if (tipoSolicitacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoSolicitacao);
        }

        // POST: TipoSolicitacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoSolicitacao,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoSolicitacao tipoSolicitacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoSolicitacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoSolicitacao);
        }

        // GET: TipoSolicitacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitacao tipoSolicitacao = db.TiposSolicitacao.Find(id);
            if (tipoSolicitacao == null)
            {
                return HttpNotFound();
            }
            return View(tipoSolicitacao);
        }

        // POST: TipoSolicitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoSolicitacao tipoSolicitacao = db.TiposSolicitacao.Find(id);
            db.TiposSolicitacao.Remove(tipoSolicitacao);
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
