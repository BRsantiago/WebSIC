using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity.Entities;
using Repository.Context;
using Service.Interface;

namespace WebSIC.Controllers
{
    public class AeroportoController : Controller
    {
        private IAeroportoService AeroportoService;

        public AeroportoController(IAeroportoService _AeroportoService)
        {
            AeroportoService = _AeroportoService;
        }

        // GET: Aeroporto
        public ActionResult Index()
        {
            List<Aeroporto> retorno = AeroportoService.ObterTodos();
            return View(retorno);
        }

        // GET: Aeroporto/Details/5
        public ActionResult Details(int? id)
        {
            return View(new Aeroporto());
        }

        // GET: Aeroporto/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Aeroporto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aeroporto aeroporto)
        {
            if (ModelState.IsValid)
            {
                AeroportoService.Incluir(aeroporto);
                return RedirectToAction("Index");
            }

            return View(aeroporto);
        }

        // GET: Aeroporto/Edit/5
        public ActionResult Edit(int? id)
        {
            Aeroporto aeroporto = AeroportoService.ObterPorId(id.Value);
            return PartialView(aeroporto);
        }

        // POST: Aeroporto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Aeroporto aeroporto)
        {
            if (ModelState.IsValid)
            {
                AeroportoService.Atualizar(aeroporto);
                return RedirectToAction("Index");
            }
            return View(aeroporto);
        }

        // GET: Aeroporto/Delete/5
        public ActionResult Delete(int? id)
        {
            Aeroporto aeroporto = AeroportoService.ObterPorId(id.Value);
            return PartialView(aeroporto);
        }

        // POST: Aeroporto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                AeroportoService.Excluir(id);
                return Json(new { success = true, title = "Sucesso", message = "Aeroporto excluído com sucesso !" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, title = "Erro", message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
