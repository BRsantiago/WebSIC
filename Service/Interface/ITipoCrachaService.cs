using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITipoCrachaService
    {
        IList<TipoCracha> Listar();
        TipoCracha Obter(int id);
        TipoCracha Incluir(TipoCracha tipoCracha);
        TipoCracha Atualizar(TipoCracha tipoCracha);
        int Excluir(int id);
        TipoCracha ObterTipoCrachaTemporario();
    }
}
