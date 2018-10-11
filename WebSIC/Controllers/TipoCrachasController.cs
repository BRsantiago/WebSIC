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
    public class TipoCrachasController : Controller
    {
        private ITipoCrachaService Service;

        public TipoCrachasController(ITipoCrachaService service)
        {
            Service = service;
        }

        // GET: TipoCrachas
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: TipoCrachas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCracha tipoCracha = Service.Obter(id.Value);
            if (tipoCracha == null)
            {
                return HttpNotFound();
            }
            return View(tipoCracha);
        }

        // GET: TipoCrachas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCrachas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoCracha,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoCracha tipoCracha)
        {
            if (ModelState.IsValid)
            {
                tipoCracha.Criador =
                    tipoCracha.Atualizador = User.Identity.Name;
                var check = Service.Incluir(tipoCracha);
                if (check != null)
                    return RedirectToAction("Index");
            }

            return View(tipoCracha);
        }

        // GET: TipoCrachas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCracha tipoCracha = Service.Obter(id.Value);
            if (tipoCracha == null)
            {
                return HttpNotFound();
            }
            return View(tipoCracha);
        }

        // POST: TipoCrachas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoCracha,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoCracha tipoCracha)
        {
            if (ModelState.IsValid)
            {
                tipoCracha.Atualizacao = DateTime.Now;
                tipoCracha.Atualizador = User.Identity.Name;
                var check = Service.Atualizar(tipoCracha);
                if (check != null)
                    return RedirectToAction("Index");
            }
            return View(tipoCracha);
        }

        // GET: TipoCrachas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCracha tipoCracha = Service.Obter(id.Value);
            if (tipoCracha == null)
            {
                return HttpNotFound();
            }
            return View(tipoCracha);
        }

        // POST: TipoCrachas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var check = Service.Excluir(id);
            if (check != 0)
                return RedirectToAction("Index");

            return View(id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
