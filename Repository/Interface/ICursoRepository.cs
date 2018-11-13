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
        List<Curso> ObterPorArea(int idArea);
        List<Curso> ObterCursosRealizadosComValidadePorIdPessoa(int idPessoa);
        List<Curso> ObterPorRamoAtividade(int value);
    }
}
