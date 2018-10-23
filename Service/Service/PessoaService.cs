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

        public PessoaService(IPessoaRepository _PessoaRepository,
                                ICursoRepository _CursoRepository)
        {
            PessoaRepository = _PessoaRepository;
            CursoRepository = _CursoRepository;
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
            PessoaRepository.Incluir(pessoa);

            if (!String.IsNullOrEmpty(pessoa.CNH))
            {
                Curso DDA = CursoRepository.ObterTodos().Where(x => x.Titulo.Contains("DDA")).SingleOrDefault();
                pessoa.Curso.Add(new CursoSemTurma()
                {
                    Curso = DDA,
                    DataValidade = DateTime.Now
                });
            }

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
    }
}
