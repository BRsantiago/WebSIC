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
using Service.Interface;

namespace WebSIC.Controllers
{
    public class AeroportoController : Controller
    {
        private IAeroportoService Service;

        public AeroportoController(IAeroportoService _Service)
        {
            Service = _Service;
        }

        // GET: Aeroporto
        public ActionResult Index()
        {
            return View(new List<Aeroporto>());
        }

        // GET: Aeroporto/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Aeroporto aeroporto = db.Aeroportos.Find(id);
            //if (aeroporto == null)
            //{
            //    return HttpNotFound();
            //}
            return View(new Aeroporto());
        }

        // GET: Aeroporto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aeroporto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAeroporto,Descricao,IATA,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Aeroporto aeroporto)
        {
            if (ModelState.IsValid)
            {
                //db.Aeroportos.Add(aeroporto);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aeroporto);
        }

        // GET: Aeroporto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Aeroporto aeroporto = db.Aeroportos.Find(id);
            //if (aeroporto == null)
            //{
            //    return HttpNotFound();
            //}
            return View(new Aeroporto());
        }

        // POST: Aeroporto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAeroporto,Descricao,IATA,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Aeroporto aeroporto)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(aeroporto).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aeroporto);
        }

        // GET: Aeroporto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Aeroporto aeroporto = db.Aeroportos.Find(id);
            //if (aeroporto == null)
            //{
            //    return HttpNotFound();
            //}
            return View(new Aeroporto());
        }

        // POST: Aeroporto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Aeroporto aeroporto = db.Aeroportos.Find(id);
            //db.Aeroportos.Remove(aeroporto);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
