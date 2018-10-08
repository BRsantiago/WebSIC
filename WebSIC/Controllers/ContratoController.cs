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
    public class ContratoController : Controller
    {
        private IContratoService Service;
        private IEmpresaService EmpresaService;

        public ContratoController(IContratoService service, IEmpresaService empresaService)
        {
            Service = service;
            EmpresaService = empresaService;
        }

        // GET: Contrato
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: Contrato/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = Service.Obter(id.Value);;
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // GET: Contrato/Create
        public ActionResult Create()
        {
            ViewBag.Empresas = new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia");
            return View();
        }

        // POST: Contrato/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdContrato,Numero,InicioVigencia,FimVigencia,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Contrato contrato, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                contrato.Criador =
                    contrato.Atualizador = User.Identity.Name;
                contrato.Empresa = EmpresaService.ObterPorId(int.Parse(form["Empresa.IdEmpresa"]));
                var check = Service.Incluir(contrato);
                if (check != null)
                    return RedirectToAction("Index");
            }

            return View(contrato);
        }

        // GET: Contrato/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = Service.Obter(id.Value);
            ViewBag.Empresas = new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia", contrato.Empresa.IdEmpresa);
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // POST: Contrato/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdContrato,Numero,InicioVigencia,FimVigencia,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Contrato contrato, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                contrato.Atualizacao = DateTime.Now;
                contrato.Atualizador = User.Identity.Name;
                contrato.Empresa = EmpresaService.ObterPorId(int.Parse(form["Empresa.IdEmpresa"]));
                var check = Service.Atualizar(contrato);
                if (check != null)
                    return RedirectToAction("Index");
            }
            return View(contrato);
        }

        // GET: Contrato/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = Service.Obter(id.Value);;
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return View(contrato);
        }

        // POST: Contrato/Delete/5
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
