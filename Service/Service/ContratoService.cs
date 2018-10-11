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
        public IContratoRepository ContratoRepository;
        public ContratoService(IContratoRepository _ContratoRepository)
        {
            ContratoRepository = _ContratoRepository;
        }

        public List<Contrato> ObterTodos()
        {
            return ContratoRepository.ObterTodos();
        }
    }
}
