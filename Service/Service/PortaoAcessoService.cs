using Entity.DTO;
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

        public ServiceReturn Atualizar(PortaoAcesso portaoAcesso)
        {
            try
            {
                portaoRepository.Atualizar(portaoAcesso);
                portaoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Portão de Acesso atualizado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar o portão de acesso! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            PortaoAcesso portaoAcesso = null;

            try
            {
                portaoAcesso = portaoRepository.ObterPorId(id);
                portaoRepository.Remover(portaoAcesso);
                portaoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Portão de Acesso deletado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar o portão de acesso! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(PortaoAcesso portaoAcesso)
        {
            try
            {
                portaoRepository.Incluir(portaoAcesso);
                portaoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Portão de Acesso cadastrado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar o portão de acesso! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
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
