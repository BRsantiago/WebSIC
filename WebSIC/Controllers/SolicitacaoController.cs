using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Entity.DTO;
using Entity.Entities;
using Repository.Context;
using Service.Interface;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    public class SolicitacaoController : Controller
    {

        public IAeroportoService AeroportoService;
        public IEmpresaService EmpresaService;
        public IContratoService ContratoService;
        public ITipoSolicitacaoService TipoSolicitacaoService;
        public ISolicitacaoService SolicitacaoService;
        public IPessoaService PessoaService;

        public SolicitacaoController(IAeroportoService _AeroportoService,
                                     IEmpresaService _EmpresaService,
                                     IContratoService _ContratoService,
                                     ITipoSolicitacaoService _TipoSolicitacaoService,
                                     ISolicitacaoService _SolicitacaoService,
                                     IPessoaService _PessoaService)
        {
            AeroportoService = _AeroportoService;
            EmpresaService = _EmpresaService;
            ContratoService = _ContratoService;
            TipoSolicitacaoService = _TipoSolicitacaoService;
            SolicitacaoService = _SolicitacaoService;
            PessoaService = _PessoaService;
        }


        // GET: Solicitacao
        public ActionResult Index()
        {
            List<Solicitacao> solicitacoes = SolicitacaoService.ObterTodos();
            return View(solicitacoes);
        }

        // GET: Solicitacao/Details/5
        public ActionResult Details(int? id)
        {
            return View(new Solicitacao());
        }

        // GET: Solicitacao/Create
        public ActionResult Create()
        {
            SolicitacaoViewModel model = new SolicitacaoViewModel();

            List<TipoCredencial> TipoCredencialLista = new List<TipoCredencial>();
            TipoCredencialLista.Add(new TipoCredencial() { IdTipoCredencial = 0, Descricao = "Normal" });
            TipoCredencialLista.Add(new TipoCredencial() { IdTipoCredencial = 1, Descricao = "ATIV" });

            List<TipoEmissao> TipoEmissaoLista = new List<TipoEmissao>();
            TipoEmissaoLista.Add(new TipoEmissao() { IdTipoEmissao = 0, Descricao = "Temporário" });
            TipoEmissaoLista.Add(new TipoEmissao() { IdTipoEmissao = 1, Descricao = "Definitivo" });

            List<Genero> GeneroLista = new List<Genero>();
            GeneroLista.Add(new Genero() { IdGenero = 0, Descricao = "Masculino" });
            GeneroLista.Add(new Genero() { IdGenero = 1, Descricao = "Feminino" });

            model.Aeroportos = AeroportoService.ObterTodos();
            model.Empresas = EmpresaService.ObterTodos();
            model.Contratos = ContratoService.ObterTodos();
            model.TiposSolicitacao = TipoSolicitacaoService.ObterTodos();
            model.Generos = GeneroLista;
            model.TiposCredencial = TipoCredencialLista;
            model.TiposEmissao = TipoEmissaoLista;


            return View(model);
        }

        // POST: Solicitacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSolicitacao,FlgMotorista,FlgTemporario,DataAutorizacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Solicitacao solicitacao)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Solicitacoes.Add(solicitacao);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.IdSolicitacao = new SelectList(db.Schedules, "IdSchedule", "Descricao", solicitacao.IdSolicitacao);
            return View(new Solicitacao());
        }

        // GET: Solicitacao/Edit/5
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Solicitacao solicitacao = db.Solicitacoes.Find(id);
            //if (solicitacao == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.IdSolicitacao = new SelectList(db.Schedules, "IdSchedule", "Descricao", solicitacao.IdSolicitacao);
            return View(new Solicitacao());
        }

        // POST: Solicitacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSolicitacao,FlgMotorista,FlgTemporario,DataAutorizacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Solicitacao solicitacao)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(solicitacao).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.IdSolicitacao = new SelectList(db.Schedules, "IdSchedule", "Descricao", solicitacao.IdSolicitacao);
            return View(new Solicitacao());
        }

        // GET: Solicitacao/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Solicitacao solicitacao = db.Solicitacoes.Find(id);
            //if (solicitacao == null)
            //{
            //    return HttpNotFound();
            //}
            return View(new Solicitacao());
        }

        // POST: Solicitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Solicitacao solicitacao = db.Solicitacoes.Find(id);
            //db.Solicitacoes.Remove(solicitacao);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}
