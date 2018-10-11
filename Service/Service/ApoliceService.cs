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

        public Apolice Atualizar(Apolice apolice)
        {
            try
            {
                var check = ObterPorNumero(apolice.Numero);
                if (check != null && check.IdApolice == apolice.IdApolice)
                {
                    apoliceRepository.Atualizar(apolice);
                    apoliceRepository.Salvar();
                    return apolice;
                }
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            Apolice apolice = null;

            try
            {
                apolice = Obter(id);
                apoliceRepository.Remover(apolice);
                apoliceRepository.Salvar();
                return apolice.IdApolice;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public Apolice Incluir(Apolice apolice)
        {
            try
            {
                var check = ObterPorNumero(apolice.Numero);
                if (check == null)
                {
                    apoliceRepository.Incluir(apolice);
                    apoliceRepository.Salvar();
                    return apolice;
                }
            }
            catch (Exception ex)
            { }

            return null;
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

        public IList<Apolice> ObterPorEmpresa(int idEmpresa)
        {
            IList<Apolice> apolices = null;

            try
            {
                apolices = apoliceRepository.ObterPorEmpresa(idEmpresa);
            }
            catch (Exception ex)
            { }

            return apolices;
        }

        public Apolice ObterPorNumero(string numero)
        {
            Apolice apolice = null;

            try
            {
                apolice = apoliceRepository.ObterPorNumero(numero);
            }
            catch (Exception ex)
            { }

            return apolice;
        }
        
        public IList<Apolice> ObterValidas(int idEmpresa)
        {

            IList<Apolice> apolices = null;

            try
            {
                apolices = apoliceRepository.ObterValidas(idEmpresa);
            }
            catch (Exception ex)
            { }

            return apolices;
        }
    }
}
