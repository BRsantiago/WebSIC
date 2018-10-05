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
    public class OcorrenciaController : Controller
    {
        private WebSICContext db = new WebSICContext();

        // GET: Ocorrencia
        public ActionResult Index()
        {
            return View(db.Ocorrencias.ToList());
        }

        // GET: Ocorrencia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }

        // GET: Ocorrencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ocorrencia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOcorrencia,Historico,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                db.Ocorrencias.Add(ocorrencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ocorrencia);
        }

        // GET: Ocorrencia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }

        // POST: Ocorrencia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOcorrencia,Historico,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ocorrencia);
        }

        // GET: Ocorrencia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }

        // POST: Ocorrencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            db.Ocorrencias.Remove(ocorrencia);
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
