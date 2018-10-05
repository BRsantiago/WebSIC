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
    public class EmpresaController : Controller
    {
        public IEmpresaService EmpresaService;

        public EmpresaController(IEmpresaService _EmpresaService)
        {
            EmpresaService = _EmpresaService;
        }

        // GET: Empresa
        public ActionResult Index()
        {
            List<Empresa> empresas = EmpresaService.ObterTodos();

            return View(empresas);
        }

        // GET: Empresa/Details/5
        public ActionResult Details(int? id)
        {
            Empresa empresa = EmpresaService.ObterPorId(id.Value);
            return PartialView(empresa);
        }

        // GET: Empresa/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Empresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmpresa,RazaoSocial,NomeFantasia,Endereco,Complemento,Numero,Bairro,Cidade,UF,CGC,Telefone,TipoCobranca,Observacao,CEP,NumeroContrato,Email,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                EmpresaService.IncluirNovaEmpresa(empresa);
                return RedirectToAction("Index");
            }

            return PartialView(empresa);
        }

        // GET: Empresa/Edit/5
        public ActionResult Edit(int? id)
        {
            Empresa empresa = EmpresaService.ObterPorId(id.Value);
            return PartialView(empresa);
        }

        // POST: Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEmpresa,RazaoSocial,NomeFantasia,Endereco,Complemento,Numero,Bairro,Cidade,UF,CGC,Telefone,TipoCobranca,Observacao,CEP,NumeroContrato,Email,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                EmpresaService.AtualizarNovaEmpresa(empresa);
                return RedirectToAction("Index");
            }
            return PartialView(empresa);
        }

        // GET: Empresa/Delete/5
        public ActionResult Delete(int? id)
        {
            Empresa empresa = EmpresaService.ObterPorId(id.Value);
            return PartialView(empresa);
        }

        // POST: Empresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            EmpresaService.ExcluirEmpresa(id.Value);
            return RedirectToAction("Index");
        }
    }
}
