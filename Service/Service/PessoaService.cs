using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class PessoaService : IPessoaService
    {
        public IPessoaRepository PessoaRepository;

        public PessoaService(IPessoaRepository _PessoaRepository)
        {
            PessoaRepository = _PessoaRepository;
        }

        public Pessoa ObterPorCPF(string cpf)
        {
            return this.PessoaRepository.ObterPorCPF(cpf);
        }

        public List<Pessoa> ObterPorEmpresa(int idEmpresa)
        {
            return this.PessoaRepository.ObterPorEmpresa(idEmpresa);
        }

        public void IncluirNovoRepresentante(Pessoa representante)
        {
            PessoaRepository.IncluirNovoRepresentante(representante);
            PessoaRepository.Salvar();
        }

        public Pessoa ObterPorId(string id)
        {
            return this.ObterPorId(id);
        }
    }
}
