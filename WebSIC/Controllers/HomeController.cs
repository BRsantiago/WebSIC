using Entity.Entities;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSIC.Controllers
{
    public class HomeController : Controller
    {
        public IEmpresaService EmpresaService { get; set; }
        public HomeController(IEmpresaService _EmpresaService)
        {
            EmpresaService = _EmpresaService;
        }

        public ActionResult Index()
        {
            List<Empresa> retorno = this.EmpresaService.ObterTodos();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}