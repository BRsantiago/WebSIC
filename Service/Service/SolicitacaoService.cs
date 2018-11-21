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
        public IRamoAtividadeRepository RamoAtividadeRepository;


        public SolicitacaoService(ISolicitacaoRepository _SolicitacaoRepository,
                                    ICursoRepository _CursoRepository,
                                        ICursoSemTurmaRepository _CursoSemTurmaRepository,
                                            IPessoaRepository _PessoaRepository,
                                                IAreaRepository _AreaRepository,
                                                    IContratoRepository _ContratoRepository,
                                                        IEmpresaRepository _EmpresaRepository,
                                                            ITipoSolicitacaoRepository _TipoSolicitacaoRepository,
                                                                ICredencialRepository _CredencialRepository,
                                                                    ICargoRepository _CargoRepository,
                                                                        IRamoAtividadeRepository _RamoAtividadeRepository)
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
            RamoAtividadeRepository = _RamoAtividadeRepository;
        }

        public List<Solicitacao> ObterTodos()
        {
            return SolicitacaoRepository.ObterTodos();
        }

        public void Salvar(Solicitacao solicitacao)
        {
            solicitacao.DataAutorizacao = DateTime.Now; //Isto precisar mudar quando pessoas de fora fizerem o cadastro da solicitacao.
            solicitacao.Pessoa = this.PessoaRepository.ObterPorId(solicitacao.PessoaId.Value);

            CarregarCursosExigidos(solicitacao);

            SolicitacaoRepository.IncluirNovaSolicitacao(solicitacao);

            GerarCredencial(solicitacao);

            SolicitacaoRepository.Salvar();
        }

        private void GerarCredencial(Solicitacao novaSolicitacao)
        {
            Credencial credencial = this.CredencialRepository.ObterPorEmpresaPessoaTipoEmissao(novaSolicitacao.EmpresaId.Value, novaSolicitacao.PessoaId.Value, novaSolicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);
            Cargo cargo = this.CargoRepository.ObterPorId(novaSolicitacao.CargoId.Value);

            this.Validar(credencial, novaSolicitacao);

            if (credencial == null)
            {
                credencial = new Credencial();

                credencial.FlgTemporario = novaSolicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria;
                credencial.FlgCVE = novaSolicitacao.Pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = novaSolicitacao.Pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = cargo.Descricao;

                credencial.AeroportoId = novaSolicitacao.AeroportoId;
                credencial.EmpresaId = novaSolicitacao.EmpresaId;
                credencial.PessoaId = novaSolicitacao.PessoaId;
                credencial.VeiculoId = novaSolicitacao.VeiculoId;
                credencial.Area1Id = novaSolicitacao.Area1Id;
                credencial.Area2Id = novaSolicitacao.Area2Id;
                credencial.CargoId = novaSolicitacao.CargoId;
                credencial.Solicitacoes = new List<Solicitacao>();
                credencial.Solicitacoes.Add(novaSolicitacao);

                this.CredencialRepository.IncluirNovaCredencial(credencial);
            }
            else
            {
                credencial.FlgCVE = novaSolicitacao.Pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = novaSolicitacao.Pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = cargo.Descricao;
                credencial.CategoriaMotorista1 = novaSolicitacao.Pessoa.CategoriaUm.ToString();
                credencial.CategoriaMotorista2 = novaSolicitacao.Pessoa.CategoriaDois.ToString();

                credencial.Area1Id = novaSolicitacao.Area1Id;
                credencial.Area2Id = novaSolicitacao.Area2Id;
                credencial.CargoId = novaSolicitacao.CargoId;

                credencial.DataExpedicao = null;
                credencial.Solicitacoes.Add(novaSolicitacao);

                if (novaSolicitacao.TipoSolicitacao.FlgDesativaCredencial)
                {
                    credencial.DataDesativacao = DateTime.Now;
                }

                this.CredencialRepository.Atualizar(credencial);
            }

            //novaSolicitacao.Credencial = credencial;
        }

        private void Validar(Credencial credencial, Solicitacao solicitacao)
        {
            if (solicitacao.IdSolicitacao != 0 && credencial != null && credencial.DataExpedicao.HasValue)
                throw new Exception("Esta solicitação não pode ser alterada pois a credencial já foi emitida.");

            if (credencial != null && credencial.Solicitacoes.Any(s => s.TipoSolicitacao.FlgGeraNovaCredencial && s.TipoSolicitacao.IdTipoSolicitacao == solicitacao.TipoSolicitacaoId && s.IdSolicitacao != solicitacao.IdSolicitacao))
                throw new Exception("Esta solicitação não pode ser concluída pois esta já foi realizada.");

            Contrato contrato = this.ContratoRepository.ObterPorId(solicitacao.ContratoId.Value);
            if (contrato.FimVigencia < DateTime.Now)
                throw new Exception("Esta solicitação não pode ser realizada pois o contrato selecionado não está mais vigente.");
        }

        private void CarregarCursosExigidos(Solicitacao solicitacao)
        {
            IList<Curso> cursosRealizadosComValidade = this.CursoRepository.ObterCursosRealizadosComValidadePorIdPessoa(solicitacao.PessoaId.Value);

            List<Curso> cursosExigidos = new List<Curso>();

            if (solicitacao.Area1Id.HasValue)
                cursosExigidos.AddRange(CursoRepository.ObterPorArea(solicitacao.Area1Id.Value));

            if (solicitacao.Area2Id.HasValue)
                cursosExigidos.AddRange(CursoRepository.ObterPorArea(solicitacao.Area2Id.Value));

            if (solicitacao.RamoAtividadeId.HasValue)
                cursosExigidos.AddRange(CursoRepository.ObterPorRamoAtividade(solicitacao.RamoAtividadeId.Value));

            //var cursosId = ConfigurationManager.AppSettings[solicitacao.RamoAtividade.ToString()];
            //if (!string.IsNullOrEmpty(cursosId))
            //{
            //    foreach (var cursoId in cursosId.Split(','))
            //    {
            //        cursosExigidos.Add(CursoRepository.ObterPorId(int.Parse(cursoId)));
            //    }
            //}

            var cursos2Add = cursosExigidos.Distinct().Except(cursosRealizadosComValidade).ToList();

            foreach (var curso2Add in cursos2Add)
            {
                solicitacao.Pessoa.Curso.Add(new CursoSemTurma()
                {

                    Curso = curso2Add,
                    CursoId = curso2Add.IdCurso,
                    DataValidade = DateTime.Now.AddDays(curso2Add.Validade),
                    PessoaId = solicitacao.PessoaId,
                    Criacao = DateTime.Now,
                    Criador = solicitacao.Criador,
                    Atualizacao = DateTime.Now,
                    Atualizador = solicitacao.Atualizador
                });
            }
        }

        public void Atualizar(Solicitacao solicitacao)
        {
            solicitacao.Pessoa = this.PessoaRepository.ObterPorId(solicitacao.PessoaId.Value);

            CarregarCursosExigidos(solicitacao);

            SolicitacaoRepository.Atualizar(solicitacao);

            GerarCredencial(solicitacao);

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

        public void AtualizarATIV(Solicitacao solicitacao)
        {
            SolicitacaoRepository.Atualizar(solicitacao);
            SolicitacaoRepository.Salvar();
        }

        public void AprovarATIV(Solicitacao solicitacao)
        {
            AtualizarATIV(solicitacao);

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
                    Aeroporto = solicitacao.Aeroporto,
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

        public void ExcluirSolicitacao(Solicitacao solicitacao)
        {
            if (solicitacao.IdSolicitacao != 0 && solicitacao.Credencial != null && solicitacao.Credencial.DataExpedicao.HasValue)
                throw new Exception("Esta solicitação não pode ser excluída pois a credencial já foi emitida.");

            //if(solicitacao.Credencial.Solicitacoes.Count() == 1)
            //    this.CredencialRepository.Remover(solicitacao.Credencial);


            this.SolicitacaoRepository.Remover(solicitacao);
            this.SolicitacaoRepository.Salvar();
        }

        private void ValidarATIV(Solicitacao solicitacao)
        {
            if (solicitacao.Area1.IdArea == solicitacao.Area2.IdArea)
                throw new Exception("Favor selecionar áreas diferentes.");

            if (solicitacao.Area1.IdArea == 0 && solicitacao.Area2.IdArea == 0)
                throw new Exception("Pelo menos uma área deve ser selecionada.");

            if (solicitacao.PortaoAcesso.IdPortaoAcesso == 0)
                throw new Exception("Favor informar o portão de acesso.");

            Credencial credencial = this.CredencialRepository.ObterPorVeiculo(solicitacao.Veiculo.IdVeiculo, solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);
            if (credencial != null && credencial.Solicitacoes.Any(s => s.TipoSolicitacao.FlgGeraNovaCredencial && s.TipoSolicitacao.IdTipoSolicitacao == solicitacao.TipoSolicitacaoId && s.IdSolicitacao != solicitacao.IdSolicitacao))
                throw new Exception("Esta solicitação não pode ser concluída pois esta já foi realizada.");

            Contrato contrato = this.ContratoRepository.ObterPorId(solicitacao.ContratoId.Value);
            if (contrato.FimVigencia < DateTime.Now)
                throw new Exception("Esta solicitação não pode ser realizada pois o contrato selecionado não está mais vigente.");
        }

    }
}
