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
    public class CredencialRepository : RepositoryBase<Credencial>, ICredencialRepository
    {
        //WebSICContext contexto;

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

            if (credencial.Empresa != null) contexto.Entry(credencial.Empresa).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Contrato != null) contexto.Entry(credencial.Contrato).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Pessoa != null) contexto.Entry(credencial.Pessoa).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Veiculo != null) contexto.Entry(credencial.Veiculo).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Area1 != null) contexto.Entry(credencial.Area1).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.Area2 != null) contexto.Entry(credencial.Area2).State = System.Data.Entity.EntityState.Unchanged;
            if (credencial.PortaoAcesso != null) contexto.Entry(credencial.PortaoAcesso).State = System.Data.Entity.EntityState.Unchanged;

            this.contexto.Credenciais.Add(credencial);
        }

        public Credencial ObterPorEmpresaPessoaTipoEmissao(int idEmpresa, int idPessoa, bool flgTemporario)
        {
            return this.contexto.Credenciais
                                .Where(c => c.Empresa.IdEmpresa == idEmpresa && c.Pessoa.IdPessoa == idEmpresa && c.FlgTemporario == flgTemporario)
                                .SingleOrDefault();
        }

        public override Credencial ObterPorId(int id)
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

        public override List<Credencial> ObterTodos()
        {
            return contexto.Credenciais
                           .Include(c => c.Pessoa)
                           .Include(c => c.Area1)
                           .Include(c => c.Area2)
                           .Include(c => c.Empresa)
                           .Include(c => c.Veiculo)
                           .ToList();
        }

        public override void Atualizar(Credencial obj)
        {
            if (obj.Contrato != null && obj.Contrato.IdContrato != 0 && contexto.Entry(obj.Contrato).State == EntityState.Added)
            {
                contexto.Entry(obj.Contrato).State = EntityState.Detached;
                obj.Contrato = contexto.Contratos.Find(obj.ContratoId);
            }
            if (obj.Veiculo != null && obj.Veiculo.IdVeiculo != 0 && contexto.Entry(obj.Veiculo).State == EntityState.Added)
            {
                contexto.Entry(obj.Veiculo).State = EntityState.Detached;
                obj.Veiculo = contexto.Veiculos.Find(obj.VeiculoId);
            }
            if (obj.Area1 != null && obj.Area1.IdArea != 0 && contexto.Entry(obj.Area1).State == EntityState.Added)
            {
                contexto.Entry(obj.Area1).State = EntityState.Detached;
                obj.Area1 = contexto.Areas.Find(obj.Area1Id);
            }
            if (obj.Area2 != null && obj.Area2.IdArea != 0 && contexto.Entry(obj.Area2).State == EntityState.Added)
            {
                contexto.Entry(obj.Area2).State = EntityState.Detached;
                obj.Area2 = contexto.Areas.Find(obj.Area2Id);
            }
            if (obj.PortaoAcesso != null && obj.PortaoAcesso.IdPortaoAcesso != 0 && contexto.Entry(obj.PortaoAcesso).State == EntityState.Added)
            {
                contexto.Entry(obj.PortaoAcesso).State = EntityState.Detached;
                obj.PortaoAcesso = contexto.PortoesAcesso.Find(obj.PortaoAcessoId);
            }            

            base.Atualizar(obj);
        }

        //public  void Remover(Credencial obj)
        //{
        //    contexto.Credenciais.Remove(obj);
        //}

        //public void Dispose()
        //{
        //    contexto.Dispose();
        //}

        //public void Salvar()
        //{
        //    contexto.SaveChanges();
        //}

        public Credencial ObterPorVeiculo(int veiculoId, bool isTemp)
        {
            Credencial obj =
                contexto.Credenciais
                    .Include(c => c.Veiculo)
                    .Include(c => c.Empresa)
                    .Include(c => c.Contrato)
                    .Include(c => c.Area1)
                    .Include(c => c.Area2)
                    .Include(c => c.PortaoAcesso)
                    .Where(c => c.Veiculo.IdVeiculo == veiculoId && c.FlgTemporario == isTemp && c.Ativo == true)
                    .OrderByDescending(c => c.Criacao)
                    .FirstOrDefault();

            return obj;
        }
    }
}
