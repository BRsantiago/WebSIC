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
            ViewBag.Apolices = new List<SelectListItem>();
            return PartialView();
        }

        public ActionResult GetApolices(int idEmpresa)
        {
            var apoliceItems = ApoliceService.ObterValidas(idEmpresa, false)
                .Select(a => new SelectListItem() { Text = a.Numero, Value = a.IdApolice.ToString() });
            return Json(apoliceItems, JsonRequestBehavior.AllowGet);
        }

        // POST: Veiculo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdVeiculo,Marca,Modelo,AnoFabricacao,AnoModelo,Cor,Placa,Chassi,Observacao,Criacao,Criador,Atualizacao,Atualizador,Ativo,TipoServico,Categoria,AcessoManobra,EmpresaId,ApoliceId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                veiculo.Criador =
                    veiculo.Atualizador = User.Identity.Name;

                veiculo.Empresa = EmpresaService.ObterPorId(veiculo.EmpresaId.Value);
                veiculo.Apolice = (veiculo.Empresa.Apolices != null && veiculo.Empresa.Apolices.Any(a => a.IdApolice == veiculo.ApoliceId.Value))
                    ? veiculo.Empresa.Apolices.FirstOrDefault(ap => ap.IdApolice == veiculo.ApoliceId.Value)
                    : ApoliceService.Obter(veiculo.ApoliceId.Value);

                var check = Service.Incluir(veiculo);

                //return Json(check, JsonRequestBehavior.AllowGet);
                if (check.success)
                    return RedirectToAction("Edit", new { id = veiculo.IdVeiculo });
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
            ViewBag.Empresas = new SelectList(
                EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia", veiculo.Empresa.IdEmpresa);
            ViewBag.Apolices = new SelectList(
                ApoliceService.ObterValidas(veiculo.Empresa.IdEmpresa, false), "IdApolice", "Numero", veiculo.Apolice.IdApolice);
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
        public ActionResult Edit([Bind(Include = "IdVeiculo,Marca,Modelo,AnoFabricacao,AnoModelo,Cor,Placa,Chassi,Observacao,Criacao,Criador,Atualizacao,Atualizador,Ativo,TipoServico,Categoria,AcessoManobra,EmpresaId,ApoliceId")] Veiculo veiculo)
        {
            if (ModelState.IsValid)
            {
                veiculo.Atualizacao = DateTime.Now;
                veiculo.Atualizador = User.Identity.Name;

                veiculo.Empresa = EmpresaService.ObterPorId(veiculo.EmpresaId.Value);
                veiculo.Apolice = (veiculo.Empresa.Apolices != null && veiculo.Empresa.Apolices.Any(a => a.IdApolice == veiculo.ApoliceId.Value))
                    ? veiculo.Empresa.Apolices.FirstOrDefault(ap => ap.IdApolice == veiculo.ApoliceId.Value)
                    : ApoliceService.Obter(veiculo.ApoliceId.Value);

                var check = Service.Atualizar(veiculo);

                return Json(check, JsonRequestBehavior.AllowGet);
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
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
