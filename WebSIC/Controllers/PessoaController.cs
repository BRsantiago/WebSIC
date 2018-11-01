using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Entity.DTO;
using Entity.Entities;
using Repository.Context;
using Service.Interface;
using WebSIC.Models;

namespace WebSIC.Controllers
{
    public class PessoaController : Controller
    {
        public IPessoaService PessoaService;
        public ICursoSemTurmaService CursoSemTurmaService;

        public PessoaController(IPessoaService _PessoaService,
                                    ICursoSemTurmaService _CursoSemTurmaService)
        {
            PessoaService = _PessoaService;
            CursoSemTurmaService = _CursoSemTurmaService;
        }

        // GET: Pessoa
        public ActionResult Index()
        {
            var pessoas = PessoaService.ObterTodos();
            return View(pessoas);
        }

        // GET: Pessoa/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = PessoaService.ObterPorId(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // GET: Pessoa/Create
        public ActionResult Create()
        {
            ViewBag.Picture = "../../WebImages/person.jpg";

            return View();
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PessoaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Pessoa pessoa = model.MapearParaObjetoDominio();
                pessoa.ImageUrl = "../../WebImages/person.jpg";
                PessoaService.IncluirPessoa(pessoa);
                return RedirectToAction("Edit", new { id = pessoa.IdPessoa });
            }
            return View(model);
        }

        // GET: Pessoa/Edit/5
        public ActionResult Edit(string id)
        {
            //ViewBag.Picture = (Convert.ToString(Session["val"]) != string.Empty) ? "../../WebImages/" + Session["val"].ToString() : "../../WebImages/person.jpg";

            Pessoa pessoa = PessoaService.ObterPorId(id);
            return View(new PessoaViewModel(pessoa));
        }

        // POST: Pessoa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PessoaViewModel model)
        {
            try
            {
                PessoaService.Atualizar(model.MapearParaObjetoDominio());
                //return RedirectToAction("Index");
                var msg = "<script> swal({title: 'Good job!', text: 'Empresa atualizada com sucesso !', icon: 'success', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                var msg = "<script> swal({title: 'Atenção!', text: '" + ex.Message + "', icon: 'warning', button: 'OK!'}) </script>";

                TempData["notification"] = msg;

                //return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
                return PartialView(model);
            }
        }

        // GET: Pessoa/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = PessoaService.ObterPorId(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return PartialView(new PessoaViewModel(pessoa));
        }

        // POST: Pessoa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string IdPessoa)
        {
            try
            {
                Pessoa pessoa = PessoaService.ObterPorId(IdPessoa);
                PessoaService.ExcluirPessoa(pessoa);

                return Json(new
                {
                    success = true,
                    title = "Sucesso",
                    message = "Pessoa excluída com sucesso !"
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

        public ActionResult ObterPessoaPorCPF(string cpf)
        {
            //Here I'll bind the list of cities corresponding to selected state's state id  

            Pessoa pessoa = this.PessoaService.ObterPorCPF(cpf);
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            string result = javaScriptSerializer.Serialize(pessoa);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CriarNovoCST(string id)
        {
            CursoSemTurma cst = new CursoSemTurma();
            cst.Pessoa = new Pessoa() { IdPessoa = Convert.ToInt32(id) };
            return View("../CursoSemTurma/Create", cst);
        }

        // POST: Pessoa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarNovoCST(CursoSemTurma cst)
        {
            try
            {
                CursoSemTurmaService.IncluirNovoCST(cst);
                return Json(new { success = true, title = "Sucesso", message = "Representante cadastrado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ExcluirCursoSemTurma(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoSemTurma cst = CursoSemTurmaService.ObterPorId(Convert.ToInt32(id));
            if (cst == null)
            {
                return HttpNotFound();
            }
            return PartialView("../CursoSemTurma/Delete", cst);
        }

        [HttpPost, ActionName("ExcluirCursoSemTurma")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirCursoSemTurmaConfirmacao(string IdCursoSemTurma)
        {
            try
            {
                CursoSemTurma cst = CursoSemTurmaService.ObterPorId(Convert.ToInt32(IdCursoSemTurma));
                CursoSemTurmaService.Excluir(cst);

                return Json(new { success = true, title = "Sucesso", message = "Curso excluído com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditarCursoSemTurma(string id)
        {
            CursoSemTurma cst = CursoSemTurmaService.ObterPorId(Convert.ToInt32(id));
            return PartialView("../CursoSemTurma/Edit", cst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCursoSemTurma(CursoSemTurma cst)
        {
            try
            {
                CursoSemTurmaService.Atualizar(cst);
                return Json(new { success = true, title = "Sucesso", message = "Registro Atualizado com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
