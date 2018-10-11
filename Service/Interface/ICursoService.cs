using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICursoService
    {
        IList<Curso> Listar();
        IList<Curso> ObterPorArea(int idArea);
        Curso Obter(int id);
        Curso Incluir(Curso curso);
        Curso Atualizar(Curso curso);
        int Excluir(int id);
    }
}
