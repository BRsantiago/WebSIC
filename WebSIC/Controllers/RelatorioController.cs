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
        public ICursoService cursoService;

        public RelatorioController(IEmpresaService _empresaService,
                                        IAeroportoService _aeroportoService,
                                            ICursoService _cursoService)
        {
            empresaService = _empresaService;
            aeroportoService = _aeroportoService;
            cursoService = _cursoService;
        }

        public ActionResult GetEmpresas(int idAeroporto)
        {
            var empresaItems = this.empresaService.ObterPorAeroporto(idAeroporto)
                                                  .OrderBy(a => a.NomeFantasia)
                                                  .Select(e => new SelectListItem()
                                                  {
                                                      Text = string.Format("{0} - {1}", e.NomeFantasia, e.CGC),
                                                      Value = e.IdEmpresa.ToString()
                                                  }).ToList();

            empresaItems.Add(new SelectListItem() { Text = "TODAS AS EMPRESAS", Value = "0" });

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

        public ActionResult IdentificadosComCursoLegadoFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            return View(model);
        }

        public ActionResult FuncionariosPorEmpresaFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult CredenciaisExtraviadasRoubadasOuFurtadaFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult CredenciaisEmitidasNoPeridoFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult CredenciaisVencidasNoPeridoFiltro()
        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult IdentificadosComCursoVencidoFiltro()

        {
            RelatorioViewModel model = new RelatorioViewModel();

            model.Aeroportos = this.aeroportoService.ObterTodos();
            model.Cursos = this.cursoService.ObterTodos();
            model.Empresas = new List<Empresa>();

            return View(model);
        }

        public ActionResult CredenciaisPorTipoSolicitacaoFiltro()
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

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
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

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
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

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
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

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
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

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
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

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Termo de via Adicional";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioCredenciaisExtraviadasRoubadasOuFurtada(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Credenciais extraviadas";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioCredenciaisEmitidasNoPeriodo(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Credenciais Emitidas";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioCredenciaisVencidasNoPeriodo(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Credenciais vencidas no periodo";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioIdentificadosComCursoVencido(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Identificados com curso vencido";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDCURSO", model.IdCurso));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioCredenciaisPorTipoSolicitacao(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Credenciais por tipo de solicitação";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAINICIAL", model.DataInicial));
            reportViewer.ServerReport.SetParameters(new ReportParameter("DATAFINAL", model.DataFinal));
            reportViewer.ServerReport.SetParameters(new ReportParameter("IDEMPRESA", model.IdEmpresa.ToString()));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }

        public ActionResult RenderizarRelatorioIdentificadosComCursoLegado(RelatorioViewModel model)
        {
            var reportViewer = new ReportViewer()
            {
                ProcessingMode = ProcessingMode.Remote,
                SizeToReportContent = true
            };

            reportViewer.ShowParameterPrompts = false;
            reportViewer.ShowCredentialPrompts = false;

            reportViewer.ServerReport.ReportServerCredentials = new ReportServerNetworkCredentials("CASSA\\bruno.santiago", "on19290932572");
            reportViewer.ServerReport.ReportPath = ConfigurationManager.AppSettings["ReportServerPath"] + "Identificados com curso - Legado";
            reportViewer.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);

            reportViewer.ServerReport.SetParameters(new ReportParameter("NOME", model.PesquisaGeral));

            ViewBag.ReportViewer = reportViewer;

            return PartialView("../Shared/Report");
        }
    }
}