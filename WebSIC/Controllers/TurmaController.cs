using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entity.Entities;
using Newtonsoft.Json;
using Repository.Context;
using Service.Interface;

namespace WebSIC.Controllers
{
    //[AllowAnonymous]
    public class TurmaController : Controller
    {
        private ITurmaService Service;
        private ICursoService CursoService;

        public TurmaController(ITurmaService service, ICursoService cursoService)
        {
            Service = service;
            CursoService = cursoService;
        }

        // GET: Turma
        public ActionResult Index()
        {
            return View(Service.Listar());
        }

        // GET: Turma/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = Service.Obter(id.Value);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return PartialView(turma);
        }

        // GET: Turma/Create
        public ActionResult Create()
        {
            ViewBag.Cursos = new SelectList(CursoService.Listar(), "IdCurso", "Titulo");
            return PartialView();
        }

        // POST: Turma/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTurma,Inicio,Fim,Observacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Turma turma, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                turma.Criador =
                    turma.Atualizador = User.Identity.Name;
                turma.Curso = CursoService.Obter(int.Parse(form["Curso.IdCurso"]));
                var check = Service.Incluir(turma);

                return Json(check, JsonRequestBehavior.AllowGet);
            }

            return PartialView(turma);
        }

        // GET: Turma/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = Service.Obter(id.Value);
            ViewBag.Cursos = new SelectList(CursoService.Listar(), "IdCurso", "Titulo", turma.Curso.IdCurso);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return PartialView(turma);
        }

        // POST: Turma/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTurma,Inicio,Fim,Observacao,Criacao,Criador,Atualizacao,Atualizador,Ativo")] Turma turma, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                turma.Atualizacao = DateTime.Now;
                turma.Atualizador = User.Identity.Name;
                turma.Curso = CursoService.Obter(int.Parse(form["Curso.IdCurso"]));
                var check = Service.Atualizar(turma);

                return Json(check, JsonRequestBehavior.AllowGet);
            }
            return PartialView(turma);
        }

        // GET: Turma/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turma turma = Service.Obter(id.Value);
            if (turma == null)
            {
                return HttpNotFound();
            }
            return PartialView(turma);
        }

        // POST: Turma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var check = Service.Excluir(id);
            return Json(check, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
