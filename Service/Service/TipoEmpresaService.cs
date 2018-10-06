using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class TipoEmpresaService : ITipoEmpresaService
    {
        public ITipoEmpresaRepository TipoEmpresaRepository { get; set; }

        public TipoEmpresaService(ITipoEmpresaRepository _TipoEmpresaRepository)
        {
            TipoEmpresaRepository = _TipoEmpresaRepository;
        }

        public List<TipoEmpresa> ObterTodos()
        {
            return TipoEmpresaRepository.ObterTodos();
        }

        public TipoEmpresa ObterPorId(int id)
        {
            return TipoEmpresaRepository.ObterPorId(id);
        }

        public void Incluir(TipoEmpresa entidade)
        {
            TipoEmpresaRepository.Incluir(entidade);
            TipoEmpresaRepository.Salvar();
        }

        public void Atualizar(TipoEmpresa entidade)
        {
            TipoEmpresaRepository.Atualizar(entidade);
            TipoEmpresaRepository.Salvar();
        }

        public void Excluir(int id)
        {
            TipoEmpresa entidade = this.ObterPorId(id);
            TipoEmpresaRepository.Remover(entidade);
            TipoEmpresaRepository.Salvar();
        }


    }
}
