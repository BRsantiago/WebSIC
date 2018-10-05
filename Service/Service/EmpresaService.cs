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
            Empresa empresa = this.ObterPorId(id);
            EmpresaRepository.Remover(empresa);
            EmpresaRepository.Salvar();
        }


    }
}
