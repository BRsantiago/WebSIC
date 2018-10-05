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
    public class SolicitacaoController : Controller
    {
        private WebSICContext db = new WebSICContext();

        // GET: Solicitacao
        public ActionResult Index()
        {
            var solicitacoes = db.Solicitacoes.Include(s => s.Schedule);
            return View(solicitacoes.ToList());
        }

        // GET: Solicitacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = db.Solicitacoes.Find(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // GET: Solicitacao/Create
        public ActionResult Create()
        {
            ViewBag.IdSolicitacao = new SelectList(db.Schedules, "IdSchedule", "Descricao");
            return View();
        }

        // POST: Solicitacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSolicitacao,FlgMotorista,FlgTemporario,DataAutorizacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                db.Solicitacoes.Add(solicitacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSolicitacao = new SelectList(db.Schedules, "IdSchedule", "Descricao", solicitacao.IdSolicitacao);
            return View(solicitacao);
        }

        // GET: Solicitacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = db.Solicitacoes.Find(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSolicitacao = new SelectList(db.Schedules, "IdSchedule", "Descricao", solicitacao.IdSolicitacao);
            return View(solicitacao);
        }

        // POST: Solicitacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSolicitacao,FlgMotorista,FlgTemporario,DataAutorizacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Solicitacao solicitacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSolicitacao = new SelectList(db.Schedules, "IdSchedule", "Descricao", solicitacao.IdSolicitacao);
            return View(solicitacao);
        }

        // GET: Solicitacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = db.Solicitacoes.Find(id);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return View(solicitacao);
        }

        // POST: Solicitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitacao solicitacao = db.Solicitacoes.Find(id);
            db.Solicitacoes.Remove(solicitacao);
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
