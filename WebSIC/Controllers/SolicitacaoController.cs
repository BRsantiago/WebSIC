using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CrystalDecisions.CrystalReports.Engine;
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
        public IAreaService AreaService;
        public IVeiculoService VeiculoService;
        public IPortaoAcessoService PortaoService;

        public SolicitacaoController(IAeroportoService _AeroportoService,
                                     IEmpresaService _EmpresaService,
                                     IContratoService _ContratoService,
                                     ITipoSolicitacaoService _TipoSolicitacaoService,
                                     ISolicitacaoService _SolicitacaoService,
                                     IPessoaService _PessoaService,
                                     IAreaService _AreaService,
                                     IVeiculoService _VeiculoService,
                                     IPortaoAcessoService _PortaoService)
        {
            AeroportoService = _AeroportoService;
            EmpresaService = _EmpresaService;
            ContratoService = _ContratoService;
            TipoSolicitacaoService = _TipoSolicitacaoService;
            SolicitacaoService = _SolicitacaoService;
            PessoaService = _PessoaService;
            AreaService = _AreaService;
            VeiculoService = _VeiculoService;
            PortaoService = _PortaoService;
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
        public ActionResult Create(string id)
        {
            SolicitacaoViewModel model = new SolicitacaoViewModel();

            //List<TipoEmissao> TipoEmissaoLista = new List<TipoEmissao>();
            //TipoEmissaoLista.Add(new TipoEmissao() { IdTipoEmissao = 0, Descricao = "Temporário" });
            //TipoEmissaoLista.Add(new TipoEmissao() { IdTipoEmissao = 1, Descricao = "Definitivo" });
            model.IdPessoa = Convert.ToInt32(id);


            model.Aeroportos = AeroportoService.ObterTodos();
            model.Empresas = EmpresaService.ObterTodos();
            model.Contratos = ContratoService.ObterTodos();
            model.TiposSolicitacao = TipoSolicitacaoService.ObterTodos();
            //model.TiposEmissao = TipoEmissaoLista;
            model.Areas = AreaService.Listar().ToList();

            return PartialView(model);
        }

        // POST: Solicitacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SolicitacaoViewModel model)
        {
            try
            {
                SolicitacaoService.Salvar(model.MapearParaObjetoDominio());
                return Json(new
                {
                    success = true,
                    title = "Sucesso",
                    message = "Representante cadastrado com sucesso !"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    title = "Erro",
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
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

        public ActionResult IndexATIV(int? veiculoId)
        {
            if (veiculoId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            List<Solicitacao> solicitacoes = SolicitacaoService.ObterPorVeiculo(veiculoId.Value);

            return View(solicitacoes);
        }

        // GET: Solicitacao/Details/5
        public ActionResult DetailsATIV(int? id)
        {
            return View(new Solicitacao());
        }

        public ActionResult CreateATIV(int? veiculoId)
        {
            if (veiculoId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Veiculo veiculo = VeiculoService.Obter(veiculoId.Value);

            ViewBag.VeiculoId =
                veiculo.IdVeiculo;
            ViewBag.Veiculo =
                string.Format("{0} {1} {2}/{3} {4} {5} {6}", veiculo.Marca, veiculo.Modelo, veiculo.AnoFabricacao, veiculo.AnoModelo, veiculo.Cor, veiculo.Placa, veiculo.Chassi);
            ViewBag.Empresas =
                new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia", veiculo.Empresa.IdEmpresa);
            ViewBag.Contratos =
                new SelectList(ContratoService.ObterVigentes(veiculo.EmpresaId.Value), "IdContrato", "Numero");
            ViewBag.TiposSolicitacao =
                new SelectList(TipoSolicitacaoService.Listar(), "IdTipoSolicitacao", "Descricao");
            ViewBag.Areas =
                new SelectList(AreaService.Listar(), "IdArea", "Descricao");
            ViewBag.Portoes =
                new SelectList(PortaoService.Listar(), "IdPortaoAcesso", "Descricao");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateATIV([Bind(Include = "IdSolicitacao,Criacao,Criador,Atualizacao,Atualizador,Ativo,TipoEmissao")] Solicitacao solicitacao, FormCollection form)
        {
            solicitacao.Criador =
                solicitacao.Atualizador = User.Identity.Name;
            
            solicitacao.Veiculo = new Veiculo() { IdVeiculo = (int.Parse(form["VeiculoId"])) };
            solicitacao.Empresa = new Empresa() { IdEmpresa = (int.Parse(form["EmpresaId"])) };
            solicitacao.Contrato = new Contrato() { IdContrato = int.Parse(form["Contrato.IdContrato"]) };
            solicitacao.TipoSolicitacao = new TipoSolicitacao() { IdTipoSolicitacao = int.Parse(form["TipoSolicitacao.IdTipoSolicitacao"]) };
            solicitacao.Area1 = new Entity.Entities.Area() { IdArea = int.Parse(form["Area1.IdArea"]) };
            solicitacao.Area2 = new Entity.Entities.Area() { IdArea = int.Parse(form["Area2.IdArea"]) };
            solicitacao.PortaoAcesso = new PortaoAcesso() { IdPortaoAcesso = int.Parse(form["PortaoAcesso.IdPortaoAcesso"]) };

            ServiceReturn check = null;

            try
            {
                SolicitacaoService.Salvar(solicitacao);
                check = new ServiceReturn()
                {
                    success = true,
                    title = "Sucesso",
                    message = "Solicitação de ATIV cadastrada com sucesso!",
                    id = solicitacao.IdSolicitacao
                };
            }
            catch (Exception ex)
            {
                check = new ServiceReturn()
                {
                    success = false,
                    title = "Erro",
                    message = string.Format("Erro ao cadastrar a solicitação de ATIV! {0} - {1}", ex.GetType(), ex.Message),
                    id = 0
                };
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }


        public void Imprimir()
        {
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Server.MapPath("/Credenciais/VinciCardFrontBack.rpt"));

            cryRpt.SetParameterValue("Nombre", "Bruno Santiago");
            cryRpt.SetParameterValue("Fecha", "17/07/2018");
            cryRpt.SetParameterValue("Acceso", "A R");
            cryRpt.SetParameterValue("Pocision", "Analista de Sistemas");
            cryRpt.SetParameterValue("FotoPath", "../../WebImages/person.jpg");
            cryRpt.SetParameterValue("Motorista1", "N");
            cryRpt.SetParameterValue("Motorista2", "N");
            cryRpt.SetParameterValue("EmpresaPath", "");
            cryRpt.SetParameterValue("RG", "99999999-99");
            cryRpt.SetParameterValue("CPF", "999.999.999-99");
            cryRpt.SetParameterValue("Matricula", "99999");
            cryRpt.SetParameterValue("Empresa", "Vinci");
            cryRpt.SetParameterValue("Emergencia", "71 99999-9999");
            cryRpt.SetParameterValue("Expedicao", "26/10/2018");

            //cryRpt.SetParameterValue("Nombre", "Alexandre H. A. Monteiro");
            //cryRpt.SetParameterValue("Fecha", "26/09/2021");
            //cryRpt.SetParameterValue("Acceso", "R");
            //cryRpt.SetParameterValue("Pocision", "Analista de TI Senior");
            //cryRpt.SetParameterValue("Motorista1", "B");
            //cryRpt.SetParameterValue("Motorista2", "");
            //cryRpt.SetParameterValue("FotoPath", @"\\172.21.12.21\id-img\user.jpg");

            //cryRpt.SetParameterValue("Expedicao", "26/09/2018");
            //cryRpt.SetParameterValue("RG", "08.790.022-04");
            //cryRpt.SetParameterValue("CPF", "020.062.495-41");
            //cryRpt.SetParameterValue("Empresa", "Vinci Airports");
            //cryRpt.SetParameterValue("Matricula", "01.547-18");
            //cryRpt.SetParameterValue("Emergencia", "71 99981-8816");




            cryRpt.ReportClientDocument.PrintOutputController.PrintReport();
            //cryRpt.PrintToPrinter(1, true, 1, 2);

            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //CrDiskFileDestinationOptions.DiskFileName = resultPath;
            //PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            //ExportOptions CrExportOptions = cryRpt.ExportOptions; //cryRptFront.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = CrFormatTypeOptions;
            //}
            //#region
            ////cryRptFront.Export();
            //#endregion

        }
    }
}
