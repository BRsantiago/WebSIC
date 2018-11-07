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
    public partial class ReportBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                //CODIGO PARA TESTE
                ReportDocument reportBack = new ReportDocument();
                reportBack.Load(Server.MapPath("Credenciais") + "/ClientApp.Reports.CardBack.rpt");

                reportBack.SetParameterValue("Nombre", Session["Nome"].ToString());
                reportBack.SetParameterValue("RG", Session["RG"].ToString());
                reportBack.SetParameterValue("CPF", Session["CPF"].ToString());
                reportBack.SetParameterValue("Empresa", Session["Empresa"].ToString());
                reportBack.SetParameterValue("Matricula", Session["Matricula"].ToString());
                reportBack.SetParameterValue("Emergencia", Session["Emergencia"].ToString());
                reportBack.SetParameterValue("Fecha", Session["DataExpediacao"].ToString());
                reportBack.SetParameterValue("Logo", Server.MapPath("Images/Logo") + "/" + Session["PathLogoBack"].ToString());

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