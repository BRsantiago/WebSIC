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
    public class ApoliceController : Controller
    {
        private IApoliceService Service;
        private IEmpresaService EmpresaService;

        public ApoliceController(IApoliceService service, IEmpresaService empresaService)
        {
            Service = service;
            EmpresaService = empresaService;
        }

        // GET: Apolice
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: Apolice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apolice apolice = Service.Obter(id.Value);
            if (apolice == null)
            {
                return HttpNotFound();
            }
            return PartialView(apolice);
        }

        // GET: Apolice/Create
        public ActionResult Create()
        {
            ViewBag.Empresas = new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia");
            return PartialView();
        }

        // POST: Apolice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdApolice,Numero,DataValidade,Observacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Apolice apolice, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                apolice.Criador =
                  apolice.Atualizador = User.Identity.Name;
                apolice.Empresa = EmpresaService.ObterPorId(int.Parse(form["Empresa.IdEmpresa"]));
                var check = Service.Incluir(apolice);
                if (check != null)
                    return RedirectToAction("Index");
            }

            return PartialView(apolice);
        }

        // GET: Apolice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apolice apolice = Service.Obter(id.Value);
            ViewBag.Empresas = new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia", apolice.Empresa.IdEmpresa);
            if (apolice == null)
            {
                return HttpNotFound();
            }
            return PartialView(apolice);
        }

        // POST: Apolice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdApolice,Numero,DataValidade,Observacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Apolice apolice, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                apolice.Atualizacao = DateTime.Now;
                apolice.Atualizador = User.Identity.Name;
                apolice.Empresa = EmpresaService.ObterPorId(int.Parse(form["Empresa.IdEmpresa"]));
                var check = Service.Atualizar(apolice);
                if (check != null)
                    return RedirectToAction("Index");
            }
            return PartialView(apolice);
        }

        // GET: Apolice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apolice apolice = Service.Obter(id.Value);
            if (apolice == null)
            {
                return HttpNotFound();
            }
            return PartialView(apolice);
        }

        // POST: Apolice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var check = Service.Excluir(id);
            if (check != 0)
                return RedirectToAction("Index");

            return PartialView(id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
