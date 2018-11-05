using Entity.DTO;
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
    public class CredencialRepository : ICredencialRepository
    {
        WebSICContext contexto;

        public CredencialRepository(WebSICContext _contexto)
        {
            contexto = _contexto;
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

        public Credencial ObterPorId(int id)
        {
            return contexto.Credenciais
                           .Include(c => c.Pessoa)
                           .Include(c => c.Area1)
                           .Include(c => c.Area2)
                           .Include(c => c.Empresa)
                           .Include(c => c.Veiculo)
                           .Where(c => c.IdCredencial == id)
                           .SingleOrDefault();
        }

        public List<Credencial> ObterTodos()
        {
            return contexto.Credenciais
                           .Include(c => c.Pessoa)
                           .Include(c => c.Area1)
                           .Include(c => c.Area2)
                           .Include(c => c.Empresa)
                           .Include(c => c.Veiculo)
                           .ToList();
        }

        public void Atualizar(Credencial obj)
        {
            contexto.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public void Remover(Credencial obj)
        {
            contexto.Credenciais.Remove(obj);
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public void Salvar()
        {
            contexto.SaveChanges();
        }
    }
}
