using Entity.DTO;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ICredencialRepository
    {
        Credencial ObterPorEmpresaPessoaTipoEmissao(int idEmpresa, int idPessoa, bool flgTemporario);
        void IncluirNovaCredencial(Credencial credencial);
        Credencial ObterPorId(int id);
        List<Credencial> ObterTodos();
        void Atualizar(Credencial obj);
        void Remover(Credencial obj);
        void Dispose();
        void Salvar();

        List<Credencial> ObterTodasCredenciaisAtivasDeFuncionario();

        Credencial ObterPorVeiculo(int veiculoId, bool isTemp);
        List<Credencial> ObterATIVs();
    }
}
