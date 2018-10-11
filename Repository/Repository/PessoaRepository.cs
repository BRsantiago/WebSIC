﻿using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public Pessoa ObterPorCPF(string cpf)
        {
            return this.contexto.Pessoas.Where(p => p.CPF.Contains(cpf)).SingleOrDefault();
        }
    }
}
