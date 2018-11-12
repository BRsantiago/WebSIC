using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using Entity.Entities;
using Repository.Context;
using Service.Interface;

namespace WebSIC.Controllers
{
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

            return View(lista);
        }

        // GET: Credencial/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(id));
            if (credencial == null)
            {
                return HttpNotFound();
            }
            return View(credencial);
        }

        public ActionResult Edit(string id)
        {
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(id));

            credencial.DataVencimento = this.GerarDataVencimentoCredencial(credencial);

            ViewBag.Printers = GetPrinters();

            return View(credencial);
        }

        private DateTime GerarDataVencimentoCredencial(Credencial credencial)
        {
            List<DateTime> datas = new List<DateTime>();

            credencial.Pessoa.Curso
                             .ToList()
                             .ForEach(c => datas.Add(c.DataValidade));

            credencial.Empresa.Contratos
                              .Where(c => c.FimVigencia > DateTime.Now)
                              .ToList()
                              .ForEach(c => datas.Add(c.FimVigencia));

            if (credencial.Pessoa.DataValidadeCNH.HasValue)
                datas.Add(credencial.Pessoa.DataValidadeCNH.Value);

            return datas.OrderByDescending(x => x.Date).FirstOrDefault();
        }

        // POST: Credencial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Credencial credencial)
        {
            ViewBag.Printers = GetPrinters();

            Credencial credencialBase = this.CredencialService.ObterPorId(credencial.IdCredencial);

            credencialBase.NomeImpressaoFrenteCracha = credencial.NomeImpressaoFrenteCracha;
            credencialBase.DescricaoFuncaoFrenteCracha = credencial.DescricaoFuncaoFrenteCracha;
            credencialBase.DataVencimento = credencial.DataVencimento;

            this.CredencialService.Atualizar(credencialBase);

            return View(credencialBase);

        }

        public void PreviewCredencial(string idCredencial)
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

            Session["SiglaAeroporto"] = credencial.Aeroporto.IATA;
            Session["NomeFrenteCracha"] = credencial.NomeImpressaoFrenteCracha.ToUpper();
            Session["DataValidade"] = String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento.HasValue ? credencial.DataVencimento.Value : this.GerarDataVencimentoCredencial(credencial));
            Session["AreaDeAcesso"] = (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : "");
            Session["Funcao"] = credencial.DescricaoFuncaoFrenteCracha.ToUpper();
            Session["Foto"] = Server.MapPath(credencial.Pessoa.ImageUrl.Replace("../..", ""));
            Session["CategoriaMotoristaUm"] = (credencial.CategoriaMotorista1 == "A" || credencial.CategoriaMotorista2 == "A" ? "A" : "" + credencial.CategoriaMotorista1 == "B" || credencial.CategoriaMotorista2 == "B" ? "B" : "") == "" ? "N" : "N";
            Session["CategoriaMotoristaDois"] = credencial.CategoriaMotorista1 == "D" || credencial.CategoriaMotorista2 == "D" ? "D" : "N";
            Session["CategoriaMotoristaTres"] = credencial.CategoriaMotorista1 == "E" || credencial.CategoriaMotorista2 == "E" ? "E" : "N";
            Session["LogoEmpresa"] = Server.MapPath(credencial.Empresa.ImageUrl);
            Session["Nome"] = credencial.Pessoa.Nome.ToUpper();
            Session["RG"] = credencial.Pessoa.RG;
            Session["CPF"] = credencial.Pessoa.CPF;
            Session["Empresa"] = credencial.Empresa.NomeFantasia.ToUpper();
            Session["Matricula"] = credencial.IdCredencial.ToString().PadLeft(8, '0');
            Session["Emergencia"] = credencial.Pessoa.TelefoneEmergencia;
            Session["DataExpediacao"] = String.Format("{0:dd/MM/yy}", DateTime.Now);
            Session["PathLogoBack"] = credencial.FlgCVE ? "logo_vol_emergencia.png" : "logo_ssa_airport.png";
            Session["TipoCredencial"] = "Credencial";
        }

        public JsonResult Imprimir(string idCredencial, string printerName)
        {
            try
            {
                Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

                credencial.DataExpedicao = DateTime.Now;
                credencial.DataVencimento = credencial.DataVencimento.HasValue ? credencial.DataVencimento.Value : this.GerarDataVencimentoCredencial(credencial);

                ReportDocument cryRpt = new ReportDocument();

                if (credencial.FlgTemporario)
                {
                    TipoCracha temporario = this.TipoCrachaService.ObterTipoCrachaTemporario();

                    cryRpt.Load(Server.MapPath("/Credenciais/" + temporario.Arquivo));
                    cryRpt.SetParameterValue("ImgFundoPath", Server.MapPath(temporario.ImgFundoCracha));
                    cryRpt.SetParameterValue("TipoCracha", temporario.ImgFundoCracha);
                }
                else
                {
                    cryRpt.Load(Server.MapPath("/Credenciais/" + credencial.Empresa.TipoEmpresa.TipoCracha.Arquivo));
                    cryRpt.SetParameterValue("ImgFundoPath", Server.MapPath(credencial.Empresa.TipoEmpresa.TipoCracha.ImgFundoCracha));
                    cryRpt.SetParameterValue("TipoCracha", credencial.Empresa.TipoEmpresa.TipoCracha.Descricao);

                }

                cryRpt.SetParameterValue("Aeroporto", credencial.Aeroporto.IATA.ToUpper());
                cryRpt.SetParameterValue("Nombre", credencial.NomeImpressaoFrenteCracha.ToUpper());
                cryRpt.SetParameterValue("Fecha", String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento.Value));
                cryRpt.SetParameterValue("Acceso", (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : ""));
                cryRpt.SetParameterValue("Pocision", credencial.DescricaoFuncaoFrenteCracha.ToUpper());
                cryRpt.SetParameterValue("FotoPath", Server.MapPath(credencial.Pessoa.ImageUrl));
                cryRpt.SetParameterValue("Motorista1", (credencial.CategoriaMotorista1 == "A" || credencial.CategoriaMotorista2 == "A" ? "A" : "" + credencial.CategoriaMotorista1 == "B" || credencial.CategoriaMotorista2 == "B" ? "B" : "") == "" ? "N" : "N");
                cryRpt.SetParameterValue("Motorista2", credencial.CategoriaMotorista1 == "D" || credencial.CategoriaMotorista2 == "D" ? "D" : "N");
                cryRpt.SetParameterValue("Motorista3", credencial.CategoriaMotorista1 == "E" || credencial.CategoriaMotorista2 == "E" ? "E" : "N");
                cryRpt.SetParameterValue("EmpresaPath", Server.MapPath(credencial.Empresa.ImageUrl));
                cryRpt.SetParameterValue("Nombre", credencial.Pessoa.Nome.ToUpper(), "CardBack.rpt");
                cryRpt.SetParameterValue("RG", credencial.Pessoa.RG, "CardBack.rpt");
                cryRpt.SetParameterValue("CPF", credencial.Pessoa.CPF, "CardBack.rpt");
                cryRpt.SetParameterValue("Matricula", credencial.IdCredencial.ToString().PadLeft(8, '0'), "CardBack.rpt");
                cryRpt.SetParameterValue("Empresa", credencial.Empresa.NomeFantasia.ToUpper(), "CardBack.rpt");
                cryRpt.SetParameterValue("Emergencia", credencial.Pessoa.TelefoneEmergencia, "CardBack.rpt");
                cryRpt.SetParameterValue("Fecha", String.Format("{0:dd/MM/yy}", credencial.DataExpedicao), "CardBack.rpt");
                cryRpt.SetParameterValue("Logo", Server.MapPath("Images/Logo") + "/" + (credencial.FlgCVE ? "logo_vol_emergencia.png" : "logo_ssa_airport.png"));


                cryRpt.PrintOptions.PrinterName = printerName;
                cryRpt.ReportClientDocument.PrintOutputController.PrintReport();

                this.CredencialService.Atualizar(credencial);


                return Json(new { success = true, title = "Sucesso", message = "Registro Atualizado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult IndexATIV()
        {
            return View(CredencialService.ObterATIVs());
        }

        public void PreviewATIV(string idCredencial)
        {
            Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

            ReportDocument report = new ReportDocument();

            Session["Arquivo"] = "ATIV.rpt";
            Session["TipoCredencial"] = "ATIV";
            Session["TipoEmissao"] = credencial.FlgTemporario ? "TEMPORÁRIO" : "";
            Session["SiglaAeroporto"] = credencial.Aeroporto.IATA;
            Session["AreaDeAcesso"] = (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : "");
            Session["PortaoDeAcesso"] = credencial.PortaoAcesso.Sigla;
            Session["Categoria"] = credencial.Veiculo.Categoria;
            Session["DataValidade"] = String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento.HasValue ? credencial.DataVencimento.Value : this.GerarDataVencimentoCredencial(credencial));
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

        public ActionResult ImprimirATIV(string idCredencial, string printerName)
        {
            try
            {
                Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

                credencial.DataExpedicao = DateTime.Now;
                credencial.DataVencimento = credencial.DataVencimento.HasValue ? credencial.DataVencimento.Value : this.GerarDataVencimentoCredencial(credencial);

                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Server.MapPath("/Credenciais/ATIV.rpt"));

                cryRpt.SetParameterValue("TipoEmissao", credencial.FlgTemporario ? "TEMPORÁRIO" : "");
                cryRpt.SetParameterValue("DataValidade", String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento));
                cryRpt.SetParameterValue("NivelAcesso", (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : ""));
                cryRpt.SetParameterValue("Aeroporto", credencial.Aeroporto.IATA);
                cryRpt.SetParameterValue("Portao", credencial.PortaoAcesso.Sigla);
                cryRpt.SetParameterValue("Categoria", credencial.Veiculo.Categoria);
                cryRpt.SetParameterValue("Placa", credencial.Veiculo.Placa);
                cryRpt.SetParameterValue("Empresa", credencial.Empresa.NomeFantasia);
                cryRpt.SetParameterValue("MarcaModelo", credencial.Veiculo.Modelo);
                cryRpt.SetParameterValue("Cor", credencial.Veiculo.Cor);
                cryRpt.SetParameterValue("TipoServico", credencial.Veiculo.TipoServico);
                cryRpt.SetParameterValue("DataExpedicao", credencial.DataExpedicao);
                cryRpt.SetParameterValue("Chassi", credencial.Veiculo.Chassi);
                cryRpt.SetParameterValue("Matricula", credencial.IdCredencial);
                cryRpt.SetParameterValue("AreaManobra", credencial.Veiculo.AcessoManobra ? "ÁREA DE MANOBRA" : "");
                cryRpt.SetParameterValue("Empresa", credencial.Empresa.NomeFantasia);
                cryRpt.SetParameterValue("Logo", Server.MapPath(credencial.Empresa.ImageUrl));


                cryRpt.PrintOptions.PrinterName = printerName;
                cryRpt.ReportClientDocument.PrintOutputController.PrintReport();

                this.CredencialService.Atualizar(credencial);

                return Json(new { success = true, title = "Sucesso", message = "Registro Atualizado com sucesso !" }, JsonRequestBehavior.AllowGet);
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

        // POST: Credencial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditATIV([Bind(Include = "IdCredencial,Matricula,FlgMotorista,FlgTemporario,FlgCVE,DataExpedicao,DataDesativacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Credencial credencial)
        {
            ViewBag.Printers = GetPrinters();

            if (ModelState.IsValid)
            {
                //db.Entry(credencial).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(credencial);
        }

        private List<SelectListItem> GetPrinters()
        {
            System.Drawing.Printing.PrinterSettings.StringCollection printersList = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            List<SelectListItem> list = new List<SelectListItem>();

            int i = 1;
            foreach (string printer in printersList)
            {
                list.Add(new SelectListItem { Text = printer, Value = i.ToString() });
            }

            return list;
        }
    }
}

