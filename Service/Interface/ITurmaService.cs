using Entity.DTO;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITurmaService
    {
        IList<Turma> Listar();
        IList<Turma> ObterPorCurso(int idCurso);
        IList<Turma> ObterProgramadasPorCurso(int idCurso);
        IList<Turma> ObterRealizadasValidasPorCurso(int idCurso);

        Turma Obter(int id);

        ServiceReturn Incluir(Turma turma);
        ServiceReturn Atualizar(Turma turma);
        ServiceReturn Excluir(int id);
    }
}
