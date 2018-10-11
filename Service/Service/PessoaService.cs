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
    }
}
