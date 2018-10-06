using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Service.Interface
{
    public interface ITipoEmpresaService
    {
        List<TipoEmpresa> ObterTodos();
        TipoEmpresa ObterPorId(int id);
        void Incluir(TipoEmpresa tipoEmpresa);
        void Atualizar(TipoEmpresa tipoEmpresa);
        void Excluir(int id);
    }
}
