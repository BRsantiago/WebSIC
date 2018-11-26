using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using CrystalDecisions.CrystalReports.Engine;
using Entity.DTO;
using Entity.Entities;
using Repository.Context;
using Service.Interface;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    [AllowAnonymous]
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
        public ICargoService CargoService;
        public IRamoAtividadeService RamoAtividadeService;

        public SolicitacaoController(IAeroportoService _AeroportoService,
                                     IEmpresaService _EmpresaService,
                                     IContratoService _ContratoService,
                                     ITipoSolicitacaoService _TipoSolicitacaoService,
                                     ISolicitacaoService _SolicitacaoService,
                                     IPessoaService _PessoaService,
                                     IAreaService _AreaService,
                                     IVeiculoService _VeiculoService,
                                     IPortaoAcessoService _PortaoService,
                                     ICargoService _CargoService,
                                     IRamoAtividadeService _RamoAtividadeService)
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
            CargoService = _CargoService;
            RamoAtividadeService = _RamoAtividadeService;
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

            model.IdPessoa = Convert.ToInt32(id);


            model.Aeroportos = AeroportoService.ObterTodos();
            model.Empresas = new List<Empresa>(); //EmpresaService.ObterTodos();
            model.Contratos = new List<Contrato>(); //ContratoService.ObterTodos();
            model.TiposSolicitacao = TipoSolicitacaoService.ObterTodos();
            model.Areas = AreaService.Listar().ToList();
            model.Cargo = CargoService.Listar().ToList();
            model.RamoAtividade = RamoAtividadeService.ObterTodos().ToList();

            return PartialView(model);
        }

        public ActionResult GetEmpresas(int idAeroporto)
        {
            var empresaItems = EmpresaService.ObterPorAeroporto(idAeroporto)
                .Select(e => new SelectListItem() { Text = string.Format("{0} - {1}", e.NomeFantasia, e.CGC), Value = e.IdEmpresa.ToString() });
            return Json(empresaItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetContratos(int idEmpresa)
        {
            var contratoItems = ContratoService.ObterVigentes(idEmpresa)
                .Select(c => new SelectListItem() { Text = c.Numero, Value = c.IdContrato.ToString() });
            return Json(contratoItems, JsonRequestBehavior.AllowGet);
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
                var solicitacao = model.MapearParaObjetoDominio();
                solicitacao.Criador =
                    solicitacao.Atualizador = User.Identity.Name;

                SolicitacaoService.Salvar(solicitacao);

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
            SolicitacaoViewModel model = new SolicitacaoViewModel(this.SolicitacaoService.ObterPorId(id));

            model.Aeroportos = AeroportoService.ObterTodos();
            model.Empresas = EmpresaService.ObterTodos();
            model.Contratos = ContratoService.ObterTodos();
            model.TiposSolicitacao = TipoSolicitacaoService.ObterTodos();
            model.Areas = AreaService.Listar().ToList();
            model.Cargo = CargoService.Listar().ToList();
            model.RamoAtividade = RamoAtividadeService.ObterTodos().ToList();

            return PartialView(model);
        }

        // POST: Solicitacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SolicitacaoViewModel model)
        {
            try
            {
                SolicitacaoService.Atualizar(model.MapearParaObjetoDominio());
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

        // GET: Solicitacao/Delete/5
        public ActionResult Delete(int? id)
        {
            SolicitacaoViewModel model = new SolicitacaoViewModel(this.SolicitacaoService.ObterPorId(id));

            model.Aeroportos = AeroportoService.ObterTodos();
            model.Empresas = EmpresaService.ObterTodos();
            model.Contratos = ContratoService.ObterTodos();
            model.TiposSolicitacao = TipoSolicitacaoService.ObterTodos();
            model.Areas = AreaService.Listar().ToList();
            model.Cargo = CargoService.Listar().ToList();
            model.RamoAtividade = RamoAtividadeService.ObterTodos().ToList();

            return PartialView(model);
        }

        // POST: Solicitacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int IdSolicitacao)
        {
            try
            {
                Solicitacao solicitacao = SolicitacaoService.ObterPorId(IdSolicitacao);
                SolicitacaoService.ExcluirSolicitacao(solicitacao);

                return Json(new { success = true, title = "Sucesso", message = "Solicitação excluída com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitacao solicitacao = SolicitacaoService.Obter(id.Value);
            if (solicitacao == null)
            {
                return HttpNotFound();
            }
            return PartialView(solicitacao);
        }

        public ActionResult CreateATIV(int? veiculoId)
        {
            if (veiculoId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Veiculo veiculo = VeiculoService.Obter(veiculoId.Value);
            var solicitacoes = SolicitacaoService.ObterPorVeiculo(veiculoId.Value);
            var solicitacao = solicitacoes.LastOrDefault(s => s.DataAutorizacao.HasValue || s.Ativo);

            ViewBag.VeiculoId =
                veiculo.IdVeiculo;
            ViewBag.TiposEmissao = solicitacao != null 
                ? EnumHelper.GetSelectList(typeof(Entity.Enum.TipoEmissao), solicitacao.TipoEmissao)
                : EnumHelper.GetSelectList(typeof(Entity.Enum.TipoEmissao));

            ViewBag.Veiculo =
                string.Format("{0} {1} {2}/{3} {4} {5} {6}", veiculo.Marca, veiculo.Modelo, veiculo.AnoFabricacao, veiculo.AnoModelo, veiculo.Cor, veiculo.Placa, veiculo.Chassi);
            ViewBag.Aeroportos =
                new SelectList(AeroportoService.ObterTodos(), "IdAeroporto", "Descricao", solicitacao?.AeroportoId ?? veiculo.Empresa?.AeroportoId); //(veiculo.Empresa != null && veiculo.Empresa.Aeroporto != null) ? veiculo.Empresa.Aeroporto.IdAeroporto : 0
            ViewBag.Empresas =
                new SelectList(EmpresaService.ObterTodos(), "IdEmpresa", "NomeFantasia", solicitacao?.EmpresaId ?? veiculo.EmpresaId); //veiculo.Empresa.IdEmpresa
            ViewBag.Contratos =
                new SelectList(ContratoService.ObterVigentes(veiculo.EmpresaId.Value).OrderBy(c => c.InicioVigencia), "IdContrato", "Numero", solicitacao?.ContratoId);
            ViewBag.TiposSolicitacao =
                new SelectList(TipoSolicitacaoService.Listar().OrderBy(ts => ts.Descricao), "IdTipoSolicitacao", "Descricao");
            ViewBag.Areas =
                new SelectList(AreaService.Listar().OrderBy(a => a.Descricao), "IdArea", "Descricao", solicitacao?.Area1Id);
            ViewBag.Portoes =
                new SelectList(PortaoService.Listar().OrderBy(p => p.Descricao), "IdPortaoAcesso", "Descricao", solicitacao?.PortaoAcesso1Id);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateATIV([Bind(Include = "IdSolicitacao,Criacao,Criador,Atualizacao,Atualizador,Ativo,TipoEmissao,VeiculoId,EmpresaId,ContratoId,TipoSolicitacaoId,Area1Id,Area2Id,PortaoAcesso1Id,PortaoAcesso2Id,PortaoAcesso3Id")] Solicitacao solicitacao, FormCollection form)
        {
            solicitacao.Criador =
                solicitacao.Atualizador = User.Identity.Name;

            #region
            //solicitacao.Veiculo = new Veiculo() { IdVeiculo = (int.Parse(form["VeiculoId"])) };
            //solicitacao.Empresa = new Empresa() { IdEmpresa = (int.Parse(form["EmpresaId"])) };
            //solicitacao.Contrato = new Contrato() { IdContrato = int.Parse(form["ContratoId"]) };
            //solicitacao.TipoSolicitacao = new TipoSolicitacao() { IdTipoSolicitacao = int.Parse(form["TipoSolicitacaoId"]) };
            //solicitacao.Area1 = new Entity.Entities.Area() { IdArea = int.Parse(form["Area1Id"]) };
            //solicitacao.Area2 = !string.IsNullOrEmpty(form["Area2Id"]) ? new Entity.Entities.Area() { IdArea = int.Parse(form["Area2Id"]) } : null;
            //solicitacao.PortaoAcesso = new PortaoAcesso() { IdPortaoAcesso = int.Parse(form["PortaoAcessoId"]) };
            #endregion

            ServiceReturn check = SolicitacaoService.SalvarATIV(solicitacao);
            
            #region
            //try
            //{
            //    SolicitacaoService.SalvarATIV(solicitacao);
            //    check = new ServiceReturn()
            //    {
            //        success = true,
            //        title = "Sucesso",
            //        message = "Solicitação de ATIV cadastrada com sucesso!",
            //        id = solicitacao.IdSolicitacao
            //    };
            //}
            //catch (Exception ex)
            //{
            //    check = new ServiceReturn()
            //    {
            //        success = false,
            //        title = "Erro",
            //        message = string.Format("Erro ao cadastrar a solicitação de ATIV! {0} - {1}", ex.GetType(), ex.Message),
            //        id = 0
            //    };
            //}
            #endregion

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ApproveATIV(int id)
        {
            ServiceReturn check = null;

            Solicitacao solicitacao = SolicitacaoService.Obter(id);
            solicitacao.DataAutorizacao = DateTime.Now;
            solicitacao.Atualizacao = DateTime.Now;
            solicitacao.Atualizador = User.Identity.Name;

            try
            {
                SolicitacaoService.AprovarATIV(solicitacao);
                check = new ServiceReturn()
                {
                    success = true,
                    title = "Sucesso",
                    message = "Solicitação de ATIV aprovada com sucesso!",
                    id = solicitacao.IdSolicitacao
                };
            }
            catch (Exception ex)
            {
                check = new ServiceReturn()
                {
                    success = false,
                    title = "Erro",
                    message = string.Format("Erro ao aprovar a solicitação de ATIV! {0} - {1}", ex.GetType(), ex.Message),
                    id = 0
                };
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CancelATIV(int id)
        {
            ServiceReturn check = null;
            Solicitacao solicitacao = SolicitacaoService.Obter(id);
            solicitacao.Ativo = false;
            solicitacao.Atualizacao = DateTime.Now;
            solicitacao.Atualizador = User.Identity.Name;

            try
            {
                SolicitacaoService.AtualizarATIV(solicitacao);
                check = new ServiceReturn()
                {
                    success = true,
                    title = "Sucesso",
                    message = "Solicitação de ATIV cancelada com sucesso!",
                    id = solicitacao.IdSolicitacao
                };
            }
            catch (Exception ex)
            {
                check = new ServiceReturn()
                {
                    success = false,
                    title = "Erro",
                    message = string.Format("Erro ao cancelar a solicitação de ATIV! {0} - {1}", ex.GetType(), ex.Message),
                    id = 0
                };
            }

            return Json(check, JsonRequestBehavior.AllowGet);
        }
    }
}
