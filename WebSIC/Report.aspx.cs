using CrystalDecisions.CrystalReports.Engine;
using Entity.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebSIC
{
    public partial class Report : System.Web.UI.Page
    {
        public ICredencialService CredencialService;

        public Report(ICredencialService _CredencialService)
        {
            CredencialService = _CredencialService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string idCredencial = string.Empty;

                if (Request.QueryString["IdCredencial"] != null)
                {
                    idCredencial = Request.QueryString["IdCredencial"].ToString();
                }

                Credencial credencial = this.CredencialService.ObterPorId(Convert.ToInt32(idCredencial));

                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("Credenciais") + "/" + credencial.Empresa.TipoEmpresa.TipoCracha.Arquivo);
                //report.SetDatabaseLogon("username", "password", @"server", "database");
                report.SetParameterValue("Nombre", credencial.NomeImpressaoFrenteCracha);
                report.SetParameterValue("Fecha", credencial.DataVencimento);
                report.SetParameterValue("Acceso", credencial.Area1.Sigla + " " + credencial.Area2.Sigla);
                report.SetParameterValue("Pocision", credencial.Cargo.Descricao);
                report.SetParameterValue("FotoPath", Server.MapPath(credencial.Pessoa.ImageUrl));//"WebImages") + "/27.jpg" );
                report.SetParameterValue("Motorista1", credencial.Pessoa.CategoriaUm);
                report.SetParameterValue("Motorista2", credencial.Pessoa.CategoriaDois);
                report.SetParameterValue("EmpresaPath", Server.MapPath(credencial.Empresa.ImageUrl));// "Images") + "/Logo/IE_GLO.jpg");

                if (credencial.Pessoa.FlgCVE)
                {
                    ReportDocument subReport = report.OpenSubreport("CardBackEmergencia.rpt");
                }
                else
                {
                    ReportDocument subReport = report.OpenSubreport("CardBack.rpt");
                }

                report.SetParameterValue("Nombre", credencial.Pessoa.Nome, "CardBack.rpt");
                report.SetParameterValue("RG", credencial.Pessoa.RG, "CardBack.rpt");
                report.SetParameterValue("CPF", credencial.Pessoa.CPF, "CardBack.rpt");
                report.SetParameterValue("Empresa", credencial.Empresa.NomeFantasia, "CardBack.rpt");
                report.SetParameterValue("Matricula", credencial.IdCredencial, "CardBack.rpt");
                report.SetParameterValue("Emergencia", credencial.Pessoa.TelefoneEmergencia, "CardBack.rpt");
                report.SetParameterValue("Fecha", credencial.DataExpedicao, "CardBack.rpt");

                CrystalReportViewer1.ReportSource = report;

                ReportDocument reportBack = new ReportDocument();
                reportBack.Load(Server.MapPath("Credenciais") + "/ClientApp.Reports.CardBack.rpt");

                reportBack.SetParameterValue("Nombre", "teste");
                reportBack.SetParameterValue("RG", "teste");
                reportBack.SetParameterValue("CPF", "teste");
                reportBack.SetParameterValue("Empresa", "teste");
                reportBack.SetParameterValue("Matricula", "teste");
                reportBack.SetParameterValue("Emergencia", "teste");
                reportBack.SetParameterValue("Fecha", "teste");

                CrystalReportViewer2.ReportSource = reportBack;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void InitializeComponent()
        {

        }
    }
}