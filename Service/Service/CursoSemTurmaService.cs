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
        public IPessoaRepository PessoaRepository;
        public ICursoRepository CursoRepository;

        public CursoSemTurmaService(ICursoSemTurmaRepository _CursoSemTurmaRepository,
                                        IPessoaRepository _PessoaRepository,
                                            ICursoRepository _CursoRepository)
        {
            CursoSemTurmaRepository = _CursoSemTurmaRepository;
            PessoaRepository = _PessoaRepository;
            CursoRepository = _CursoRepository;
        }

        public void Atualizar(CursoSemTurma cst)
        {
            CursoSemTurma cstBase = this.CursoSemTurmaRepository.ObterPorId(cst.IdCursoSemTurma);

            //cstBase.Pessoa = this.PessoaRepository.ObterPorId(cst.PessoaId.Value);
            //cstBase.Curso = this.CursoRepository.ObterPorId(cst.CursoId.Value);
            cstBase.DataValidade = cst.DataValidade;

            this.Validar(cst);
            CursoSemTurmaRepository.Atualizar(cstBase);
            CursoSemTurmaRepository.Salvar();
        }

        public void Excluir(CursoSemTurma cst)
        {
            CursoSemTurmaRepository.Remover(cst);
            CursoSemTurmaRepository.Salvar();
        }

        public void Incluir(CursoSemTurma cst)
        {
            //cst.Pessoa = this.PessoaRepository.ObterPorId(cst.PessoaId.Value);
            //cst.Curso = this.CursoRepository.ObterPorId(cst.CursoId.Value);
            this.Validar(cst);
            CursoSemTurmaRepository.Incluir(cst);
            CursoSemTurmaRepository.Salvar();
        }

        public CursoSemTurma ObterPorId(int id)
        {
            return CursoSemTurmaRepository.ObterPorId(id);
        }

        public void Validar(CursoSemTurma cst){
            if (cst.CursoId == null)
                throw new Exception("Favor selecionar um curso.");

            if (cst.PessoaId == null)
                throw new Exception("Pessoa não informada, favor verificar.");

            if (cst.DataValidade == null)
                throw new Exception("Favor informar a data de validade do curso.");
        }
    }
}
