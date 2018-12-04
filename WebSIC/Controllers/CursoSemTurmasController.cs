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

namespace WebSIC.Controllers
{
    public class CursoSemTurmasController : Controller
    {
        private WebSICContext db = new WebSICContext();

        // GET: CursoSemTurmas
        public ActionResult Index()
        {
            var cursosSemTurma = db.CursosSemTurma.Include(c => c.Curso).Include(c => c.Pessoa);
            return View(cursosSemTurma.ToList());
        }

        // GET: CursoSemTurmas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoSemTurma cursoSemTurma = db.CursosSemTurma.Find(id);
            if (cursoSemTurma == null)
            {
                return HttpNotFound();
            }
            return View(cursoSemTurma);
        }

        // GET: CursoSemTurmas/Create
        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "IdCurso", "Titulo");
            ViewBag.PessoaId = new SelectList(db.Pessoas, "IdPessoa", "NomeCompleto");
            return View();
        }

        // POST: CursoSemTurmas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCursoSemTurma,DataValidade,CursoId,PessoaId,Criacao,Criador,Atualizacao,Atualizador,Ativo")] CursoSemTurma cursoSemTurma)
        {
            if (ModelState.IsValid)
            {
                db.CursosSemTurma.Add(cursoSemTurma);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "IdCurso", "Titulo", cursoSemTurma.CursoId);
            ViewBag.PessoaId = new SelectList(db.Pessoas, "IdPessoa", "NomeCompleto", cursoSemTurma.PessoaId);
            return View(cursoSemTurma);
        }

        // GET: CursoSemTurmas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoSemTurma cursoSemTurma = db.CursosSemTurma.Find(id);
            if (cursoSemTurma == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "IdCurso", "Titulo", cursoSemTurma.CursoId);
            ViewBag.PessoaId = new SelectList(db.Pessoas, "IdPessoa", "NomeCompleto", cursoSemTurma.PessoaId);
            return View(cursoSemTurma);
        }

        // POST: CursoSemTurmas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCursoSemTurma,DataValidade,CursoId,PessoaId,Criacao,Criador,Atualizacao,Atualizador,Ativo")] CursoSemTurma cursoSemTurma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cursoSemTurma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "IdCurso", "Titulo", cursoSemTurma.CursoId);
            ViewBag.PessoaId = new SelectList(db.Pessoas, "IdPessoa", "NomeCompleto", cursoSemTurma.PessoaId);
            return View(cursoSemTurma);
        }

        // GET: CursoSemTurmas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CursoSemTurma cursoSemTurma = db.CursosSemTurma.Find(id);
            if (cursoSemTurma == null)
            {
                return HttpNotFound();
            }
            return View(cursoSemTurma);
        }

        // POST: CursoSemTurmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CursoSemTurma cursoSemTurma = db.CursosSemTurma.Find(id);
            db.CursosSemTurma.Remove(cursoSemTurma);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
