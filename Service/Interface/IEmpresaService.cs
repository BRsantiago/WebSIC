using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IEmpresaService
    {
        List<Empresa> ObterTodos();
        Empresa ObterPorId(int id);
        void IncluirNovaEmpresa(Empresa empresa);
        void AtualizarNovaEmpresa(Empresa empresa);
        void ExcluirEmpresa(int id);
    }
}
