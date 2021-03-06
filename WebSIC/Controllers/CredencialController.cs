﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Net;
using System.Printing;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Entity.Entities;
using Repository.Context;
using Service.Interface;

namespace WebSIC.Controllers
{
    //[AllowAnonymous]
    public class CredencialController : Controller
    {

        public ICredencialService CredencialService;
        public ITipoCrachaService TipoCrachaService;

        public CredencialController(ICredencialService _CredencialService,
                                        ITipoCrachaService _TipoCrachaService)
        {
            CredencialService = _CredencialService;
            TipoCrachaService = _TipoCrachaService;
        }

        // GET: Credencial
        public ActionResult Index()
        {
            List<Credencial> lista = this.CredencialService.ObterTodasCredenciaisAtivasDeFuncionario();

            lista.ForEach(c => c.DataVencimento = this.GerarDataVencimentoCredencial(c));

            return View(lista);
        }

        public ActionResult Edit(string id)
        {
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(id));

            credencial.DataVencimento = this.GerarDataVencimentoCredencial(credencial);

            ViewBag.Printers = GetPrinters();

            return View(credencial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Credencial credencial)
        {
            try
            {
                ViewBag.Printers = GetPrinters();

                Credencial credencialBase = this.CredencialService.ObterPorId(credencial.IdCredencial);

                credencialBase.Atualizacao = DateTime.Now;
                credencialBase.Atualizador = User.Identity.Name;
                credencialBase.NomeImpressaoFrenteCracha = credencial.NomeImpressaoFrenteCracha;
                credencialBase.DescricaoFuncaoFrenteCracha = credencial.DescricaoFuncaoFrenteCracha;
                credencialBase.DataVencimento = credencial.DataVencimento;

                this.CredencialService.Atualizar(credencialBase);

                credencial = credencialBase;

                var msg = "<script> swal({title: 'Good job!', text: 'Alterações salvas com sucesso !', icon: 'success', button: 'OK!'}) </script>";
                TempData["notification"] = msg;
            }
            catch (Exception ex)
            {
                var msg = "<script> swal({title: 'Atenção!', text: '" + ex.Message.Replace('\'', ' ') + "', icon: 'warning', button: 'OK!'}) </script>";
                TempData["notification"] = msg;
            }

            return View(credencial);
        }

