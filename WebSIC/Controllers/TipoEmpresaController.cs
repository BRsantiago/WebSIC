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
    public class TipoEmpresaController : Controller
    {
        private WebSICContext db = new WebSICContext();

        // GET: TipoEmpresa
        public ActionResult Index()
        {
            return View(db.TipoEmpresas.ToList());
        }

        // GET: TipoEmpresa/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEmpresa tipoEmpresa = db.TipoEmpresas.Find(id);
            if (tipoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(tipoEmpresa);
        }

        // GET: TipoEmpresa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEmpresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoEmpresa,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoEmpresa tipoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.TipoEmpresas.Add(tipoEmpresa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoEmpresa);
        }

        // GET: TipoEmpresa/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEmpresa tipoEmpresa = db.TipoEmpresas.Find(id);
            if (tipoEmpresa == null)
            {
                return HttpNotFound();
            }
            return PartialView(tipoEmpresa);
        }

        // POST: TipoEmpresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoEmpresa,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoEmpresa tipoEmpresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoEmpresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoEmpresa);
        }

        // GET: TipoEmpresa/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoEmpresa tipoEmpresa = db.TipoEmpresas.Find(id);
            if (tipoEmpresa == null)
            {
                return HttpNotFound();
            }
            return View(tipoEmpresa);
        }

        // POST: TipoEmpresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoEmpresa tipoEmpresa = db.TipoEmpresas.Find(id);
            db.TipoEmpresas.Remove(tipoEmpresa);
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
