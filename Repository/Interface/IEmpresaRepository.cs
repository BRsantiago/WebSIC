using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IEmpresaRepository : IRepositoryBase<Empresa>
    {
        List<Empresa> ObterPorAeroporto(int aeroportoId);

        List<Empresa> GetDataFromDatabase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
    }
}
