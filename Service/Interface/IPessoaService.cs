using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPessoaService
    {
        Pessoa ObterPorCPF(string cpf);
        List<Pessoa> ObterPorEmpresa(int idEmpresa);
        Pessoa IncluirNovoRepresentante(Pessoa representante);
        Pessoa ObterPorId(string id);
        List<Pessoa> ObterTodos();
        void Atualizar(Pessoa representante);
        void ExcluirRepresentante(Pessoa representante, int idEmpresa);
    }
}
