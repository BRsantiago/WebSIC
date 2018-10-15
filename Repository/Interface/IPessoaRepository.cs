﻿using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IPessoaRepository : IRepositoryBase<Pessoa>
    {
        Pessoa ObterPorCPF(string cpf);
        List<Pessoa> ObterPorEmpresa(int idEmpresa);
        void IncluirNovoRepresentante(Pessoa representante);
    }
}
