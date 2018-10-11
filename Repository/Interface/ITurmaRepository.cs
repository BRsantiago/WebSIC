using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ITurmaRepository : IRepositoryBase<Turma>
    {
        IList<Turma> ObterPorCurso(int idCurso);
        IList<Turma> ObterProgramadasPorCurso(int idCurso);
        IList<Turma> ObterRealizadasValidasPorCurso(int idCurso);
    }
}
