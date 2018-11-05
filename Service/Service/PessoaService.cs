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
    public class PessoaService : IPessoaService
    {
        public IPessoaRepository PessoaRepository;
        public ICursoRepository CursoRepository;
        public ICursoSemTurmaRepository CSTRepository;

        public PessoaService(IPessoaRepository _PessoaRepository,
                                ICursoRepository _CursoRepository,
                                ICursoSemTurmaRepository _CSTRepository)
        {
            PessoaRepository = _PessoaRepository;
            CursoRepository = _CursoRepository;
            CSTRepository = _CSTRepository;
        }

        public Pessoa ObterPorCPF(string cpf)
        {
            return this.PessoaRepository.ObterPorCPF(cpf);
        }

        public List<Pessoa> ObterPorEmpresa(int idEmpresa)
        {
            return this.PessoaRepository.ObterPorEmpresa(idEmpresa);
        }

        public void IncluirPessoa(Pessoa pessoa)
        {
            this.IncluirCursoDDA(pessoa);
            PessoaRepository.IncluirNovaPessoa(pessoa);
            PessoaRepository.Salvar();
        }

        public Pessoa IncluirNovoRepresentante(Pessoa representante)
        {
            try
            {
                PessoaRepository.IncluirNovoRepresentante(representante);
                PessoaRepository.Salvar();

                return representante;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public Pessoa ObterPorId(string id)
        {
            return this.PessoaRepository.ObterPorIdPessoa(Convert.ToInt32(id));
        }

        public List<Pessoa> ObterTodos()
        {
            return this.PessoaRepository.ObterTodos();
        }

        public void Atualizar(Pessoa representante)
        {
            IncluirCursoDDA(representante);
            PessoaRepository.AtualizarRepresentante(representante);
            PessoaRepository.Salvar();
        }

        public void ExcluirRepresentante(Pessoa representante, int idEmpresa)
        {
            representante.Empresas.Remove(representante.Empresas.Where(e => e.IdEmpresa == idEmpresa).SingleOrDefault());
            this.Atualizar(representante);
        }

        public void ExcluirPessoa(Pessoa pessoa)
        {
            PessoaRepository.Remover(pessoa);
            PessoaRepository.Salvar();
        }

        private void IncluirCursoDDA(Pessoa pessoa)
        {
            if (pessoa.Curso == null || (pessoa.Curso != null && !pessoa.Curso.Any(c => c.Curso.PermiteDirigirEmAreasRestritas)) && !String.IsNullOrEmpty(pessoa.CNH))
            {
                Curso DDA = CursoRepository.ObterTodos().Where(x => x.PermiteDirigirEmAreasRestritas).SingleOrDefault();
                pessoa.Curso = new List<CursoSemTurma>();
                CursoSemTurma cst = new CursoSemTurma() { Curso = DDA, DataValidade = DateTime.Now, Pessoa = pessoa };


                pessoa.Curso.Add(cst);

            }
        }

    }
}
