using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICursoRepository : IRepositoryBase<Curso>
    {
        IList<Curso> ObterPorArea(int idArea);
        IList<Curso> ObterCursosRealizadosComValidadePorIdPessoa(int idPessoa);
    }
}
