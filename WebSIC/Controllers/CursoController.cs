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
    public class CursoController : Controller
    {
        private ICursoService Service;
        private IAreaService AreaService;

        public CursoController(ICursoService service, IAreaService areaService)
        {
            Service = service;
            AreaService = areaService;
        }

        // GET: Curso
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: Curso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = Service.Obter(id.Value);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            ViewBag.Areas = new SelectList(AreaService.Listar(), "IdArea", "Descricao");
            return View();
        }

        // POST: Curso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCurso,Duracao,Observacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Curso curso, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                curso.Criador =
                    curso.Atualizador = User.Identity.Name;
                curso.Area = AreaService.Obter(int.Parse(form["Area.IdArea"]));
                var check = Service.Incluir(curso);
                if (check != null)
                    return RedirectToAction("Index");
            }

            return View(curso);
        }

        // GET: Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = Service.Obter(id.Value);
            ViewBag.Areas = new SelectList(AreaService.Listar(), "IdArea", "Descricao", curso.Area.IdArea);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Curso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCurso,Duracao,Observacao,DataVencimento,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Curso curso, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                curso.Atualizacao = DateTime.Now;
                curso.Atualizador = User.Identity.Name;
                curso.Area = AreaService.Obter(int.Parse(form["Area.IdArea"]));
                var check = Service.Atualizar(curso);
                if (check != null)
                    return RedirectToAction("Index");
            }
            return View(curso);
        }

        // GET: Curso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = Service.Obter(id.Value);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var check = Service.Excluir(id);
            if (check != 0)
                return RedirectToAction("Index");

            return View(id);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
