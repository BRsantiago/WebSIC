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
            CursoSemTurmaRepository.Salvar();
        }

        private void GerarCredencial(Solicitacao solicitacao)
        {
            Credencial credencial = this.CredencialRepository.ObterPorEmpresaPessoaTipoEmissao(solicitacao.Empresa.IdEmpresa, solicitacao.Pessoa.IdPessoa, solicitacao.TipoEmissao == Entity.DTO.TipoEmissao.Temporaria);
            Pessoa pessoa = this.PessoaRepository.ObterPorId(solicitacao.Pessoa.IdPessoa);
            Cargo cargo = this.CargoRepository.ObterPorId(solicitacao.Cargo.IdCargo);

            if (credencial == null)
            {
                credencial = new Credencial();

                credencial.FlgTemporario = solicitacao.TipoEmissao == Entity.DTO.TipoEmissao.Temporaria;
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

                if (solicitacao.TipoSolicitacao.FlgDesativaCredencial)
                {
                    credencial.DataDesativacao = DateTime.Now;
                }

                this.CredencialRepository.Atualizar(credencial);
            }
        }

        private void CarregarCursosExigidos(Solicitacao solicitacao)
        {
            IList<Curso> CursosRealizadosComValidade = this.CursoRepository.ObterCursosRealizadosComValidadePorIdPessoa(solicitacao.Pessoa.IdPessoa);

            if (solicitacao.Area1 != null)
            {
                IList<Curso> CursosExigidos = CursoRepository.ObterPorArea(solicitacao.Area1.IdArea);

                foreach (Curso curso in CursosExigidos.Where(c => CursosRealizadosComValidade.Any(crv => crv.IdCurso != c.IdCurso)))
                {
                    CursoSemTurma cst = new CursoSemTurma()
                    {
                        Curso = curso,
                        Pessoa = solicitacao.Pessoa,
                        DataValidade = DateTime.Now
                    };

                    CursoSemTurmaRepository.Incluir(cst);

                    solicitacao.Pessoa.Curso.Add(cst);
                }
            }
            if (solicitacao.Area2 != null)
            {
                IList<Curso> CursosExigidos = CursoRepository.ObterPorArea(solicitacao.Area2.IdArea);

                foreach (Curso curso in CursosExigidos.Where(c => CursosRealizadosComValidade.Any(crv => crv.IdCurso != c.IdCurso)))
                {
                    CursoSemTurma cst = new CursoSemTurma()
                    {
                        Curso = curso,
                        Pessoa = solicitacao.Pessoa,
                        DataValidade = DateTime.Now
                    };

                    CursoSemTurmaRepository.Incluir(cst);

                    solicitacao.Pessoa.Curso.Add(cst);
                }
            }
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
                .ObterPorVeiculo(solicitacao.Veiculo.IdVeiculo, solicitacao.TipoEmissao == Entity.DTO.TipoEmissao.Temporaria);

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
                    FlgTemporario = solicitacao.TipoEmissao == Entity.DTO.TipoEmissao.Temporaria,
                    DataVencimento = solicitacao.Veiculo.Apolice.DataValidade
                };

                CredencialRepository.IncluirNovaCredencial(newCredencial);
            }

            CredencialRepository.Salvar();
        }
    }
}
