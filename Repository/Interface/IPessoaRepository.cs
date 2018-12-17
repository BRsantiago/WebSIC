using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa>
    {
        //Pessoa ObterPorIdPessoa(int idPessoa);
        Pessoa ObterPorCPF(string cpf);
        List<Pessoa> ObterPorEmpresa(int idEmpresa);
        void IncluirNovoRepresentante(Pessoa representante);
        void AtualizarRepresentante(Pessoa representante);
        void IncluirNovaPessoa(Pessoa pessoa);
        bool VerificarSeExistePessoaComMesmoCPF(string cPF, int idPessoa);
        Pessoa ObterPorIdSemAgregacao(int idPessoa);
        bool VerificarSeExistePessoaComMesmoRNE(string RNE, int idPessoa);

        List<Pessoa> GetDataFromDatabase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
    }
}
