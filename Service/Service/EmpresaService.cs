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
    public class EmpresaService : IEmpresaService
    {
        public IEmpresaRepository EmpresaRepository { get; set; }

        public EmpresaService(IEmpresaRepository _EmpresaRepository)
        {
            EmpresaRepository = _EmpresaRepository;
        }

        public List<Empresa> ObterTodos()
        {
            return this.EmpresaRepository.ObterTodos();
        }
    }
}
