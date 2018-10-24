using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Entities;

namespace Service.Interface
{
    public interface ICursoSemTurmaService
    {
        CursoSemTurma ObterPorId(int id);
        void Excluir(CursoSemTurma cst);
        void Atualizar(CursoSemTurma cst);
        void IncluirNovoCST(CursoSemTurma cst);
    }
}
