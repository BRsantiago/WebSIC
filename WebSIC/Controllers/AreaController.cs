using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity.Entities;
using Newtonsoft.Json;
using Repository.Context;
using Service.Interface;

namespace WebSIC.Controllers
{
    //[AllowAnonymous]
    public class AreaController : Controller
    {
        private IAreaService Service;

        public AreaController(IAreaService service)
        {
            Service = service;
        }

        // GET: Areas
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = Service.Obter(id.Value); 
            if (area == null)
            {
                return HttpNotFound();
            }
            return PartialView(area);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdArea,Sigla,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Entity.Entities.Area area)
        {
            if (ModelState.IsValid)
            {
                area.Criador = 
                    area.Atualizador = User.Identity.Name;
                var check = Service.Incluir(area);

                return Json(check, JsonRequestBehavior.AllowGet);
            }

            return PartialView(area);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = Service.Obter(id.Value);
            if (area == null)
            {
                return HttpNotFound();
            }
            return PartialView(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdArea,Sigla,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Area area)
        {
            if (ModelState.IsValid)
            {
                area.Atualizacao = DateTime.Now;
                area.Atualizador = User.Identity.Name;
                var check = Service.Atualizar(area);

                return Json(check, JsonRequestBehavior.AllowGet);
            }
            return PartialView(area);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Area area = Service.Obter(id.Value);
            if (area == null)
            {
                return HttpNotFound();
            }
            return PartialView(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var check = Service.Excluir(id);
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
