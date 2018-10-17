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
            contratoRepository.Atualizar(contrato);
            contratoRepository.Salvar();
            return contrato;
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

        public void Incluir(Contrato contrato)
        {
            var check = ObterPorNumero(contrato.Numero, false);
            if (check == null)
            {
                contratoRepository.IncluirNovoContrato(contrato);
                contratoRepository.Salvar();
            }
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

        public Contrato ObterPorNumero(string numero, bool withTracking)
        {
            Contrato contrato = null;

            try
            {
                contrato = contratoRepository.ObterPorNumero(numero, withTracking);
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
        public List<Contrato> ObterTodos()
        {
            return contratoRepository.ObterTodos();
        }
    }
}