        public ActionResult PreviewCredencial(string idCredencial)
        {
            try
            {
                Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

                ReportDocument report = new ReportDocument();

                if (credencial.FlgTemporario)
                {
                    TipoCracha temporario = this.TipoCrachaService.ObterTipoCrachaTemporario();

                    Session["ArquivoCrachaFrotal"] = temporario.Arquivo;
                    Session["ImgFundoCracha"] = temporario.ImgFundoCracha;
                    Session["TipoCracha"] = temporario.Descricao;
                }
                else
                {
                    Session["ArquivoCrachaFrotal"] = credencial.Empresa.TipoEmpresa.TipoCracha.Arquivo;
                    Session["ImgFundoCracha"] = credencial.Empresa.TipoEmpresa.TipoCracha.ImgFundoCracha;
                    Session["TipoCracha"] = credencial.Empresa.TipoEmpresa.TipoCracha.Descricao;
                }

                Session["SiglaAeroporto"] = credencial.Aeroporto.Sigla;
                Session["NomeFrenteCracha"] = credencial.NomeImpressaoFrenteCracha == null ? credencial.Pessoa.NomeCompleto.ToUpper() : credencial.NomeImpressaoFrenteCracha.ToUpper();
                Session["DataValidade"] = String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento.HasValue ? credencial.DataVencimento.Value : this.GerarDataVencimentoCredencial(credencial));
                Session["AreaDeAcesso1"] = (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ");// + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : "");
                Session["AreaDeAcesso2"] = (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : " ");// + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : "");
                Session["Funcao"] = credencial.DescricaoFuncaoFrenteCracha.ToUpper();
                Session["Foto"] = Server.MapPath(credencial.Pessoa.ImageUrl.Replace("../..", ""));
                Session["CategoriaMotoristaUm"] = !String.IsNullOrEmpty(credencial.CategoriaMotorista1) && credencial.CategoriaMotorista1 != "0" ? credencial.CategoriaMotorista1 : "N";
                Session["CategoriaMotoristaDois"] = credencial.CategoriaMotorista1 == "C" || credencial.CategoriaMotorista1 == "D" ? credencial.CategoriaMotorista1 : "N";
                Session["CategoriaMotoristaTres"] = credencial.CategoriaMotorista1 == "E" ? credencial.CategoriaMotorista1 : "N";
                Session["LogoEmpresa"] = Server.MapPath(credencial.Empresa.ImageUrl);
                Session["Nome"] = credencial.Pessoa.NomeCompleto.ToUpper();
                Session["RG"] = credencial.Pessoa.RG == null ? "" : credencial.Pessoa.RG;
                Session["CPF"] = credencial.Pessoa.CPF == null ? credencial.Pessoa.RNE : credencial.Pessoa.CPF;
                Session["Empresa"] = credencial.Empresa.NomeFantasia.ToUpper();
                Session["Matricula"] = credencial.IdCredencial.ToString().PadLeft(8, '0');
                Session["Emergencia"] = credencial.Pessoa.TelefoneEmergencia;
                Session["DataExpediacao"] = String.Format("{0:dd/MM/yy}", DateTime.Now);
                Session["PathLogoBack"] = credencial.Pessoa.FlgCVE ? "logo_vol_emergencia.png" : "CoberturaCVE.png";
                Session["TipoCredencial"] = "Credencial";
                Session["SegundaVia"] = credencial.FlgSegundaVia ? "2ª" : "";
                Session["ManipulaBagagem"] = credencial.ManipulaBagagem ? "S" : "N";
                Session["AcessoAreaManobra"] = credencial.AcessoAreaManobra ? "ComAcesso.png" : "SemAcesso.png";


                return Json(new { success = true, title = "Sucesso", message = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Imprimir(string idCredencial, string printerName)
        {
            try
            {
                if (printerName == "Selecione")
                    throw new Exception("Selecione uma impressora antes de solicitar a impressão.");

                Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

                this.ValidarParaImpressão(credencial);

                credencial.Atualizacao = DateTime.Now;
                credencial.Atualizador = User.Identity.Name;
                credencial.DataExpedicao = DateTime.Now;
                credencial.DataVencimento = credencial.DataVencimento.HasValue ? credencial.DataVencimento.Value : this.GerarDataVencimentoCredencial(credencial);

                ReportDocument cryRpt = new ReportDocument();

                if (credencial.FlgTemporario)
                {
                    TipoCracha temporario = this.TipoCrachaService.ObterTipoCrachaTemporario();

                    cryRpt.Load(Server.MapPath("/Credenciais/" + temporario.Arquivo));
                    cryRpt.SetParameterValue("ImgFundoPath", Server.MapPath("/Images/FundoCracha/" + temporario.ImgFundoCracha));
                    cryRpt.SetParameterValue("TipoCracha", temporario.Descricao);
                }
                else
                {
                    cryRpt.Load(Server.MapPath("/Credenciais/" + credencial.Empresa.TipoEmpresa.TipoCracha.Arquivo));
                    cryRpt.SetParameterValue("ImgFundoPath", Server.MapPath("/Images/FundoCracha/" + credencial.Empresa.TipoEmpresa.TipoCracha.ImgFundoCracha));
                    cryRpt.SetParameterValue("TipoCracha", credencial.Empresa.TipoEmpresa.TipoCracha.Descricao);

                }

                cryRpt.SetParameterValue("Aeroporto", credencial.Aeroporto.Sigla.ToUpper());
                cryRpt.SetParameterValue("Nombre", credencial.NomeImpressaoFrenteCracha == null ? credencial.Pessoa.NomeCompleto.ToUpper() : credencial.NomeImpressaoFrenteCracha.ToUpper());
                cryRpt.SetParameterValue("Fecha", String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento.Value));
                cryRpt.SetParameterValue("Acceso", (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " "));// + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : ""));
                cryRpt.SetParameterValue("Acceso2", (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : " "));
                cryRpt.SetParameterValue("Pocision", credencial.DescricaoFuncaoFrenteCracha.ToUpper());
                cryRpt.SetParameterValue("FotoPath", Server.MapPath(credencial.Pessoa.ImageUrl));
                cryRpt.SetParameterValue("Motorista1", !String.IsNullOrEmpty(credencial.CategoriaMotorista1) && credencial.CategoriaMotorista1 != "0" ? credencial.CategoriaMotorista1 : "N");
                cryRpt.SetParameterValue("Motorista2", credencial.CategoriaMotorista1 == "C" || credencial.CategoriaMotorista1 == "D" ? credencial.CategoriaMotorista1 : "N");
                cryRpt.SetParameterValue("Motorista3", credencial.CategoriaMotorista1 == "E" ? credencial.CategoriaMotorista1 : "N");
                cryRpt.SetParameterValue("EmpresaPath", Server.MapPath(credencial.Empresa.ImageUrl));
                cryRpt.SetParameterValue("Nombre", credencial.Pessoa.NomeCompleto.ToUpper(), "CardBack.rpt");
                cryRpt.SetParameterValue("RG", credencial.Pessoa.RG == null ? "" : credencial.Pessoa.RG, "CardBack.rpt");
                cryRpt.SetParameterValue("CPF", credencial.Pessoa.CPF == null ? credencial.Pessoa.RNE : credencial.Pessoa.CPF, "CardBack.rpt");
                cryRpt.SetParameterValue("Matricula", credencial.IdCredencial.ToString().PadLeft(8, '0'), "CardBack.rpt");
                cryRpt.SetParameterValue("Empresa", credencial.Empresa.NomeFantasia.ToUpper(), "CardBack.rpt");
                cryRpt.SetParameterValue("Emergencia", credencial.Pessoa.TelefoneEmergencia, "CardBack.rpt");
                cryRpt.SetParameterValue("Fecha", String.Format("{0:dd/MM/yy}", credencial.DataExpedicao), "CardBack.rpt");
                cryRpt.SetParameterValue("Logo", Server.MapPath("/Images/Logo/" + (credencial.Pessoa.FlgCVE ? "logo_vol_emergencia.png" : "CoberturaCVE.png")));
                cryRpt.SetParameterValue("SegundaVia", credencial.FlgSegundaVia ? "2" : "");
                cryRpt.SetParameterValue("ManipulaBagagem", credencial.ManipulaBagagem ? "S" : "N");
                cryRpt.SetParameterValue("AcessoAreaManobra", Server.MapPath("/Images/DDA/" + (credencial.AcessoAreaManobra ? "ComAcesso.png" : "SemAcesso.png")));

                this.CredencialService.Atualizar(credencial);

                cryRpt.PrintToPrinter(new PrinterSettings() { PrinterName = printerName }, new PageSettings(), false);

                cryRpt.Refresh();

                return Json(new { success = true, title = "Sucesso", message = "Registro Atualizado com sucesso !" }, JsonRequestBehavior.AllowGet);
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

        public ActionResult IndexATIV()
        {
            var credenciais = CredencialService.ObterATIVs();
            return View(credenciais);
        }

        public void PreviewATIV(string idCredencial)
        {
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

            ReportDocument report = new ReportDocument();

            Session["Arquivo"] = "ATIV.rpt";
            Session["TipoCredencial"] = "ATIV";
            Session["TipoEmissao"] = credencial.FlgTemporario ? "TEMPORÁRIO" : "";
            Session["SiglaAeroporto"] = credencial.Aeroporto?.Sigla;
            Session["AreaDeAcesso"] = credencial.Area1?.Sigla; //(credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : "");
            Session["PortaoDeAcesso"] = string.Format("{0} {1} {2}", credencial.PortaoAcesso1?.Sigla, credencial.PortaoAcesso2?.Sigla, credencial.PortaoAcesso3?.Sigla).Trim(); //credencial.PortaoAcesso1.Sigla;
            Session["Categoria"] = credencial.Veiculo.Categoria;
            Session["DataValidade"] = String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento.HasValue
                ? credencial.DataVencimento.Value
                : credencial.FlgTemporario
                    ? credencial.Criacao.AddDays(30)
                    : credencial.Criacao.AddDays(365)
            );
            Session["Placa"] = credencial.Veiculo.Placa;
            Session["AreaManobra"] = credencial.Veiculo.AcessoManobra ? "ÁREA DE MANOBRA" : "";

            Session["DataExpedicao"] = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            Session["Empresa"] = credencial.Empresa.NomeFantasia.ToUpper();
            Session["MarcaModelo"] = credencial.Veiculo.Marca;
            Session["Cor"] = credencial.Veiculo.Cor;
            Session["Matricula"] = credencial.IdCredencial.ToString().PadLeft(8, '0');
            Session["Chassi"] = credencial.Veiculo.Chassi;
            Session["TipoServico"] = credencial.Veiculo.TipoServico;
            Session["Logo"] = Server.MapPath(credencial.Empresa.ImageUrl);
        }

        [HttpPost]
        public ActionResult ImprimirATIV(string idCredencial)
        {
            try
            {
                Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

                credencial.Atualizacao = DateTime.Now;
                credencial.Atualizador = User.Identity.Name;

                credencial.DataExpedicao = DateTime.Now;
                credencial.DataVencimento = credencial.DataVencimento.HasValue ? credencial.DataVencimento.Value : this.GerarDataVencimentoCredencial(credencial);

                #region Crystal Reports
                //ReportDocument cryRpt = new ReportDocument();
                //cryRpt.Load(Server.MapPath("/Credenciais/ATIV.rpt"));

                //cryRpt.SetParameterValue("TipoEmissao", credencial.FlgTemporario ? "TEMPORÁRIO" : "");
                //cryRpt.SetParameterValue("DataValidade", String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento));
                //cryRpt.SetParameterValue("NivelAcesso", credencial.Area1?.Sigla); //(credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : ""));
                //cryRpt.SetParameterValue("Aeroporto", credencial.Aeroporto?.Sigla);
                //cryRpt.SetParameterValue("Portao", string.Format("{0} {1} {2}", credencial.PortaoAcesso1.Sigla, credencial.PortaoAcesso2.Sigla, credencial.PortaoAcesso3.Sigla).Trim());
                //cryRpt.SetParameterValue("Categoria", credencial.Veiculo.Categoria);
                //cryRpt.SetParameterValue("Placa", credencial.Veiculo.Placa);
                //cryRpt.SetParameterValue("Empresa", credencial.Empresa.NomeFantasia);
                //cryRpt.SetParameterValue("MarcaModelo", credencial.Veiculo.Modelo);
                //cryRpt.SetParameterValue("Cor", credencial.Veiculo.Cor);
                //cryRpt.SetParameterValue("TipoServico", credencial.Veiculo.TipoServico);
                //cryRpt.SetParameterValue("DataExpedicao", credencial.DataExpedicao);
                //cryRpt.SetParameterValue("Chassi", credencial.Veiculo.Chassi);
                //cryRpt.SetParameterValue("Matricula", credencial.IdCredencial);
                //cryRpt.SetParameterValue("AreaManobra", credencial.Veiculo.AcessoManobra ? "ÁREA DE MANOBRA" : "");
                //cryRpt.SetParameterValue("Empresa", credencial.Empresa.NomeFantasia);
                //cryRpt.SetParameterValue("Logo", Server.MapPath(credencial.Empresa.ImageUrl));


                //cryRpt.PrintOptions.PrinterName = printerName;
                //cryRpt.ReportClientDocument.PrintOutputController.PrintReport();
                #endregion

                this.CredencialService.Atualizar(credencial);

                return Json(new { success = true, title = "Sucesso", message = "Credencial de ATIV expedida com sucesso!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult EditATIV(string id)
        {
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(id));
            ViewBag.Printers = GetPrinters();

            return View(credencial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditATIV([Bind(Include = "IdCredencial,FlgTemporario,DataVencimento,AeroportoId,EmpresaId,ContratoId,VeiculoId,Area1Id,PortaoAcesso1Id,PortaoAcesso2Id,PortaoAcesso3Id,Criacao,Criador,Ativo")] Credencial credencial)
        {
            Credencial credencialBase = this.CredencialService.ObterPorId(credencial.IdCredencial);

            credencialBase.Atualizacao = DateTime.Now;
            credencialBase.Atualizador = User.Identity.Name;
            credencialBase.DataVencimento = credencial.DataVencimento;
            CredencialService.Atualizar(credencialBase);

            credencial = credencialBase;

            ViewBag.Printers = GetPrinters();
            return View(credencial);
        }

        private List<SelectListItem> GetPrinters()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            int i = 1;
            using (var printServer = new PrintServer(string.Format(@"\\{0}", "D-CASSASV900")))
            {
                foreach (var queue in printServer.GetPrintQueues())
                {
                    list.Add(new SelectListItem { Text = queue.FullName, Value = i.ToString() });
                }
            }

            using (var printServer = new PrintServer(string.Format(@"\\{0}", "S-CASSASV06")))
            {
                foreach (var queue in printServer.GetPrintQueues())
                {
                    list.Add(new SelectListItem { Text = queue.FullName, Value = i.ToString() });
                }
            }

            return list;
        }

        private DateTime GerarDataVencimentoCredencial(Credencial credencial)
        {
            List<DateTime> datas = new List<DateTime>();

            if (credencial.FlgTemporario)
            {
                datas.Add(DateTime.Now.AddDays(90));
            }
            else
            {
                credencial.Pessoa.Curso
                                 .ToList()
                                 .ForEach(c => datas.Add(c.DataValidade));

                credencial.Empresa.Contratos?
                                  .Where(c => c.FimVigencia > DateTime.Now)
                                  .ToList()
                                  .ForEach(c => datas.Add(c.FimVigencia));

                if (credencial.Pessoa.DataValidadeCNH.HasValue)
                    datas.Add(credencial.Pessoa.DataValidadeCNH.Value);
            }

            return datas.OrderBy(x => x.Date).FirstOrDefault();
        }

        private void ValidarParaImpressão(Credencial credencial)
        {
            if ((!credencial.FlgTemporario) && (credencial.Area1 != null || credencial.Area2 != null) && (credencial.Pessoa.Curso?.Count == 0 && credencial.Pessoa.Turmas?.Count == 0))
                throw new Exception("Favor verificar o cadastro desssa pessoa, ela possui acesso a àreas restritas mas não há curso válido para ela.");

            if (!String.IsNullOrEmpty(credencial.Pessoa.CNH) && (!credencial.Pessoa.Curso.Any(c => c.Curso.PermiteDirigirEmAreasRestritas) || credencial.Pessoa.Curso.Any(c => c.Curso.PermiteDirigirEmAreasRestritas && c.DataValidade < DateTime.Now)))
                throw new Exception("Favor verificar se o curso DDA foi informado no cadastro desta pessoa.");

            if (credencial.Pessoa.Curso.Any(c => c.DataValidade <= DateTime.Now))
                throw new Exception("Favor verificar o cadastro dessa pessoa, existem cursos vencidos.");

            if (credencial.DataDesativacao.HasValue)
                throw new Exception("Esta credencial está desativada.");

            if (credencial.DataExpedicao.HasValue)
                throw new Exception("Esta credencial já foi impressa! Caso seja necessário uma reimpressão, realizar a solicitação no cadastro da pessoa.");

            if (credencial.DataVencimento > credencial.Contrato?.FimVigencia)
                throw new Exception("Esta credencial não pode ser impressa pois a data de vencimento informada é maior que a vigência do contrato selecionado.");

            credencial.Pessoa.Curso.ToList().ForEach(c =>
            {
                if (credencial.DataVencimento > c.DataValidade)
                    throw new Exception("Esta credencial não pode ser impressa pois a data de vencimento informada é maior que a validade do " + c.Curso.Titulo);
            });

            if (credencial.AcessoAreaManobra && (credencial.Pessoa.Curso == null || (credencial.Pessoa.Curso != null && !credencial.Pessoa.Curso.Any(c => c.Curso.FlgAcessoAreaManobra))))
                throw new Exception("Esta credencial não pode ser impressa pois esta pessoa não tem o curso necessário para acessar àrea de manobra.");
        }

    }
}

