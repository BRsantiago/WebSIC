using Entity.Entities;
using Microsoft.Reporting.WebForms;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    public class RelatorioController : Controller
    {
        public IEmpresaService empresaService;
        public IAeroportoService aeroportoService;

        public RelatorioController(IEmpresaService _empresaService,
                                        IAeroportoService _aeroportoService)
        {
            empresaService = _empresaService;
            aeroportoService = _aeroportoService;
        }

        public ActionResult GetEmpresas(int idAeroporto)
        {
            var empresaItems = this.empresaService.ObterPorAeroporto(idAeroporto)
                                                  .OrderBy(a => a.NomeFantasia)
                                                  .Select(e => new SelectListItem()
                                                  {
                                                      Text = string.Format("{0} - {1}", e.NomeFantasia, e.CGC),
                                                      Value = e.IdEmpresa.ToString()
                                                  });

            return Json(empresaItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CredenciaisEmitidasFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult CobrancaFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult FuncionariosPorEmpresaFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult TermoCancelamentoFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult TermoDestruicaoFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult TermoIndeferimentoFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult TermoViaAdicionalFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult RenderizarRelatorioCredenciaisEmitidas(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Credenciais Emitidas";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioCobranca(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Cobrança";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioFuncionariosPorEmpresa(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Funcionários Por Empresa";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioTermoCancelamento(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Termo de Cancelamento";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioTermoDestruicao(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Termo de Destruição";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioTermoIndeferimento(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Termo de Indeferimento";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioViaAdicional(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Termo de via Adicional";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }
    }
}