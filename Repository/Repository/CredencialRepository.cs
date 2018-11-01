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
            contexto.Entry(credencial).State = System.Data.Entity.EntityState.Modified;
        }

        public void IncluirNovaCredencial(Credencial credencial)
        {

            if (credencial.Pessoa != null) contexto.Entry(credencial.Pessoa).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Area1 != null) contexto.Entry(credencial.Area1).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Area2 != null) contexto.Entry(credencial.Area2).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Empresa != null) contexto.Entry(credencial.Empresa).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Veiculo != null) contexto.Entry(credencial.Veiculo).State = System.Data.Entity.EntityState.Unchanged;

            this.contexto.Credenciais.Add(credencial);
        }

        public Credencial ObterPorEmpresaPessoaTipoEmissao(int idEmpresa, int idPessoa, bool flgTemporario)
        {
            return this.contexto.Credenciais
                                .Where(c => c.Empresa.IdEmpresa == idEmpresa && c.Pessoa.IdPessoa == idEmpresa && c.FlgTemporario == flgTemporario)
                                .SingleOrDefault();
        }
    }
}
