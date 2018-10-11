using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPortaoAcessoService
    {
        IList<PortaoAcesso> Listar();
        PortaoAcesso Obter(int id);
        PortaoAcesso Incluir(PortaoAcesso portao);
        PortaoAcesso Atualizar(PortaoAcesso portao);
        int Excluir(int id);
    }
}
