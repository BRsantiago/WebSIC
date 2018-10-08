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
    public class ContratoService : IContratoService
    {
        private IContratoRepository contratoRepository;

        public ContratoService(IContratoRepository repository)
        {
            contratoRepository = repository;
        }

        public Contrato Atualizar(Contrato contrato)
        {
            try
            {
                var check = ObterPorNumero(contrato.Numero);
                if (check != null && check.IdContrato == contrato.IdContrato)
                {
                    contratoRepository.Atualizar(contrato);
                    contratoRepository.Salvar();
                    return contrato;
                }
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            Contrato contrato = null;

            try
            {
                contrato = Obter(id);
                contratoRepository.Remover(contrato);
                contratoRepository.Salvar();
                return contrato.IdContrato;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public Contrato Incluir(Contrato contrato)
        {
            try
            {
                var check = ObterPorNumero(contrato.Numero);
                if (check == null)
                {
                    contratoRepository.Incluir(contrato);
                    contratoRepository.Salvar();
                    return contrato;
                }
            }
            catch (Exception ex)
            { }

            return null;
        }

        public IList<Contrato> Listar()
        {
            IList<Contrato> contratos = null;

            try
            {
                contratos = contratoRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return contratos;
        }

        public Contrato Obter(int id)
        {
            Contrato contrato = null;

            try
            {
                contrato = contratoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return contrato;
        }

        public IList<Contrato> ObterPorEmpresa(int idEmpresa)
        {
            IList<Contrato> contratos = null;

            try
            {
                contratos = contratoRepository.ObterPorEmpresa(idEmpresa);
            }
            catch (Exception ex)
            { }

            return contratos;
        }

        public Contrato ObterPorNumero(string numero)
        {
            Contrato contrato = null;

            try
            {
                contrato = contratoRepository.ObterPorNumero(numero);
            }
            catch (Exception ex)
            { }

            return contrato;
        }

        public IList<Contrato> ObterPorPeriodo(int idEmpresa, DateTime inicioVigencia, DateTime finalVigencia)
        {
            IList<Contrato> contratos = null;

            try
            {
                contratos = contratoRepository.ObterPorPeriodo(idEmpresa, inicioVigencia, finalVigencia);
            }
            catch (Exception ex)
            { }

            return contratos;
        }

        public IList<Contrato> ObterVigentes(int idEmpresa)
        {
            IList<Contrato> contratos = null;

            try
            {
                contratos = contratoRepository.ObterVigentes(idEmpresa);
            }
            catch (Exception ex)
            { }

            return contratos;
        }
    }
}
