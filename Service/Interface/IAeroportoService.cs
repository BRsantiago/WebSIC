using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAeroportoService
    {
        List<Aeroporto> ObterTodos();
        Aeroporto ObterPorId(int idAeroporto);
        void Incluir(Aeroporto aeroporto);
        void Atualizar(Aeroporto aeroporto);
        void Excluir(int idAeroporto);
    }
}
