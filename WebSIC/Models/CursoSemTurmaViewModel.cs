using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class CursoSemTurmaViewModel
    {
        public int IdCursoSemTurma { get; set; }

        [Display(Name = "Data de Validade")]
        public DateTime DataValidade { get; set; }

        public List<Curso> Cursos { get; set; }
        public Nullable<int> CursoId { get; set; }

        public Nullable<int> PessoaId { get; set; }

        public CursoSemTurma MapearParaObjetoDeDominio()
        {
            CursoSemTurma cst = new CursoSemTurma();

            cst.IdCursoSemTurma = this.IdCursoSemTurma;
            cst.DataValidade = this.DataValidade;
            cst.CursoId = this.CursoId;
            cst.PessoaId = this.PessoaId;

            return cst;
        }

        public CursoSemTurmaViewModel()
        {

        }

        public CursoSemTurmaViewModel(CursoSemTurma cst)
        {
            this.IdCursoSemTurma = cst.IdCursoSemTurma;
            this.DataValidade = cst.DataValidade;
            this.CursoId = cst.CursoId;
            this.PessoaId = cst.PessoaId;
        }
    }
}