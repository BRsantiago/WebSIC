using Entity.Entities;
using Repository.Context;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public IList<Veiculo> ObterPorAno(string ano)
        {
            return contexto.Veiculos.Include("Empresa").Include("Apolice").Where(v => v.AnoFabricacao == ano || v.AnoModelo == ano).ToList();
        }

        public IList<Veiculo> ObterPorApolice(int idApolice)
        {
            return contexto.Veiculos.Include("Empresa").Include("Apolice").Where(v => v.Apolice.IdApolice == idApolice).ToList();
        }

        public Veiculo ObterPorChassi(string chassi, bool withTracking)
        {
            return withTracking
                ? contexto.Veiculos.Include("Empresa").Include("Apolice").Where(v => v.Chassi == chassi).FirstOrDefault()
                : contexto.Veiculos.AsNoTracking().Include("Empresa").Include("Apolice").Where(v => v.Chassi == chassi).FirstOrDefault();
        }

        public IList<Veiculo> ObterPorEmpresa(int idEmpresa)
        {
            return contexto.Veiculos.Include("Empresa").Include("Apolice").Where(v => v.Empresa.IdEmpresa == idEmpresa).ToList();
        }

        public IList<Veiculo> ObterPorMarca(string marca)
        {
            return contexto.Veiculos.Include("Empresa").Include("Apolice").Where(v => v.Marca == marca).ToList();
        }

        public IList<Veiculo> ObterPorModelo(string modelo)
        {
            return contexto.Veiculos.Include("Empresa").Include("Apolice").Where(v => v.Modelo == modelo).ToList();
        }

        public Veiculo ObterPorPlaca(string placa, bool withTracking)
        {
            return withTracking
                ? contexto.Veiculos.Include("Empresa").Include("Apolice").Where(v => v.Placa == placa).FirstOrDefault()
                : contexto.Veiculos.AsNoTracking().Include("Empresa").Include("Apolice").Where(v => v.Placa == placa).FirstOrDefault();
        }
    }
}
