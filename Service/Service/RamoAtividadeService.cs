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
    public class RamoAtividadeService : IRamoAtividadeService
    {
        private IRamoAtividadeRepository repository;

        public RamoAtividadeService(IRamoAtividadeRepository _repository)
        {
            repository = _repository;
        }

        public List<RamoAtividade> ObterTodos()
        {
            return repository.ObterTodos();
        }

        public RamoAtividade ObterPorId(int id)
        {
            return repository.ObterPorId(id);
        }
    }
}
