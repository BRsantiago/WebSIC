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
    public class CursoSemTurmaService : ICursoSemTurmaService
    {
        public ICursoSemTurmaRepository CursoSemTurmaRepository;

        public CursoSemTurmaService(ICursoSemTurmaRepository _CursoSemTurmaRepository)
        {
            CursoSemTurmaRepository = _CursoSemTurmaRepository;
        }

        public void Atualizar(CursoSemTurma cst)
        {
            CursoSemTurmaRepository.Atualizar(cst);
            CursoSemTurmaRepository.Salvar();
        }

        public void Excluir(CursoSemTurma cst)
        {
            CursoSemTurmaRepository.Remover(cst);
            CursoSemTurmaRepository.Salvar();
        }

        public void IncluirNovoCST(CursoSemTurma cst)
        {
            CursoSemTurmaRepository.IncluirNovoCursoSemTurma(cst);
            CursoSemTurmaRepository.Salvar();
        }

        public CursoSemTurma ObterPorId(int id)
        {
            return CursoSemTurmaRepository.ObterAgregacaoPorId(id);
        }
    }
}
