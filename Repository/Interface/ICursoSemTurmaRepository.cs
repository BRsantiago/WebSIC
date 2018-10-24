﻿using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICursoSemTurmaRepository : IRepositoryBase<CursoSemTurma>
    {
        void IncluirNovoCursoSemTurma(CursoSemTurma cst);
        CursoSemTurma ObterAgregacaoPorId(int id);
    }
}