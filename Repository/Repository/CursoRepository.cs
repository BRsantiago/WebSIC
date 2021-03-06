﻿using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository.Repository
{
    public class CursoRepository : RepositoryBase<Curso>, ICursoRepository
    {
        public CursoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override List<Curso> ObterTodos()
        {
            return contexto.Cursos.Include(c => c.Areas).Where(c => c.Ativo == true).ToList();
        }

        public override Curso ObterPorId(int id)
        {
            return contexto.Cursos.Include(c => c.Areas).Include(c => c.Turmas).FirstOrDefault(c => c.IdCurso == id);
        }

        public override void Incluir(Curso obj)
        {
            contexto.Entry(obj.Areas).State = EntityState.Modified;
            contexto.Entry(obj).State = EntityState.Added;
            base.Incluir(obj);
        }

        public List<Curso> ObterPorArea(int idArea)
        {
            return contexto.Cursos
                           .Include(c => c.Turmas)
                           .Include(c => c.CursosSemTurma)
                           .Include(c => c.RamosDeAtividade)
                           .Where(c => c.Areas.Any(a => a.IdArea == idArea) && c.Ativo == true)
                           .ToList();
        }

        public List<Curso> ObterCursosRealizadosComValidadePorIdPessoa(int idPessoa)
        {
            return contexto.Cursos
                           .Where(c =>
                                c.Turmas.Any(t => t.Pessoas.Any(p => p.IdPessoa == idPessoa) /*&& t.DataValidade > DateTime.Now*/) ||
                                c.CursosSemTurma.Any(t => t.Pessoa.IdPessoa == idPessoa /*&& t.DataValidade > DateTime.Now*/))
                           .ToList();
        }

        public List<Curso> ObterPorRamoAtividade(int idRamoAtividade)
        {
            return contexto.Cursos
                           .Include(c => c.Turmas)
                           .Include(c => c.CursosSemTurma)
                           .Include(c => c.RamosDeAtividade)
                           .Where(c => c.RamosDeAtividade.Any(a => a.IdRamoAtividade == idRamoAtividade) && c.Ativo == true)
                           .ToList();
        }
    }
}
