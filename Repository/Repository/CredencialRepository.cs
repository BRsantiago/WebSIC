using Entity.DTO;
using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CredencialRepository : RepositoryBase<Credencial>, ICredencialRepository
    {
        public CredencialRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public void AtualizarCredencial(Credencial credencial)
        {
            throw new NotImplementedException();
        }

        public void IncluirNovaCredencial(Credencial credencial)
        {
            throw new NotImplementedException();
        }

        public Credencial ObterPorEmpresaPessoaTipoEmissao(int idEmpresa, int idPessoa, bool flgTemporario)
        {
            return this.contexto.Credenciais
                                .Where(c => c.Empresa.IdEmpresa == idEmpresa && c.Pessoa.IdPessoa == idEmpresa && c.FlgTemporario == flgTemporario)
                                .SingleOrDefault();
        }
    }
}
