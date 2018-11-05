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
    public class CredencialController : Controller
    {
        private WebSICContext db = new WebSICContext();

        public ICredencialService CredencialService;

        public CredencialController(ICredencialService _CredencialService)
        {
            CredencialService = _CredencialService;
        }

        // GET: Credencial
        public ActionResult Index()
        {
            List<Credencial> lista = this.CredencialService.ObterTodos();

            return View(lista);
        }

        // GET: Credencial/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(id));
            if (credencial == null)
            {
                return HttpNotFound();
            }
            return View(credencial);
        }

        // GET: Credencial/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Credencial/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdCredencial,Matricula,FlgMotorista,FlgTemporario,FlgCVE,DataExpedicao,DataDesativacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Credencial credencial)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Credenciais.Add(credencial);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(credencial);
        //}

        // GET: Credencial/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(id));
            if (credencial == null)
            {
                return HttpNotFound();
            }
            return View(credencial);
        }

        // POST: Credencial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCredencial,Matricula,FlgMotorista,FlgTemporario,FlgCVE,DataExpedicao,DataDesativacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Credencial credencial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credencial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(credencial);
        }

        // GET: Credencial/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Credencial credencial = db.Credenciais.Find(id);
        //    if (credencial == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(credencial);
        //}

        //// POST: Credencial/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    Credencial credencial = db.Credenciais.Find(id);
        //    db.Credenciais.Remove(credencial);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

    }
}
