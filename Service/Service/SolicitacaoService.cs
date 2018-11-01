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
    public class SolicitacaoService : ISolicitacaoService
    {
        public ISolicitacaoRepository SolicitacaoRepository;
        public ICursoRepository CursoRepository;
        public ICursoSemTurmaRepository CursoSemTurmaRepository;
        public IPessoaRepository PessoaRepository;
        public IAreaRepository AreaRepository;
        public IContratoRepository ContratoRepository;
        public IEmpresaRepository EmpresaRepository;
        public ITipoSolicitacaoRepository TipoSolicitacaoRepository;
        public ICredencialRepository CredencialRepository;


        public SolicitacaoService(ISolicitacaoRepository _SolicitacaoRepository,
                                    ICursoRepository _CursoRepository,
                                        ICursoSemTurmaRepository _CursoSemTurmaRepository,
                                            IPessoaRepository _PessoaRepository,
                                                IAreaRepository _AreaRepository,
                                                    IContratoRepository _ContratoRepository,
                                                        IEmpresaRepository _EmpresaRepository,
                                                            ITipoSolicitacaoRepository _TipoSolicitacaoRepository,
                                                                ICredencialRepository _CredencialRepository)
        {
            SolicitacaoRepository = _SolicitacaoRepository;
            CursoRepository = _CursoRepository;
            CursoSemTurmaRepository = _CursoSemTurmaRepository;
            PessoaRepository = _PessoaRepository;
            AreaRepository = _AreaRepository;
            ContratoRepository = _ContratoRepository;
            EmpresaRepository = _EmpresaRepository;
            TipoSolicitacaoRepository = _TipoSolicitacaoRepository;
            CredencialRepository = _CredencialRepository;
        }

        public List<Solicitacao> ObterTodos()
        {
            return SolicitacaoRepository.ObterTodos();
        }

        public void Salvar(Solicitacao solicitacao)
        {

            solicitacao.DataAutorizacao = DateTime.Now; //Isto precisar mudar quando pessoas de fora fizerem o cadastro da solicitacao.

            SolicitacaoRepository.IncluirNovaSolicitacao(solicitacao);

            CarregarCursosExigidos(solicitacao);
            GerarCredencial(solicitacao);

            SolicitacaoRepository.Salvar();
        }

        private void GerarCredencial(Solicitacao solicitacao)
        {
            Credencial credencial = this.CredencialRepository.ObterPorEmpresaPessoaTipoEmissao(solicitacao.Empresa.IdEmpresa, solicitacao.Pessoa.IdPessoa, solicitacao.TipoEmissao == 0);

            if (credencial == null)
            {
                credencial = new Credencial();

                credencial.FlgTemporario = solicitacao.TipoEmissao == 0; //Temporário
                credencial.FlgCVE = solicitacao.Pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = solicitacao.Pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = solicitacao.Cargo.Descricao;

                this.CredencialRepository.IncluirNovaCredencial(credencial);
            }
            else
            {
                credencial.FlgCVE = solicitacao.Pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = solicitacao.Pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = solicitacao.Cargo.Descricao;
                credencial.CategoriaMotorista1 = solicitacao.Pessoa.CategoriaUm.ToString();
                credencial.CategoriaMotorista2 = solicitacao.Pessoa.CategoriaUm.ToString();

                if (solicitacao.TipoSolicitacao.Descricao.Contains("BAIXA"))
                {
                    credencial.DataDesativacao = DateTime.Now;
                }

                this.CredencialRepository.AtualizarCredencial(credencial);
            }

            solicitacao.Credencial = credencial;
        }

        public void SalvarATIV(Solicitacao solicitacao)
        {
            SolicitacaoRepository.IncluirNovaSolicitacao(solicitacao);
            SolicitacaoRepository.Salvar();
        }

        private void CarregarCursosExigidos(Solicitacao solicitacao)
        {
            IList<Curso> CursosExigidos = CursoRepository.ObterPorArea(solicitacao.Area1.IdArea);

            foreach (Curso curso in CursosExigidos)
            {
                CursoSemTurma cst = new CursoSemTurma()
                {
                    Curso = curso,
                    Pessoa = solicitacao.Pessoa,
                    DataValidade = DateTime.Now
                };

                CursoSemTurmaRepository.IncluirNovoCursoSemTurma(cst);
            }

            CursosExigidos = CursoRepository.ObterPorArea(solicitacao.Area2.IdArea);

            foreach (Curso curso in CursosExigidos)
            {
                CursoSemTurma cst = new CursoSemTurma()
                {
                    Curso = curso,
                    Pessoa = solicitacao.Pessoa,
                    DataValidade = DateTime.Now
                };

                CursoSemTurmaRepository.IncluirNovoCursoSemTurma(cst);
            }
        }

        public List<Solicitacao> ObterPorVeiculo(int veiculoId)
        {
            return SolicitacaoRepository.ObterPorVeiculo(veiculoId);
        }
    }
}
