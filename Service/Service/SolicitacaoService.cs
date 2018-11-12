using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public ICargoRepository CargoRepository;


        public SolicitacaoService(ISolicitacaoRepository _SolicitacaoRepository,
                                    ICursoRepository _CursoRepository,
                                        ICursoSemTurmaRepository _CursoSemTurmaRepository,
                                            IPessoaRepository _PessoaRepository,
                                                IAreaRepository _AreaRepository,
                                                    IContratoRepository _ContratoRepository,
                                                        IEmpresaRepository _EmpresaRepository,
                                                            ITipoSolicitacaoRepository _TipoSolicitacaoRepository,
                                                                ICredencialRepository _CredencialRepository,
                                                                    ICargoRepository _CargoRepository)
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
            CargoRepository = _CargoRepository;
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
            //CursoSemTurmaRepository.Salvar();
        }

        private void GerarCredencial(Solicitacao solicitacao)
        {
            Credencial credencial = this.CredencialRepository.ObterPorEmpresaPessoaTipoEmissao(solicitacao.Empresa.IdEmpresa, solicitacao.Pessoa.IdPessoa, solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);
            Pessoa pessoa = this.PessoaRepository.ObterPorId(solicitacao.Pessoa.IdPessoa);
            Cargo cargo = this.CargoRepository.ObterPorId(solicitacao.Cargo.IdCargo);

            if (credencial == null)
            {
                credencial = new Credencial();

                credencial.FlgTemporario = solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria;
                credencial.FlgCVE = solicitacao.Pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = cargo.Descricao;

                credencial.Aeroporto = solicitacao.Aeroporto;
                credencial.Empresa = solicitacao.Empresa;
                credencial.Pessoa = solicitacao.Pessoa;
                credencial.Veiculo = solicitacao.Veiculo;
                credencial.Area1 = solicitacao.Area1;
                credencial.Area2 = solicitacao.Area2;
                credencial.Cargo = solicitacao.Cargo;

                this.CredencialRepository.IncluirNovaCredencial(credencial);
            }
            else
            {
                credencial.FlgCVE = solicitacao.Pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = cargo.Descricao;
                credencial.CategoriaMotorista1 = solicitacao.Pessoa.CategoriaUm.ToString();
                credencial.CategoriaMotorista2 = solicitacao.Pessoa.CategoriaDois.ToString();

                credencial.Area1 = solicitacao.Area1;
                credencial.Area2 = solicitacao.Area2;
                credencial.Cargo = solicitacao.Cargo;

                credencial.DataExpedicao = null;
                credencial.Solicitacoes.Add(solicitacao);

                if (solicitacao.TipoSolicitacao.FlgDesativaCredencial)
                {
                    credencial.DataDesativacao = DateTime.Now;
                }

                this.CredencialRepository.Atualizar(credencial);
            }
        }

        private void CarregarCursosExigidos(Solicitacao solicitacao)
        {
            IList<Curso> cursosRealizadosComValidade = this.CursoRepository.ObterCursosRealizadosComValidadePorIdPessoa(solicitacao.Pessoa.IdPessoa);

            List<Curso> cursosExigidos = new List<Curso>();
            List<CursoSemTurma> cursos = new List<CursoSemTurma>();

            if (solicitacao.Area1 != null)
                cursosExigidos.AddRange(CursoRepository.ObterPorArea(solicitacao.Area1.IdArea));
                #region
                //foreach (Curso curso in CursosExigidos.Where(c => CursosRealizadosComValidade.Any(crv => crv.IdCurso != c.IdCurso)))
                //{
                //    CursoSemTurma cst = new CursoSemTurma()
                //    {
                //        Curso = curso,
                //        Pessoa = solicitacao.Pessoa,
                //        DataValidade = DateTime.Now
                //    };

                //    CursoSemTurmaRepository.Incluir(cst);

                //    solicitacao.Pessoa.Curso.Add(cst);
                //}
                #endregion
            
            if (solicitacao.Area2 != null)
                cursosExigidos.AddRange(CursoRepository.ObterPorArea(solicitacao.Area2.IdArea));
                #region
                //foreach (Curso curso in CursosExigidos.Where(c => CursosRealizadosComValidade.Any(crv => crv.IdCurso != c.IdCurso)))
                //{
                //    CursoSemTurma cst = new CursoSemTurma()
                //    {
                //        Curso = curso,
                //        Pessoa = solicitacao.Pessoa,
                //        DataValidade = DateTime.Now
                //    };

                //    CursoSemTurmaRepository.Incluir(cst);

                //    solicitacao.Pessoa.Curso.Add(cst);
                //}
                #endregion
            
            var cursosId = ConfigurationManager.AppSettings[solicitacao.RamoAtividade.ToString()];
            if (!string.IsNullOrEmpty(cursosId))
                foreach (var cursoId in cursosId.Split(','))
                    cursosExigidos.Add(CursoRepository.ObterPorId(int.Parse(cursoId)));

            var cursos2Add = cursosExigidos.Distinct().Except(cursosRealizadosComValidade);
            foreach (var curso2Add in cursos2Add)
                cursos.Add(new CursoSemTurma()
                {
                    Curso = curso2Add,
                    Pessoa = solicitacao.Pessoa,
                    Criacao = DateTime.Now,
                    Criador = solicitacao.Criador,
                    Atualizacao = DateTime.Now,
                    Atualizador = solicitacao.Atualizador,
                    DataValidade = DateTime.Now.AddDays(curso2Add.Validade)
                });

            cursos.ForEach(c => CursoSemTurmaRepository.Incluir(c));
        }

        public void Atualizar(Solicitacao solicitacao)
        {
            SolicitacaoRepository.Atualizar(solicitacao);
            SolicitacaoRepository.Salvar();
        }

        public Solicitacao ObterPorId(int? id)
        {
            return this.SolicitacaoRepository.ObterPorId(id.Value);
        }

        public Solicitacao Obter(int id)
        {
            return SolicitacaoRepository.ObterPorId(id);
        }

        public List<Solicitacao> ObterPorVeiculo(int veiculoId)
        {
            return SolicitacaoRepository.ObterPorVeiculo(veiculoId);
        }

        public void SalvarATIV(Solicitacao solicitacao)
        {
            SolicitacaoRepository.IncluirNovaSolicitacao(solicitacao);
            SolicitacaoRepository.Salvar();
        }

        public void AprovarATIV(Solicitacao solicitacao)
        {
            Atualizar(solicitacao);

            Credencial credencial = CredencialRepository
                .ObterPorVeiculo(solicitacao.Veiculo.IdVeiculo, solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);

            if (credencial != null)
            {
                credencial.Atualizador = solicitacao.Atualizador;
                credencial.Atualizacao = DateTime.Now;
                credencial.DataVencimento = solicitacao.Veiculo.Apolice.DataValidade;

                credencial.Contrato = solicitacao.Contrato;
                credencial.Area1 = solicitacao.Area1;
                credencial.Area2 = solicitacao.Area2;
                credencial.PortaoAcesso = solicitacao.PortaoAcesso;

                CredencialRepository.Atualizar(credencial);
            }
            else
            {
                Credencial newCredencial = new Credencial()
                {
                    Criador = solicitacao.Atualizador,
                    Atualizador = solicitacao.Atualizador,
                    Empresa = solicitacao.Veiculo.Empresa,
                    Contrato = solicitacao.Contrato,
                    Veiculo = solicitacao.Veiculo,
                    Area1 = solicitacao.Area1,
                    Area2 = solicitacao.Area2,
                    PortaoAcesso = solicitacao.PortaoAcesso,
                    FlgTemporario = solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria,
                    DataVencimento = solicitacao.Veiculo.Apolice.DataValidade
                };

                CredencialRepository.IncluirNovaCredencial(newCredencial);
            }

            CredencialRepository.Salvar();
        }
    }
}
