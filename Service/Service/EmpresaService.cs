using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class EmpresaService : IEmpresaService
    {
        public IEmpresaRepository EmpresaRepository { get; set; }

        public EmpresaService(IEmpresaRepository _EmpresaRepository)
        {
            EmpresaRepository = _EmpresaRepository;
        }

        public List<Empresa> ObterTodos()
        {
            return EmpresaRepository.ObterTodos();
        }

        public Empresa ObterPorId(int id)
        {
            return EmpresaRepository.ObterPorId(id);
        }

        public void IncluirNovaEmpresa(Empresa empresa)
        {
            EmpresaRepository.Incluir(empresa);
            EmpresaRepository.Salvar();
        }

        public void AtualizarNovaEmpresa(Empresa empresa)
        {
            EmpresaRepository.Atualizar(empresa);
            EmpresaRepository.Salvar();
        }

        public void ExcluirEmpresa(int id)
        {
            Empresa empresa = this.EmpresaRepository.ObterPorId(id);

            this.ValidarParaExcluir(empresa);

            EmpresaRepository.Remover(empresa);
            EmpresaRepository.Salvar();
        }

        private void ValidarParaExcluir(Empresa empresa)
        {
            if (empresa.Credenciais != null && empresa.Credenciais.Any(c => c.DataExpedicao.HasValue))
                throw new Exception("Esta empresa não pode ser excluída pois já existe credencial emitida.");

            if (empresa.Solicitacoes != null && empresa.Solicitacoes.Any())
                throw new Exception("Esta empresa não pode ser excluída pois já existe solicitação de credencial.");

            if (empresa.Veiculos != null && empresa.Veiculos.Any())
                throw new Exception("Esta empresa não pode ser excluída pois existem veículos cadastrados em seu nome.");
        }

        public List<Empresa> ObterPorAeroporto(int idAeroporto)
        {
            List<Empresa> empresas = new List<Empresa>();
            try
            {
                empresas = EmpresaRepository.ObterPorAeroporto(idAeroporto);
            }
            catch (Exception ex)
            {
            }

            return empresas;
        }

        public List<Empresa> GetDataList(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<Empresa> empresas = new List<Empresa>();
            try
            {
                empresas = EmpresaRepository.GetDataFromDatabase(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            }
            catch (Exception ex)
            {
                filteredResultsCount =
                    totalResultsCount = 0;
            }

            return empresas;
        }
    }
}
