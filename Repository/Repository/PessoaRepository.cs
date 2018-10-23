using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository.Repository
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public Pessoa ObterPorIdPessoa(int idPessoa)
        {
            return contexto.Pessoas
                           .Include(p => p.Empresas)
                           .Include(p => p.Solicitacaos.Select(s => s.TipoSolicitacao))
                           .Include(p => p.Solicitacaos.Select(s => s.Area1))
                           .Include(p => p.Solicitacaos.Select(s => s.Area2))
                           .Include(p => p.Curso.Select(c => c.Curso))
                           .Where(p => p.IdPessoa == idPessoa).SingleOrDefault();
        }

        public Pessoa ObterPorCPF(string cpf)
        {
            return this.contexto.Pessoas.Where(p => p.CPF.Contains(cpf)).SingleOrDefault();
        }

        public List<Pessoa> ObterPorEmpresa(int idEmpresa)
        {
            return this.contexto.Pessoas.Where(p => p.Empresas.Any(e => e.IdEmpresa == idEmpresa)).ToList();
        }

        public void IncluirNovoRepresentante(Pessoa representante)
        {
            representante.Empresas.ToList().ForEach(empresa =>
            {
                contexto.Entry(empresa).State = System.Data.Entity.EntityState.Unchanged;
            });

            contexto.Pessoas.Add(representante);
        }

        public void AtualizarRepresentante(Pessoa representante)
        {
            //representante.Empresas.ToList().ForEach(empresa =>
            //{
            //    contexto.Entry(empresa).State = System.Data.Entity.EntityState.Unchanged;
            //});

            contexto.Entry(representante).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
