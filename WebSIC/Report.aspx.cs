using CrystalDecisions.CrystalReports.Engine;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ReportDocument report = new ReportDocument();
                report.Load(Server.MapPath("Credenciais") + "/ClientApp.Reports.Employe.VinciFront.rpt");
                //report.SetDatabaseLogon("username", "password", @"server", "database");
                report.SetParameterValue("Nombre", "teste");
                report.SetParameterValue("Fecha", "teste");
                report.SetParameterValue("Acceso", "teste");
                report.SetParameterValue("Pocision", "teste");
                report.SetParameterValue("FotoPath", "teste");
                report.SetParameterValue("Motorista1", "teste");
                report.SetParameterValue("Motorista2", "teste");
                report.SetParameterValue("EmpresaPath", "teste");

                //report.SetParameterValue("Nombre", "teste");
                //report.SetParameterValue("RG", "teste");
                //report.SetParameterValue("CPF", "teste");
                //report.SetParameterValue("Empresa", "teste");
                //report.SetParameterValue("Matricula", "teste");
                //report.SetParameterValue("Emergencia", "teste");
                //report.SetParameterValue("Expedicion", "teste");


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