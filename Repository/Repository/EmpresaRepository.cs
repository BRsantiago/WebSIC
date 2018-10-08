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
    public class EmpresaRepository : IEmpresaRepository
    {

        public WebSICContext contexto;

        public EmpresaRepository(WebSICContext _contexto)
        {
            contexto = _contexto;
        }

        public void Incluir(Empresa empresa)
        {
            contexto.Entry(empresa.TipoEmpresa).State = System.Data.Entity.EntityState.Unchanged;

            foreach (var aeroporto in empresa.Aeroportos)
            {
                contexto.Entry(aeroporto).State = System.Data.Entity.EntityState.Unchanged;
            }

            contexto.Empresas.Add(empresa);
        }

        public Empresa ObterPorId(int id)
        {
            return contexto.Empresas
                           .Include(e => e.TipoEmpresa)
                           .Include(e => e.Aeroportos)
                           .Where(e => e.IdEmpresa == id)
                           .SingleOrDefault();
        }

        public List<Empresa> ObterTodos()
        {
            return contexto.Set<Empresa>().ToList();
        }

        public virtual void Atualizar(Empresa empresa)
        {
            contexto.Entry(empresa.TipoEmpresa).State = System.Data.Entity.EntityState.Modified;

            //foreach (var aeroporto in empresa.Aeroportos)
            //{
            //    contexto.Entry(aeroporto).State = System.Data.Entity.EntityState.Unchanged;
            //}

            contexto.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Remover(Empresa obj)
        {
            contexto.Set<Empresa>().Remove(obj);
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
