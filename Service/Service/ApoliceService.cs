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
    public class ApoliceService : IApoliceService
    {
        private IApoliceRepository apoliceRepository;

        public ApoliceService(IApoliceRepository repository)
        {
            apoliceRepository = repository;
        }

        public ServiceReturn Atualizar(Apolice apolice)
        {
            try
            {
                var check = ObterPorNumero(apolice.Numero, false);
                if (check == null || check.IdApolice == apolice.IdApolice)
                {
                    apoliceRepository.Atualizar(apolice);
                    apoliceRepository.Salvar();

                    return new ServiceReturn() { success = true, title = "Sucesso", message = "Apólice atualizada com sucesso!" };
                }

                return new ServiceReturn() { success = false, title = "Erro", message = "Não foi possível atualizar a apólice pois já existe um registro com o mesmo número!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar a apólice! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            Apolice apolice = null;

            try
            {
                apolice = Obter(id);
                apoliceRepository.Remover(apolice);
                apoliceRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Apólice deletada com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar a apólice! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(Apolice apolice)
        {
            try
            {
                var check = ObterPorNumero(apolice.Numero, false);
                if (check == null)
                {
                    apoliceRepository.Incluir(apolice);
                    apoliceRepository.Salvar();

                    return new ServiceReturn() { success = true, title = "Sucesso", message = "Apólice cadastrada com sucesso!" };
                }

                return new ServiceReturn() { success = false, title = "Erro", message = "Não foi possível cadastrar a apólice pois já existe um registro com o mesmo número!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar a apólice! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public IList<Apolice> Listar()
        {
            IList<Apolice> apolices = null;

            try
            {
                apolices = apoliceRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return apolices;
        }

        public Apolice Obter(int id)
        {
            Apolice apolice = null;

            try
            {
                apolice = apoliceRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return apolice;
        }

        public IList<Apolice> ObterPorEmpresa(int idEmpresa, bool withTracking)
        {
            IList<Apolice> apolices = null;

            try
            {
                apolices = apoliceRepository.ObterPorEmpresa(idEmpresa, withTracking);
            }
            catch (Exception ex)
            { }

            return apolices;
        }

        public Apolice ObterPorNumero(string numero, bool withTracking)
        {
            Apolice apolice = null;

            try
            {
                apolice = apoliceRepository.ObterPorNumero(numero, withTracking);
            }
            catch (Exception ex)
            { }

            return apolice;
        }
        
        public IList<Apolice> ObterValidas(int idEmpresa, bool withTracking)
        {

            IList<Apolice> apolices = null;

            try
            {
                apolices = apoliceRepository.ObterValidas(idEmpresa, withTracking);
            }
            catch (Exception ex)
            { }

            return apolices;
        }
    }
}
