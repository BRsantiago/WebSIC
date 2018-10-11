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
    public class TipoSolicitacaoController : Controller
    {
        private ITipoSolicitacaoService Service;

        public TipoSolicitacaoController(ITipoSolicitacaoService service)
        {
            Service = service;
        }

        // GET: TipoSolicitacao
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: TipoSolicitacao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitacao tipoSolicitacao = Service.Obter(id.Value);
            if (tipoSolicitacao == null)
            {
                return HttpNotFound();
            }
            return PartialView(tipoSolicitacao);
        }

        // GET: TipoSolicitacao/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: TipoSolicitacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoSolicitacao,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoSolicitacao tipoSolicitacao)
        {
            if (ModelState.IsValid)
            {
                tipoSolicitacao.Criador =
                    tipoSolicitacao.Atualizador = User.Identity.Name;
                var check = Service.Incluir(tipoSolicitacao);
                if (check != null)
                    return RedirectToAction("Index");
            }

            return PartialView(tipoSolicitacao);
        }

        // GET: TipoSolicitacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitacao tipoSolicitacao = Service.Obter(id.Value);
            if (tipoSolicitacao == null)
            {
                return HttpNotFound();
            }
            return PartialView(tipoSolicitacao);
        }

        // POST: TipoSolicitacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoSolicitacao,Descricao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] TipoSolicitacao tipoSolicitacao)
        {
            if (ModelState.IsValid)
            {
                tipoSolicitacao.Atualizacao = DateTime.Now;
                tipoSolicitacao.Atualizador = User.Identity.Name;
                var check = Service.Atualizar(tipoSolicitacao);
                if (check != null)
                    return RedirectToAction("Index");
            }
            return PartialView(tipoSolicitacao);
        }

        // GET: TipoSolicitacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitacao tipoSolicitacao = Service.Obter(id.Value);
            if (tipoSolicitacao == null)
            {
                return HttpNotFound();
            }
            return PartialView(tipoSolicitacao);
        }

        // POST: TipoSolicitacao/Delete/5
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
