﻿using System;
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
    public class VeiculoController : Controller
    {
        private IVeiculoService Service;
        private IEmpresaService EmpresaService;
        private IApoliceService ApoliceService;

        public VeiculoController(IVeiculoService service, IEmpresaService empresaService, IApoliceService apoliceService)
        {
            Service = service;
            EmpresaService = empresaService;
            ApoliceService = apoliceService;
        }

        // GET: Veiculo
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: Veiculo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = Service.Obter(id.Value);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return PartialView(veiculo);
        }

        // GET: Veiculo/Create
        public ActionResult Create()
        {
            ViewBag.Empresas = new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia");
            return PartialView();
        }

        // POST: Veiculo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVeiculo,Descricao,Placa,Ano,Cor,Observacao,Chassi,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                veiculo.Criador =
                    veiculo.Atualizador = User.Identity.Name;
                var check = Service.Incluir(veiculo);
                if (check != null)
                    return RedirectToAction("Index");
            }

            return PartialView(veiculo);
        }

        // GET: Veiculo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = Service.Obter(id.Value);
            ViewBag.Empresas = new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia", veiculo.Empresa.IdEmpresa);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return PartialView(veiculo);
        }

        // POST: Veiculo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdVeiculo,Descricao,Placa,Ano,Cor,Observacao,Chassi,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                veiculo.Atualizacao = DateTime.Now;
                veiculo.Atualizador = User.Identity.Name;
                var check = Service.Atualizar(veiculo);
                if (check != null)
                    return RedirectToAction("Index");
            }
            return PartialView(veiculo);
        }

        // GET: Veiculo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veiculo veiculo = Service.Obter(id.Value);
            if (veiculo == null)
            {
                return HttpNotFound();
            }
            return PartialView(veiculo);
        }

        // POST: Veiculo/Delete/5
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
