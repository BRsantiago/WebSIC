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
    public class AeroportoService : IAeroportoService
    {
        public IAeroportoRepository AeroportoRepository { get; set; }

        public AeroportoService(IAeroportoRepository _AeroportoRepository)
        {
            AeroportoRepository = _AeroportoRepository;
        }

        public List<Aeroporto> ObterTodos()
        {
            return AeroportoRepository.ObterTodos();
        }

        public Aeroporto ObterPorId(int id)
        {
            return AeroportoRepository.ObterPorId(id);
        }
    }
}
