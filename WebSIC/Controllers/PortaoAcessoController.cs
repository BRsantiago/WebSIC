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
    public class PortaoAcessoController : Controller
    {
        private IPortaoAcessoService Service;

        public PortaoAcessoController(IPortaoAcessoService service)
        {
            Service = service;
        }

        // GET: PortaoAcesso
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: PortaoAcesso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortaoAcesso portaoAcesso = Service.Obter(id.Value);
            if (portaoAcesso == null)
            {
                return HttpNotFound();
            }
            return PartialView(portaoAcesso);
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
                portaoAcesso.Criador =
                    portaoAcesso.Atualizador = User.Identity.Name;
                var check = Service.Incluir(portaoAcesso);

                return Json(check, JsonRequestBehavior.AllowGet);
            }

            return PartialView(portaoAcesso);
        }

        // GET: PortaoAcesso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortaoAcesso portaoAcesso = Service.Obter(id.Value);
            if (portaoAcesso == null)
            {
                return HttpNotFound();
            }
            return PartialView(portaoAcesso);
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
                portaoAcesso.Atualizacao = DateTime.Now;
                portaoAcesso.Atualizador = User.Identity.Name;
                var check = Service.Atualizar(portaoAcesso);

                return Json(check, JsonRequestBehavior.AllowGet);
            }
            return PartialView(portaoAcesso);
        }

        // GET: PortaoAcesso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PortaoAcesso portaoAcesso = Service.Obter(id.Value);
            if (portaoAcesso == null)
            {
                return HttpNotFound();
            }
            return PartialView(portaoAcesso);
        }

        // POST: PortaoAcesso/Delete/5
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
