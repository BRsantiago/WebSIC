using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICredencialService
    {
        List<Credencial> ObterTodos();
        Credencial ObterPorId(int v);
        List<Credencial> ObterTodasCredenciaisAtivasDeFuncionario();
        void Atualizar(Credencial credencial);

        List<Credencial> ObterATIVs();
    }
}
