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
    public class CargoController : Controller
    {
        private ICargoService Service;

        public CargoController(ICargoService service)
        {
            Service = service;
        }

        // GET: Cargo
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: Cargo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = Service.Obter(id.Value);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return PartialView(cargo);
        }

        // GET: Cargo/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Cargo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCargo,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                cargo.Criador =
                    cargo.Atualizador = User.Identity.Name;
                var check = Service.Incluir(cargo);

                return Json(check, JsonRequestBehavior.AllowGet);
            }

            return PartialView(cargo);
        }

        // GET: Cargo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = Service.Obter(id.Value);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return PartialView(cargo);
        }

        // POST: Cargo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCargo,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                cargo.Atualizacao = DateTime.Now;
                cargo.Atualizador = User.Identity.Name;
                var check = Service.Atualizar(cargo);

                return Json(check, JsonRequestBehavior.AllowGet);
            }
            return PartialView(cargo);
        }

        // GET: Cargo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = Service.Obter(id.Value);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return PartialView(cargo);
        }

        // POST: Cargo/Delete/5
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
