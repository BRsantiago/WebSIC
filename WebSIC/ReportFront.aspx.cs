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
                CrystalReportViewer1.ReportSource = Session["TipoCredencial"].ToString() == "ATIV" ? this.GerarPreviewATIV() : this.GerarPreviewCredencial();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void InitializeComponent()
        {

        }

        private ReportDocument GerarPreviewCredencial()
        {
            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath("Credenciais") + "/" + Session["ArquivoCrachaFrotal"].ToString());

            report.SetParameterValue("ImgFundoPath", Server.MapPath("Images/FundoCracha") + "/" + Session["ImgFundoCracha"].ToString());
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
            report.SetParameterValue("TipoCracha", Session["TipoCracha"].ToString());
            report.SetParameterValue("Nombre", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("RG", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("CPF", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("Empresa", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("Matricula", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("Emergencia", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("Fecha", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("SegundaVia", Session["Nome"].ToString(), "CardBack.rpt");
            report.SetParameterValue("Logo", (Server.MapPath("Images/Logo") + "/" + Session["PathLogoBack"].ToString()), "CardBack.rpt");

            return report;
        }

        private ReportDocument GerarPreviewATIV()
        {
            ReportDocument report = new ReportDocument();
            report.Load(Server.MapPath("Credenciais") + "/" + Session["Arquivo"].ToString());

            report.SetParameterValue("Aeroporto", Session["SiglaAeroporto"].ToString());
            report.SetParameterValue("TipoEmissao", Session["TipoEmissao"].ToString());
            report.SetParameterValue("NivelAcesso", Session["AreaDeAcesso"].ToString());
            report.SetParameterValue("DataValidade", Session["DataValidade"].ToString());
            
            report.SetParameterValue("Portao", Session["PortaoDeAcesso"].ToString());
            report.SetParameterValue("Categoria", Session["Categoria"].ToString());
            report.SetParameterValue("Placa", Session["Placa"].ToString());
            report.SetParameterValue("Chassi", Session["Chassi"].ToString());

            report.SetParameterValue("Empresa", Session["Empresa"].ToString());
            report.SetParameterValue("MarcaModelo", Session["MarcaModelo"].ToString());
            report.SetParameterValue("Cor", Session["Cor"].ToString());
            report.SetParameterValue("Matricula", Session["Matricula"].ToString());
            report.SetParameterValue("Chassi", Session["Chassi"].ToString());
            report.SetParameterValue("TipoServico", Session["TipoServico"].ToString());
            report.SetParameterValue("DataExpedicao", Session["DataExpedicao"].ToString());
            report.SetParameterValue("AreaManobra", Session["AreaManobra"].ToString());
            report.SetParameterValue("Logo", Session["Logo"].ToString());

            return report;
        }
    }
}