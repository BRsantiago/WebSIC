using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class PortaoAcessoService : IPortaoAcessoService
    {
        private IPortaoAcessoRepository portaoRepository;

        public PortaoAcessoService(IPortaoAcessoRepository repository)
        {
            portaoRepository = repository;
        }

        public PortaoAcesso Atualizar(PortaoAcesso portaoAcesso)
        {
            try
            {
                portaoRepository.Atualizar(portaoAcesso);
                portaoRepository.Salvar();
                return portaoAcesso;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            PortaoAcesso portaoAcesso = null;

            try
            {
                portaoAcesso = portaoRepository.ObterPorId(id);
                portaoRepository.Remover(portaoAcesso);
                portaoRepository.Salvar();

                return id;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public PortaoAcesso Incluir(PortaoAcesso portaoAcesso)
        {
            try
            {
                portaoRepository.Incluir(portaoAcesso);
                portaoRepository.Salvar();
                return portaoAcesso;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public IList<PortaoAcesso> Listar()
        {
            IList<PortaoAcesso> portaoAcessos = null;

            try
            {
                return portaoRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return portaoAcessos;
        }

        public PortaoAcesso Obter(int id)
        {
            PortaoAcesso portaoAcesso = null;

            try
            {
                portaoAcesso = portaoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return portaoAcesso;
        }
    }
}
