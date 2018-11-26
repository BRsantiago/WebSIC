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
                           .Include(c => c.Pessoa.Curso.Select(cst => cst.Curso))
                           .Include(c => c.Pessoa.Turmas.Select(cst => cst.Curso))
                           .Include(c => c.Area1)
                           .Include(c => c.Area2)
                           .Include(c => c.Empresa.TipoEmpresa.TipoCracha)
                           .Include(c => c.Empresa.Contratos)
                           .Include(c => c.Veiculo)
                           .Include(c => c.Cargo)
                           .Include(c => c.PortaoAcesso)
                           .Include(c => c.Aeroporto)
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
                           .Include(c => c.Aeroporto)
                           .ToList();
        }

        public List<Credencial> ObterTodosParaImpressao()
        {
            return contexto.Credenciais
                           .Include(c => c.Pessoa)
                           .Include(c => c.Area1)
                           .Include(c => c.Area2)
                           .Include(c => c.Empresa)
                           .Include(c => c.Veiculo)
                           .Include(c => c.Aeroporto)
                           .Where(c => c.DataDesativacao == null && c.DataExpedicao == null)
                           .ToList();
        }

        public override void Atualizar(Credencial obj)
        {
            #region

            //if (obj.Aeroporto != null && obj.Aeroporto.IdAeroporto != 0) // && contexto.Entry(obj.Aeroporto).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Aeroporto).State = EntityState.Detached;
            //    obj.Aeroporto = contexto.Aeroportos.Find(obj.AeroportoId);
            //}
            //if (obj.Empresa != null && obj.Empresa.IdEmpresa != 0) // && contexto.Entry(obj.Empresa).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Empresa).State = EntityState.Detached;
            //    obj.Empresa = contexto.Empresas.Find(obj.EmpresaId);
            //}
            //if (obj.Contrato != null && obj.Contrato.IdContrato != 0) // && contexto.Entry(obj.Contrato).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Contrato).State = EntityState.Detached;
            //    obj.Contrato = contexto.Contratos.Find(obj.ContratoId);
            //}
            //if (obj.Veiculo != null && obj.Veiculo.IdVeiculo != 0) // && contexto.Entry(obj.Veiculo).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Veiculo).State = EntityState.Detached;
            //    obj.Veiculo = contexto.Veiculos.Find(obj.VeiculoId);
            //}
            //if (obj.Area1 != null && obj.Area1.IdArea != 0) // && contexto.Entry(obj.Area1).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Area1).State = EntityState.Detached;
            //    obj.Area1 = contexto.Areas.Find(obj.Area1Id);
            //}
            //if (obj.Area2 != null && obj.Area2.IdArea != 0) // && contexto.Entry(obj.Area2).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Area2).State = EntityState.Detached;
            //    obj.Area2 = contexto.Areas.Find(obj.Area2Id);
            //}
            //if (obj.PortaoAcesso != null && obj.PortaoAcesso.IdPortaoAcesso != 0) // && contexto.Entry(obj.PortaoAcesso).State == EntityState.Added
            //{
            //    contexto.Entry(obj.PortaoAcesso).State = EntityState.Detached;
            //    obj.PortaoAcesso = contexto.PortoesAcesso.Find(obj.PortaoAcessoId);
            //}
            //if (obj.Pessoa != null && obj.Pessoa.IdPessoa != 0) // && contexto.Entry(obj.Pessoa).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Pessoa).State = EntityState.Detached;
            //    obj.Pessoa = contexto.Pessoas.Find(obj.PessoaId);
            //}
            //if (obj.Cargo != null && obj.Cargo.IdCargo != 0) // && contexto.Entry(obj.Cargo).State == EntityState.Added
            //{
            //    contexto.Entry(obj.Cargo).State = EntityState.Detached;
            //    obj.Cargo = contexto.Cargos.Find(obj.CargoId);
            //}

            #endregion
            #region
            //if (obj.Aeroporto != null) contexto.Entry(obj.Aeroporto).State = System.Data.Entity.EntityState.Unchanged;
            //if (obj.Empresa != null) contexto.Entry(obj.Empresa).State = System.Data.Entity.EntityState.Unchanged;
            //if (obj.Contrato != null) contexto.Entry(obj.Contrato).State = System.Data.Entity.EntityState.Unchanged;
            //if (obj.Pessoa != null) contexto.Entry(obj.Pessoa).State = System.Data.Entity.EntityState.Unchanged;
            //if (obj.Veiculo != null) contexto.Entry(obj.Veiculo).State = System.Data.Entity.EntityState.Unchanged;
            //if (obj.Area1 != null) contexto.Entry(obj.Area1).State = System.Data.Entity.EntityState.Unchanged;
            //if (obj.Area2 != null) contexto.Entry(obj.Area2).State = System.Data.Entity.EntityState.Unchanged;
            //if (obj.PortaoAcesso != null) contexto.Entry(obj.PortaoAcesso).State = System.Data.Entity.EntityState.Unchanged;
            #endregion
            #region

            //if (obj.AeroportoId > 0) obj.Aeroporto = null; // contexto.Aeroportos.Find(obj.AeroportoId);
            //if (obj.EmpresaId > 0) obj.Empresa = null; //contexto.Empresas.Find(obj.EmpresaId);
            //if (obj.ContratoId > 0) obj.Contrato = null; //contexto.Contratos.Find(obj.ContratoId);
            //if (obj.VeiculoId > 0) obj.Veiculo = null; //contexto.Veiculos.Find(obj.VeiculoId);
            //if (obj.Area1Id > 0) obj.Area1 = null; //contexto.Areas.Find(obj.Area1Id);
            //if (obj.Area2Id > 0) obj.Area2 = null; //contexto.Areas.Find(obj.Area2Id);
            //if (obj.PortaoAcessoId > 0) obj.PortaoAcesso = null; //contexto.PortoesAcesso.Find(obj.PortaoAcessoId);

            #endregion
            #region
            //foreach (var solicitacao in obj.Solicitacoes)
            //    contexto.Entry(solicitacao).State = EntityState.Unchanged;
            #endregion

            if (contexto.Entry(obj).State == EntityState.Detached)
            {
                var existingtObj = contexto.Credenciais.Find(obj.IdCredencial);
                contexto.Entry(existingtObj).CurrentValues.SetValues(obj);
            }

            //base.Atualizar(obj);
        }

        public Credencial ObterPorVeiculo(int veiculoId, bool isTemp)
        {
            Credencial obj =
                contexto.Credenciais
                    .AsNoTracking()
                    //.Include(c => c.Aeroporto)
                    //.Include(c => c.Veiculo)
                    //.Include(c => c.Empresa)
                    //.Include(c => c.Contrato)
                    //.Include(c => c.Area1)
                    //.Include(c => c.Area2)
                    //.Include(c => c.PortaoAcesso)
                    .Where(c => c.Veiculo.IdVeiculo == veiculoId && c.FlgTemporario == isTemp && c.Ativo == true)
                    .OrderByDescending(c => c.Criacao)
                    .FirstOrDefault();

            return obj;
        }

        public List<Credencial> ObterATIVs()
        {
            return
                contexto.Credenciais
                    .Include(c => c.Veiculo)
                    .Include(c => c.Veiculo.Apolice)
                    .Include(c => c.Empresa)
                    .Include(c => c.Contrato)
                    .Include(c => c.Area1)
                    .Include(c => c.Area2)
                    .Include(c => c.PortaoAcesso)
                    .Where(c => c.Veiculo != null && c.Ativo == true && !c.DataDesativacao.HasValue && !c.DataExpedicao.HasValue)
                    .OrderByDescending(c => c.Criacao)
                    .ToList();
        }

        public List<Credencial> ObterTodasCredenciaisAtivasDeFuncionario()
        {
            return contexto.Credenciais
                           .Include(c => c.Pessoa)
                           .Include(c => c.Area1)
                           .Include(c => c.Area2)
                           .Include(c => c.Empresa)
                           .Include(c => c.Aeroporto)
                           .Where(c => !c.DataDesativacao.HasValue && c.Pessoa != null && !c.DataExpedicao.HasValue)
                           .ToList();
        }
    }
}
