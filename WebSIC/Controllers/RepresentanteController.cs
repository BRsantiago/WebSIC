using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity.DTO;
using Entity.Entities;
using Repository.Context;
using Service.Interface;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    [AllowAnonymous]
    public class RepresentanteController : Controller
    {
        public IPessoaService PessoaService;

        public RepresentanteController(IPessoaService _PessoaService)
        {
            PessoaService = _PessoaService;
        }

        // GET: Representante
        public ActionResult Index()
        {
            List<Pessoa> pessoas = PessoaService.ObterTodos();
            return View(pessoas);
        }

        // GET: Representante/Details/5
        public ActionResult Details(string id)
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
            return View(pessoa);
        }

        // GET: Representante/Create
        public ActionResult Create(string id)
        {
            RepresentanteViewModel model = new RepresentanteViewModel();
            model.IdEmpresa = id;

            return PartialView(model);
        }

        // POST: Representante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RepresentanteViewModel representante)
        {
            try
            {
                var check = PessoaService.IncluirNovoRepresentante(representante.MapearParaObjetoDominio());
                return Json(new { success = true, title = "Sucesso", message = "Representante cadastrado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Representante/Edit/5
        public ActionResult Edit(string id)
        {
            Pessoa pessoa = PessoaService.ObterPorId(id);
            return PartialView(new RepresentanteViewModel(pessoa));
        }

        // POST: Representante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RepresentanteViewModel representante)
        {
            try
            {
                PessoaService.Atualizar(representante.MapearParaObjetoDominio());
                return Json(new { success = true, title = "Sucesso", message = "Representante cadastrado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Representante/Delete/5
        public ActionResult Delete(string idPessoa, string IdEmpresa)
        {
            Pessoa pessoa = PessoaService.ObterPorId(idPessoa);
            RepresentanteViewModel vm = new RepresentanteViewModel(pessoa);
            vm.IdEmpresa = IdEmpresa;
            return PartialView(vm);
        }

        // POST: Representante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(RepresentanteViewModel vm)
        {
            try
            {
                Pessoa representante = PessoaService.ObterPorId(vm.IdPessoa);
                PessoaService.ExcluirRepresentante(representante, Convert.ToInt32(vm.IdEmpresa));

                return Json(new { success = true, title = "Sucesso", message = "Representante excluído com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
