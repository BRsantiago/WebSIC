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
    public partial class ReportFront : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("Credenciais") + "/" + Session["ArquivoCrachaFrotal"].ToString());
                report.SetParameterValue("Aeroporto", Session["SiglaAeroporto"].ToString());
                report.SetParameterValue("Nombre", Session["NomeFrenteCracha"].ToString());
                report.SetParameterValue("Fecha", Session["DataValidade"].ToString());
                report.SetParameterValue("Acceso", Session["AreaDeAcesso"].ToString());
                report.SetParameterValue("Pocision", Session["Funcao"].ToString());
                report.SetParameterValue("FotoPath", Session["Foto"].ToString());
                report.SetParameterValue("Motorista1", Session["CategoriaMotoristaUm"].ToString());
                report.SetParameterValue("Motorista2", Session["CategoriaMotoristaDois"].ToString());
                report.SetParameterValue("Motorista3", Session["CategoriaMotoristaTres"].ToString());
                report.SetParameterValue("EmpresaPath", Session["LogoEmpresa"].ToString());

                report.SetParameterValue("Nombre", Session["Nome"].ToString(), "CardBack.rpt");
                report.SetParameterValue("RG", Session["Nome"].ToString(), "CardBack.rpt");
                report.SetParameterValue("CPF", Session["Nome"].ToString(), "CardBack.rpt");
                report.SetParameterValue("Empresa", Session["Nome"].ToString(), "CardBack.rpt");
                report.SetParameterValue("Matricula", Session["Nome"].ToString(), "CardBack.rpt");
                report.SetParameterValue("Emergencia", Session["Nome"].ToString(), "CardBack.rpt");
                report.SetParameterValue("Fecha", Session["Nome"].ToString(), "CardBack.rpt");
                report.SetParameterValue("PathLogoBack", Session["PathLogoBack"].ToString(), "CardBack.rpt");

                CrystalReportViewer1.ReportSource = report;
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