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
    [AllowAnonymous]
    public class ContratoController : Controller
    {
        private IContratoService Service;
        private IEmpresaService EmpresaService;

        public ContratoController(IContratoService service, IEmpresaService empresaService)
        {
            Service = service;
            EmpresaService = empresaService;
        }

        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contrato contrato = Service.Obter(id.Value); ;
            if (contrato == null)
            {
                return HttpNotFound();
            }
            return PartialView(contrato);
        }

        public ActionResult Create(int id)
        {
            ContratoViewModel vm = new ContratoViewModel();
            vm.IdEmpresa = id;
            return PartialView(vm);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ContratoViewModel model)
        {
            try
            {
                Contrato contrato = model.MapearParaObjetoDominio();

                contrato.Criador =
                    contrato.Atualizador = User.Identity.Name;

                Service.Incluir(contrato);

                return Json(new { success = true, title = "Sucesso", message = "Contrato cadastrado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Edit(int? id)
        {
            Contrato contrato = Service.Obter(id.Value);
            return PartialView(new ContratoViewModel(contrato));
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ContratoViewModel model)
        {
            try
            {
                Contrato contrato = model.MapearParaObjetoDominio();
                contrato.Atualizador = User.Identity.Name;

                Service.Atualizar(contrato);

                return Json(new { success = true, title = "Sucesso", message = "Contrato cadastrado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int? id)
        {
            Contrato contrato = Service.Obter(id.Value); ;
            return PartialView(new ContratoViewModel(contrato));
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ContratoViewModel contrato)
        {
            try
            {
                Service.Excluir(contrato.IdContrato);
                return Json(new { success = true, title = "Sucesso", message = "Representante excluído com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
