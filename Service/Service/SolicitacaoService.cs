using Entity.DTO;
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
            try
            {
                Cargo cargo = this.CargoRepository.ObterPorId(solicitacao.CargoId.Value);
                TipoSolicitacao tipoSolicitacao = this.TipoSolicitacaoRepository.ObterPorId(solicitacao.TipoSolicitacaoId.Value);
                Credencial credencial = this.CredencialRepository.ObterPorEmpresaPessoaTipoEmissao(solicitacao.EmpresaId.Value, solicitacao.PessoaId.Value, solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);
                Pessoa pessoa = this.PessoaRepository.ObterPorId(solicitacao.PessoaId.Value);

                //SolicitacaoRepository.IniciarTransacao();
                //PessoaRepository.IniciarTransacao();
                //CredencialRepository.IniciarTransacao();

                solicitacao.CredencialId = credencial?.IdCredencial;
                solicitacao.DataAutorizacao = DateTime.Now;

                this.Validar(credencial, solicitacao);

                SolicitacaoRepository.Incluir(solicitacao);
                SolicitacaoRepository.Salvar();

                GerarCredencial(solicitacao, credencial, pessoa, cargo, tipoSolicitacao);
                CredencialRepository.Salvar();

                //CarregarCursosExigidos(solicitacao, pessoa);
                //PessoaRepository.AtualizarRepresentante(pessoa);
                //PessoaRepository.Salvar();

                //CredencialRepository.EncerrarTransacao();
                //PessoaRepository.EncerrarTransacao();
                //SolicitacaoRepository.EncerrarTransacao();
            }
            catch (Exception ex)
            {
                //CredencialRepository.DesfazerTransacao();
                //PessoaRepository.DesfazerTransacao();
                //SolicitacaoRepository.DesfazerTransacao();
                throw new Exception(ex.Message);
            }
        }

        private void GerarCredencial(Solicitacao novaSolicitacao, Credencial credencial, Pessoa pessoa, Cargo cargo, TipoSolicitacao tipoSolicitacao)
        {
            //Credencial credencial = this.CredencialRepository.ObterPorEmpresaPessoaTipoEmissao(novaSolicitacao.EmpresaId.Value, novaSolicitacao.PessoaId.Value, novaSolicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);
            //Cargo cargo = this.CargoRepository.ObterPorId(novaSolicitacao.CargoId.Value);
            //TipoSolicitacao tipoSolicitacao = this.TipoSolicitacaoRepository.ObterPorId(novaSolicitacao.TipoSolicitacaoId.Value);

            //this.Validar(credencial, novaSolicitacao);

            if (credencial == null && tipoSolicitacao.FlgGeraNovaCredencial)
            {
                credencial = new Credencial();

                credencial.FlgTemporario = novaSolicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria;
                credencial.FlgCVE = pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = cargo.Descricao;
                credencial.CategoriaMotorista1 = pessoa.CategoriaUm.ToString();

                credencial.AeroportoId = novaSolicitacao.AeroportoId;
                credencial.EmpresaId = novaSolicitacao.EmpresaId;
                credencial.PessoaId = novaSolicitacao.PessoaId;
                credencial.VeiculoId = novaSolicitacao.VeiculoId;
                credencial.Area1Id = novaSolicitacao.Area1Id;
                credencial.Area2Id = novaSolicitacao.Area2Id;
                credencial.CargoId = novaSolicitacao.CargoId;
                credencial.ContratoId = novaSolicitacao.ContratoId;
                credencial.Solicitacoes = new List<Solicitacao>();
                credencial.Solicitacoes.Add(novaSolicitacao);

                credencial.Criador = novaSolicitacao.Criador;
                credencial.Atualizador = novaSolicitacao.Atualizador;
                credencial.Criacao = DateTime.Now;
                credencial.Atualizacao = DateTime.Now;

                this.CredencialRepository.IncluirNovaCredencial(credencial);
            }
            else
            {
                credencial.FlgCVE = pessoa.FlgCVE;
                credencial.NomeImpressaoFrenteCracha = pessoa.Nome;
                credencial.DescricaoFuncaoFrenteCracha = cargo.Descricao;
                credencial.CategoriaMotorista1 = pessoa.CategoriaUm.ToString();

                credencial.Area1Id = novaSolicitacao.Area1Id;
                credencial.Area2Id = novaSolicitacao.Area2Id;
                credencial.CargoId = novaSolicitacao.CargoId;
                credencial.ContratoId = novaSolicitacao.ContratoId;
                credencial.FlgSegundaVia = tipoSolicitacao.FlgGeraSegundaVia;

                if (tipoSolicitacao.FlgDesativaCredencial)
                {
                    credencial.DataDesativacao = DateTime.Now;
                }
                else
                {
                    credencial.DataExpedicao = null;
                }

                credencial.Atualizador = novaSolicitacao.Atualizador;
                credencial.Atualizacao = DateTime.Now;

                this.CredencialRepository.Atualizar(credencial);
            }
        }

        private void Validar(Credencial credencial, Solicitacao solicitacao)
        {
            if (solicitacao.IdSolicitacao != 0 && credencial != null && credencial.DataExpedicao.HasValue)
                throw new Exception("Esta solicitação não pode ser alterada pois a credencial já foi emitida.");

            if (credencial != null && credencial.Solicitacoes.Any(s => s.TipoSolicitacao.FlgGeraNovaCredencial && s.TipoSolicitacaoId == solicitacao.TipoSolicitacaoId && s.IdSolicitacao != solicitacao.IdSolicitacao))
                throw new Exception("Esta solicitação não pode ser concluída pois esta já foi realizada.");

            if (solicitacao.ContratoId == 0)
                throw new Exception("Favor informar um contrato.");

            Contrato contrato = this.ContratoRepository.ObterPorId(solicitacao.ContratoId.Value);
            if (contrato.FimVigencia < DateTime.Now)
                throw new Exception("Esta solicitação não pode ser realizada pois o contrato selecionado não está mais vigente.");

            if (solicitacao.TipoSolicitacaoId == 0)
                throw new Exception("Favor informar o tipo de solicitação.");

            TipoSolicitacao tipoSolicitacao = this.TipoSolicitacaoRepository.ObterPorId(solicitacao.TipoSolicitacaoId.Value);
            if (credencial == null && !tipoSolicitacao.FlgGeraNovaCredencial)
                throw new Exception("Esta solicitacão não pode ser realizada pois ainda não existe credencial emitida para esta pessoa, nesta empresa e deste tipo.");

            if ((tipoSolicitacao.FlgDesativaCredencial || tipoSolicitacao.FlgGeraSegundaVia || tipoSolicitacao.IdTipoSolicitacao == 11 || tipoSolicitacao.IdTipoSolicitacao == 9 || tipoSolicitacao.IdTipoSolicitacao == 8) && String.IsNullOrEmpty(solicitacao.Observacao))
                throw new Exception("Favor informar o motivo desta solictação no campo de observação.");
        }

        private void CarregarCursosExigidos(Solicitacao solicitacao, Pessoa pessoa)
        {
            IList<Curso> cursosRealizadosComValidade = this.CursoRepository.ObterCursosRealizadosComValidadePorIdPessoa(solicitacao.PessoaId.Value);

            List<Curso> cursosExigidos = new List<Curso>();

            if (solicitacao.Area1Id.HasValue)
                cursosExigidos.AddRange(CursoRepository.ObterPorArea(solicitacao.Area1Id.Value));

            if (solicitacao.Area2Id.HasValue)
                cursosExigidos.AddRange(CursoRepository.ObterPorArea(solicitacao.Area2Id.Value));

            if (solicitacao.RamoAtividadeId.HasValue)
                cursosExigidos.AddRange(CursoRepository.ObterPorRamoAtividade(solicitacao.RamoAtividadeId.Value));

            var cursos2Add = cursosExigidos.Distinct().Except(cursosRealizadosComValidade).ToList();

            foreach (var curso2Add in cursos2Add)
            {
                pessoa.Curso.Add(new CursoSemTurma()
                {
                    Curso = curso2Add,
                    CursoId = curso2Add.IdCurso,
                    DataValidade = DateTime.Now.AddDays(curso2Add.Validade),
                    Pessoa = pessoa,
                    PessoaId = pessoa.IdPessoa,
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

            // CarregarCursosExigidos(solicitacao);

            SolicitacaoRepository.Atualizar(solicitacao);

            // GerarCredencial(solicitacao);

            SolicitacaoRepository.Salvar();
        }

        public void AtualizarAnexos(Solicitacao solicitacao)
        {
            SolicitacaoRepository.AtualizarAnexos(solicitacao);
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

        public ServiceReturn SalvarATIV(Solicitacao solicitacao)
        {
            try
            {
                var solicitacoesPorVeiculo = ObterPorVeiculo(solicitacao.VeiculoId.Value);

                Credencial credencial = CredencialRepository
                    .ObterPorVeiculo(solicitacao.VeiculoId.Value, solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);
                solicitacao.CredencialId = credencial?.IdCredencial;

                if (solicitacao.TipoSolicitacaoId == 3)
                    if (solicitacoesPorVeiculo.Any(s => s.Ativo && s.TipoEmissao == solicitacao.TipoEmissao && s.TipoSolicitacaoId == solicitacao.TipoSolicitacaoId && s.DataAutorizacao.HasValue))
                        return new ServiceReturn() { success = false, title = "Erro", message = "Já existe uma solicitação com tipo igual a 'EMISSÃO' aprovada para este veículo!" };

                if (solicitacao.TipoSolicitacaoId != 3)
                    if (!solicitacoesPorVeiculo.Any(s => s.Ativo && s.TipoEmissao == solicitacao.TipoEmissao && s.TipoSolicitacaoId == solicitacao.TipoSolicitacaoId && s.DataAutorizacao.HasValue))
                        return new ServiceReturn() { success = false, title = "Erro", message = "Não é possível salvar esta solicitação com tipo especificado, pois não existe solicitação anterior do tipo 'EMISSÃO' aprovada para este veículo!" };

                SolicitacaoRepository.Incluir(solicitacao);
                SolicitacaoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Solicitação de ATIV cadastrada com sucesso!", id = solicitacao.IdSolicitacao };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar a solicitação! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public void AtualizarATIV(Solicitacao solicitacao)
        {
            SolicitacaoRepository.Atualizar(solicitacao);
            SolicitacaoRepository.Salvar();
        }

        public void AprovarATIV(Solicitacao solicitacao)
        {
            SolicitacaoRepository.Atualizar(solicitacao);

            Credencial credencial = CredencialRepository
                .ObterPorVeiculo(solicitacao.Veiculo.IdVeiculo, solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria);

            if (credencial != null)
            {
                credencial.Atualizador = solicitacao.Atualizador;
                credencial.Atualizacao = DateTime.Now;
                credencial.DataVencimento = solicitacao.Veiculo.Apolice.DataValidade;

                #region
                credencial.AeroportoId = solicitacao.AeroportoId;
                credencial.EmpresaId = solicitacao.EmpresaId;
                credencial.ContratoId = solicitacao.ContratoId;
                credencial.VeiculoId = solicitacao.VeiculoId;
                credencial.Area1Id = solicitacao.Area1Id;
                credencial.Area2Id = solicitacao.Area2Id;
                credencial.PortaoAcesso1Id = solicitacao.PortaoAcesso1Id;
                credencial.PortaoAcesso2Id = solicitacao.PortaoAcesso2Id;
                credencial.PortaoAcesso3Id = solicitacao.PortaoAcesso3Id;
                #endregion

                #region
                //credencial.Aeroporto = solicitacao.Aeroporto;
                //credencial.Empresa = solicitacao.Empresa;
                //credencial.Contrato = solicitacao.Contrato;
                //credencial.Veiculo = solicitacao.Veiculo;
                //credencial.Area1 = solicitacao.Area1;
                //credencial.Area2 = solicitacao.Area2;
                //credencial.PortaoAcesso = solicitacao.PortaoAcesso;
                #endregion

                if (solicitacao.TipoSolicitacao.FlgDesativaCredencial)
                    credencial.DataDesativacao = DateTime.Now;

                CredencialRepository.Atualizar(credencial);
                CredencialRepository.Salvar();
            }
            else
            {
                Credencial newCredencial = new Credencial()
                {
                    Criador = solicitacao.Atualizador,
                    Atualizador = solicitacao.Atualizador,
                    AeroportoId = solicitacao.AeroportoId,
                    EmpresaId = solicitacao.EmpresaId,
                    ContratoId = solicitacao.ContratoId,
                    VeiculoId = solicitacao.VeiculoId,
                    Area1Id = solicitacao.Area1Id,
                    Area2Id = solicitacao.Area2Id,
                    PortaoAcesso1Id = solicitacao.PortaoAcesso1Id,
                    PortaoAcesso2Id = solicitacao.PortaoAcesso2Id,
                    PortaoAcesso3Id = solicitacao.PortaoAcesso3Id,
                    FlgTemporario = solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria,
                    DataVencimento = solicitacao.TipoEmissao == Entity.Enum.TipoEmissao.Temporaria
                        ? DateTime.Now.AddDays(30)
                        : (solicitacao.Veiculo.Apolice.DataValidade - DateTime.Now).TotalDays > 365
                            ? DateTime.Now.AddDays(365)
                            : solicitacao.Veiculo.Apolice.DataValidade
                };

                #region
                newCredencial.Solicitacoes = new List<Solicitacao>();
                newCredencial.Solicitacoes.Add(solicitacao);
                #endregion

                CredencialRepository.IncluirNovaCredencial(newCredencial);
            }

            SolicitacaoRepository.Salvar();
        }

        public void ExcluirSolicitacao(Solicitacao solicitacao)
        {
            if (solicitacao.IdSolicitacao != 0 && solicitacao.Credencial != null && solicitacao.Credencial.DataExpedicao.HasValue)
                throw new Exception("Esta solicitação não pode ser excluída pois a credencial já foi emitida.");

            this.SolicitacaoRepository.Remover(solicitacao);
            this.SolicitacaoRepository.Salvar();
        }

        private void ValidarATIV(Solicitacao solicitacao)
        {
            if (solicitacao.Area1.IdArea == solicitacao.Area2.IdArea)
                throw new Exception("Favor selecionar áreas diferentes.");

            if (solicitacao.Area1.IdArea == 0 && solicitacao.Area2.IdArea == 0)
                throw new Exception("Pelo menos uma área deve ser selecionada.");

            if (solicitacao.PortaoAcesso1.IdPortaoAcesso == 0 && solicitacao.PortaoAcesso2.IdPortaoAcesso == 0 && solicitacao.PortaoAcesso3.IdPortaoAcesso == 0)
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
