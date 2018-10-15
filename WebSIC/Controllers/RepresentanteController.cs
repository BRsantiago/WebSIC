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
using WebSIC.Models;

namespace WebSIC.Controllers
{
    public class RepresentanteController : Controller
    {
        private WebSICContext db = new WebSICContext();
        public IPessoaService PessoaService;

        public RepresentanteController(IPessoaService _PessoaService)
        {
            PessoaService = _PessoaService;
        }

        // GET: Representante
        public ActionResult Index()
        {
            var pessoas = db.Pessoas.Include(p => p.Usuario);
            return View(pessoas.ToList());
        }

        // GET: Representante/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Representante/Create
        public ActionResult Create(string idEmpresa)
        {
            RepresentanteViewModel vm = new RepresentanteViewModel();
            vm.IdEmpresa = idEmpresa;
            return PartialView(vm);
        }

        // POST: Representante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RepresentanteViewModel representante)
        {
            try
            {
                PessoaService.IncluirNovoRepresentante(representante.MapearParaObjetoDominio());
                return Json(new { success = true, title = "Sucesso", message = "Representante cadastrado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            //ViewBag.IdPessoa = new SelectList(db.Usuarios, "IdUsuario", "Login", pessoa.IdPessoa);
            //return View(pessoa);
        }

        // GET: Representante/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = PessoaService.ObterPorId(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPessoa = new SelectList(db.Usuarios, "IdUsuario", "Login", pessoa.IdPessoa);
            return PartialView(pessoa);
        }

        // POST: Representante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPessoa,Nome,Apelido,DataNascimento,NomePai,NomeMae,Endereco,Numero,Complemento,Bairro,Cidade,UF,CEP,TelefoneEmergencia,TelefoneResidencial,TelefoneCelular,RNE,CPF,RG,OrgaoExpeditor,UFOrgaoExpeditor,Genero,Observacao,FlgCVE,Email,CNH,CategoriaCNH,DataValidadeCNH,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPessoa = new SelectList(db.Usuarios, "IdUsuario", "Login", pessoa.IdPessoa);
            return View(pessoa);
        }

        // GET: Representante/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Representante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pessoa pessoa = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
