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
        Turma Incluir(Turma turma);
        Turma Atualizar(Turma turma);
        int Excluir(int id);
    }
}
