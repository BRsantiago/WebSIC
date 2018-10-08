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
    public class EmpresaController : Controller
    {
        public IEmpresaService EmpresaService;
        public ITipoEmpresaService TipoEmpresaService;
        public IAeroportoService AeroportoService;

        public EmpresaController(IEmpresaService _EmpresaService,
                                    ITipoEmpresaService _TipoEmpresaService,
                                        IAeroportoService _AeroportoService)
        {
            EmpresaService = _EmpresaService;
            TipoEmpresaService = _TipoEmpresaService;
            AeroportoService = _AeroportoService;
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
            EmpresaViewModel model = new EmpresaViewModel();

            model.Aeroportos = AeroportoService.ObterTodos();
            model.TiposEmpresa = TipoEmpresaService.ObterTodos();

            return PartialView(model);
        }

        // POST: Empresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(EmpresaViewModel model)
        {
            try
            {

                TipoEmpresa tipoEmpresa = TipoEmpresaService.ObterPorId(model.IdTipoEmpresa);
                Aeroporto aeroporto = AeroportoService.ObterPorId(model.IdAeroporto);
                List<Aeroporto> aeroportos = new List<Aeroporto>();
                aeroportos.Add(aeroporto);

                Empresa novaEmpresa = new Empresa()
                {
                    RazaoSocial = model.RazaoSocial,
                    NomeFantasia = model.NomeFantasia,
                    Endereco = model.Endereco,
                    Complemento = model.Complemento,
                    Numero = model.Numero,
                    Bairro = model.Bairro,
                    Cidade = model.Cidade,
                    UF = model.UF,
                    CGC = model.CGC,
                    Telefone = model.Telefone,
                    TipoCobranca = model.TipoCobranca,
                    Observacao = model.Observacao,
                    CEP = model.CEP,
                    Email = model.Email,
                    TipoEmpresa = tipoEmpresa,
                    Aeroportos = aeroportos
                };

                EmpresaService.IncluirNovaEmpresa(novaEmpresa);

                return Json(new { success = true, title = "Sucesso", message = "Nova empresa cadastrada com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Empresa/Edit/5
        public ActionResult Edit(int? id)
        {
            EmpresaViewModel model = new EmpresaViewModel();

            Empresa empresa = EmpresaService.ObterPorId(id.Value);

            model.Aeroportos = AeroportoService.ObterTodos();
            model.TiposEmpresa = TipoEmpresaService.ObterTodos();

            model.IdEmpresa = empresa.IdEmpresa;
            model.RazaoSocial = empresa.RazaoSocial;
            model.NomeFantasia = empresa.NomeFantasia;
            model.Endereco = empresa.Endereco;
            model.Complemento = empresa.Complemento;
            model.Numero = empresa.Numero;
            model.Bairro = empresa.Bairro;
            model.Cidade = empresa.Cidade;
            model.UF = empresa.UF;
            model.CGC = empresa.CGC;
            model.Telefone = empresa.Telefone;
            model.TipoCobranca = empresa.TipoCobranca;
            model.Observacao = empresa.Observacao;
            model.CEP = empresa.CEP;
            model.Email = empresa.Email;
            model.IdTipoEmpresa = empresa.TipoEmpresa.IdTipoEmpresa;
            model.IdAeroporto = empresa.Aeroportos.FirstOrDefault().IdAeroporto;

            return PartialView(model);
        }

        // POST: Empresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(EmpresaViewModel model)
        {
            try
            {
                TipoEmpresa tipoEmpresa = TipoEmpresaService.ObterPorId(model.IdTipoEmpresa);
                Aeroporto aeroporto = AeroportoService.ObterPorId(model.IdAeroporto);
                List<Aeroporto> aeroportos = new List<Aeroporto>();
                aeroportos.Add(aeroporto);
                Empresa empresa = EmpresaService.ObterPorId(model.IdEmpresa);

                empresa.RazaoSocial = model.RazaoSocial;
                empresa.NomeFantasia = model.NomeFantasia;
                empresa.Endereco = model.Endereco;
                empresa.Complemento = model.Complemento;
                empresa.Numero = model.Numero;
                empresa.Bairro = model.Bairro;
                empresa.Cidade = model.Cidade;
                empresa.UF = model.UF;
                empresa.CGC = model.CGC;
                empresa.Telefone = model.Telefone;
                empresa.TipoCobranca = model.TipoCobranca;
                empresa.Observacao = model.Observacao;
                empresa.CEP = model.CEP;
                empresa.Email = model.Email;
                empresa.TipoEmpresa = tipoEmpresa;

                EmpresaService.AtualizarNovaEmpresa(empresa);

                return Json(new { success = true, title = "Sucesso", message = "Empresa atualizada com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
