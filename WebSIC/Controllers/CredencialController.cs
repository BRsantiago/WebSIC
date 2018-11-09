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

        // GET: Credencial/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Credencial/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdCredencial,Matricula,FlgMotorista,FlgTemporario,FlgCVE,DataExpedicao,DataDesativacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Credencial credencial)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Credenciais.Add(credencial);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(credencial);
        //}

        // GET: Credencial/Edit/5

        public ActionResult Edit(string id)
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

        // POST: Credencial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCredencial,Matricula,FlgMotorista,FlgTemporario,FlgCVE,DataExpedicao,DataDesativacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Credencial credencial)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(credencial).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(credencial);
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
            Session["DataValidade"] = String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento);
            Session["AreaDeAcesso"] = (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : "");
            Session["Funcao"] = credencial.Cargo.Descricao.ToUpper();
            Session["Foto"] = Server.MapPath(credencial.Pessoa.ImageUrl);
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
            Session["DataExpediacao"] = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            Session["PathLogoBack"] = credencial.FlgCVE ? "logo_vol_emergencia.png" : "logo_ssa_airport.png";
            Session["TipoCredencial"] = "Credencial";
        }

        public JsonResult Imprimir(string idCredencial)
        {
            try
            {
                Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

                credencial.DataExpedicao = DateTime.Now;

                ReportDocument cryRpt = new ReportDocument();
                cryRpt.Load(Server.MapPath("/Credenciais/" + credencial.Empresa.TipoEmpresa.TipoCracha.Arquivo));

                if (!String.IsNullOrEmpty(credencial.Empresa.TipoEmpresa.TipoCracha.ImgFundoCracha))
                    cryRpt.SetParameterValue("ImgFundoCracha", Server.MapPath(credencial.Empresa.TipoEmpresa.TipoCracha.ImgFundoCracha));

                cryRpt.SetParameterValue("Nombre", credencial.NomeImpressaoFrenteCracha.ToUpper());
                cryRpt.SetParameterValue("Fecha", String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento));
                cryRpt.SetParameterValue("Acceso", (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : ""));
                cryRpt.SetParameterValue("Pocision", credencial.Cargo.Descricao.ToUpper());
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
                cryRpt.SetParameterValue("Fecha", String.Format("{0:dd/MM/yyyy}", credencial.DataExpedicao), "CardBack.rpt");
                cryRpt.SetParameterValue("PathLogoBack", Server.MapPath("Images/Logo") + "/" + (credencial.Pessoa.FlgCVE ? "logo_vol_emergencia.png" : "logo_ssa_airport.png"));


                this.CredencialService.Atualizar(credencial);

                cryRpt.ReportClientDocument.PrintOutputController.PrintReport();

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
            Session["TipoEmissao"] = credencial.FlgTemporario ? "Temporário" : "";
            Session["SiglaAeroporto"] = credencial.Aeroporto.IATA;
            Session["AreaDeAcesso"] = (credencial.Area1 != null ? credencial.Area1.Sigla.ToUpper() : " ") + " " + (credencial.Area2 != null ? credencial.Area2.Sigla.ToUpper() : "");
            Session["PortaoDeAcesso"] = credencial.PortaoAcesso.Sigla;
            Session["Categoria"] = credencial.Veiculo.Categoria;
            Session["DataValidade"] = String.Format("{0:dd/MM/yyyy}", credencial.DataVencimento);
            Session["Placa"] = credencial.Veiculo.Placa;

            Session["DataExpediacao"] = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            Session["Empresa"] = credencial.Empresa.NomeFantasia.ToUpper();
            Session["MarcaModelo"] = credencial.Veiculo.Marca;
            Session["Cor"] = credencial.Veiculo.Cor;
            Session["NoRegistro"] = credencial.IdCredencial.ToString().PadLeft(8, '0');
            Session["Chassi"] = credencial.Veiculo.Chassi;
            Session["TipoServico"] = credencial.Veiculo.TipoServico;
        }


        public ActionResult EditATIV(string id)
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

        // POST: Credencial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditATIV([Bind(Include = "IdCredencial,Matricula,FlgMotorista,FlgTemporario,FlgCVE,DataExpedicao,DataDesativacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Credencial credencial)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(credencial).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(credencial);
        }
    }
}

