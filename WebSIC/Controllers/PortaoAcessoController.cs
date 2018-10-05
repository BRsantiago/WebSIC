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
    public class PortaoAcessoController : Controller
    {
        private WebSICContext db = new WebSICContext();

        // GET: PortaoAcesso
        public ActionResult Index()
        {
            return View(db.PortoesAcesso.ToList());
        }

        // GET: PortaoAcesso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortaoAcesso portaoAcesso = db.PortoesAcesso.Find(id);
            if (portaoAcesso == null)
            {
                return HttpNotFound();
            }
            return View(portaoAcesso);
        }

        // GET: PortaoAcesso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortaoAcesso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPortaoAcesso,Sigla,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] PortaoAcesso portaoAcesso)
        {
            if (ModelState.IsValid)
            {
                db.PortoesAcesso.Add(portaoAcesso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portaoAcesso);
        }

        // GET: PortaoAcesso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortaoAcesso portaoAcesso = db.PortoesAcesso.Find(id);
            if (portaoAcesso == null)
            {
                return HttpNotFound();
            }
            return View(portaoAcesso);
        }

        // POST: PortaoAcesso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPortaoAcesso,Sigla,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] PortaoAcesso portaoAcesso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portaoAcesso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portaoAcesso);
        }

        // GET: PortaoAcesso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortaoAcesso portaoAcesso = db.PortoesAcesso.Find(id);
            if (portaoAcesso == null)
            {
                return HttpNotFound();
            }
            return View(portaoAcesso);
        }

        // POST: PortaoAcesso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PortaoAcesso portaoAcesso = db.PortoesAcesso.Find(id);
            db.PortoesAcesso.Remove(portaoAcesso);
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
