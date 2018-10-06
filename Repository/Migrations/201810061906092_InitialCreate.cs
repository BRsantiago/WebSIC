namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aeroporto",
                c => new
                    {
                        IdAeroporto = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        IATA = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdAeroporto);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        IdArea = c.Int(nullable: false, identity: true),
                        Sigla = c.String(nullable: false),
                        Descricao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Aeroporto_IdAeroporto = c.Int(),
                    })
                .PrimaryKey(t => t.IdArea)
                .ForeignKey("dbo.Aeroporto", t => t.Aeroporto_IdAeroporto)
                .Index(t => t.Aeroporto_IdAeroporto);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        IdEmpresa = c.Int(nullable: false, identity: true),
                        RazaoSocial = c.String(nullable: false),
                        NomeFantasia = c.String(nullable: false),
                        Endereco = c.String(),
                        Complemento = c.String(),
                        Numero = c.Int(nullable: false),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        CGC = c.String(nullable: false),
                        Telefone = c.String(),
                        TipoCobranca = c.String(),
                        Observacao = c.String(),
                        CEP = c.String(),
                        NumeroContrato = c.String(),
                        Email = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        TipoEmpresa_IdTipoEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.IdEmpresa)
                .ForeignKey("dbo.TipoEmpresas", t => t.TipoEmpresa_IdTipoEmpresa)
                .Index(t => t.TipoEmpresa_IdTipoEmpresa);
            
            CreateTable(
                "dbo.Apolice",
                c => new
                    {
                        IdApolice = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false),
                        DataValidade = c.DateTime(nullable: false),
                        Observacao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Empresa_IdEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.IdApolice)
                .ForeignKey("dbo.Empresa", t => t.Empresa_IdEmpresa)
                .Index(t => t.Empresa_IdEmpresa);
            
            CreateTable(
                "dbo.Veiculo",
                c => new
                    {
                        IdVeiculo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Placa = c.String(),
                        Ano = c.String(),
                        Cor = c.String(),
                        Observacao = c.String(),
                        Chassi = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Apolice_IdApolice = c.Int(),
                        Empresa_IdEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.IdVeiculo)
                .ForeignKey("dbo.Apolice", t => t.Apolice_IdApolice)
                .ForeignKey("dbo.Empresa", t => t.Empresa_IdEmpresa)
                .Index(t => t.Apolice_IdApolice)
                .Index(t => t.Empresa_IdEmpresa);
            
            CreateTable(
                "dbo.Credencial",
                c => new
                    {
                        IdCredencial = c.String(nullable: false, maxLength: 128),
                        Matricula = c.String(nullable: false),
                        FlgMotorista = c.Boolean(nullable: false),
                        FlgTemporario = c.Boolean(nullable: false),
                        FlgCVE = c.Boolean(nullable: false),
                        DataExpedicao = c.String(nullable: false),
                        DataDesativacao = c.String(),
                        DataVencimento = c.String(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Aeroporto_IdAeroporto = c.Int(),
                        Area1_IdArea = c.Int(),
                        Area2_IdArea = c.Int(),
                        Cargo_IdCargo = c.Int(),
                        Empresa_IdEmpresa = c.Int(),
                        Pessoa_IdPessoa = c.String(maxLength: 128),
                        Veiculo_IdVeiculo = c.Int(),
                    })
                .PrimaryKey(t => t.IdCredencial)
                .ForeignKey("dbo.Aeroporto", t => t.Aeroporto_IdAeroporto)
                .ForeignKey("dbo.Area", t => t.Area1_IdArea)
                .ForeignKey("dbo.Area", t => t.Area2_IdArea)
                .ForeignKey("dbo.Cargo", t => t.Cargo_IdCargo)
                .ForeignKey("dbo.Empresa", t => t.Empresa_IdEmpresa)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_IdPessoa)
                .ForeignKey("dbo.Veiculo", t => t.Veiculo_IdVeiculo)
                .Index(t => t.Aeroporto_IdAeroporto)
                .Index(t => t.Area1_IdArea)
                .Index(t => t.Area2_IdArea)
                .Index(t => t.Cargo_IdCargo)
                .Index(t => t.Empresa_IdEmpresa)
                .Index(t => t.Pessoa_IdPessoa)
                .Index(t => t.Veiculo_IdVeiculo);
            
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        IdCargo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdCargo);
            
            CreateTable(
                "dbo.Ocorrencia",
                c => new
                    {
                        IdOcorrencia = c.Int(nullable: false, identity: true),
                        Historico = c.String(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Pessoa_IdPessoa = c.String(maxLength: 128),
                        Credencial_IdCredencial = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdOcorrencia)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_IdPessoa)
                .ForeignKey("dbo.Credencial", t => t.Credencial_IdCredencial)
                .Index(t => t.Pessoa_IdPessoa)
                .Index(t => t.Credencial_IdCredencial);
            
            CreateTable(
                "dbo.Pessoa",
                c => new
                    {
                        IdPessoa = c.String(nullable: false, maxLength: 128),
                        Nome = c.String(nullable: false),
                        Apelido = c.String(),
                        DataNascimento = c.String(),
                        NomePai = c.String(),
                        NomeMae = c.String(),
                        Endereco = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                        CEP = c.String(),
                        TelefoneEmergencia = c.String(nullable: false),
                        TelefoneResidencial = c.String(),
                        TelefoneCelular = c.String(),
                        RNE = c.String(),
                        CPF = c.String(),
                        RG = c.String(),
                        OrgaoExpeditor = c.String(),
                        UFOrgaoExpeditor = c.String(),
                        Genero = c.String(),
                        Observacao = c.String(),
                        FlgCVE = c.String(),
                        Email = c.String(),
                        CNH = c.String(),
                        CategoriaCNH = c.String(),
                        DataValidadeCNH = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdPessoa);
            
            CreateTable(
                "dbo.Solicitacao",
                c => new
                    {
                        IdSolicitacao = c.Int(nullable: false, identity: true),
                        FlgMotorista = c.Boolean(nullable: false),
                        FlgTemporario = c.Boolean(nullable: false),
                        DataAutorizacao = c.DateTime(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Area1_IdArea = c.Int(),
                        Area2_IdArea = c.Int(),
                        Contrato_IdContrato = c.Int(),
                        Credencial_IdCredencial = c.String(maxLength: 128),
                        Empresa_IdEmpresa = c.Int(),
                        Pessoa_IdPessoa = c.String(maxLength: 128),
                        PortaoAcesso_IdPortaoAcesso = c.Int(),
                        TipoSolicitacao_IdTipoSolicitacao = c.Int(),
                        Veiculo_IdVeiculo = c.Int(),
                    })
                .PrimaryKey(t => t.IdSolicitacao)
                .ForeignKey("dbo.Area", t => t.Area1_IdArea)
                .ForeignKey("dbo.Area", t => t.Area2_IdArea)
                .ForeignKey("dbo.Contrato", t => t.Contrato_IdContrato)
                .ForeignKey("dbo.Credencial", t => t.Credencial_IdCredencial)
                .ForeignKey("dbo.Empresa", t => t.Empresa_IdEmpresa)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_IdPessoa)
                .ForeignKey("dbo.PortaoAcesso", t => t.PortaoAcesso_IdPortaoAcesso)
                .ForeignKey("dbo.TipoSolicitacao", t => t.TipoSolicitacao_IdTipoSolicitacao)
                .ForeignKey("dbo.Veiculo", t => t.Veiculo_IdVeiculo)
                .Index(t => t.Area1_IdArea)
                .Index(t => t.Area2_IdArea)
                .Index(t => t.Contrato_IdContrato)
                .Index(t => t.Credencial_IdCredencial)
                .Index(t => t.Empresa_IdEmpresa)
                .Index(t => t.Pessoa_IdPessoa)
                .Index(t => t.PortaoAcesso_IdPortaoAcesso)
                .Index(t => t.TipoSolicitacao_IdTipoSolicitacao)
                .Index(t => t.Veiculo_IdVeiculo);
            
            CreateTable(
                "dbo.Contrato",
                c => new
                    {
                        IdContrato = c.Int(nullable: false, identity: true),
                        Numero = c.String(),
                        InicioVigencia = c.DateTime(nullable: false),
                        FimVigencia = c.DateTime(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Empresa_IdEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.IdContrato)
                .ForeignKey("dbo.Empresa", t => t.Empresa_IdEmpresa)
                .Index(t => t.Empresa_IdEmpresa);
            
            CreateTable(
                "dbo.PortaoAcesso",
                c => new
                    {
                        IdPortaoAcesso = c.Int(nullable: false, identity: true),
                        Sigla = c.String(nullable: false),
                        Descricao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdPortaoAcesso);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        IdSchedule = c.Int(nullable: false, identity: true),
                        DataEnvioEmailAviso = c.DateTime(nullable: false),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.IdSchedule)
                .ForeignKey("dbo.Solicitacao", t => t.IdSchedule)
                .Index(t => t.IdSchedule);
            
            CreateTable(
                "dbo.TipoSolicitacao",
                c => new
                    {
                        IdTipoSolicitacao = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdTipoSolicitacao);
            
            CreateTable(
                "dbo.Turma",
                c => new
                    {
                        IdTurma = c.Int(nullable: false, identity: true),
                        Inicio = c.DateTime(nullable: false),
                        Fim = c.DateTime(nullable: false),
                        Observacao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Curso_IdCurso = c.Int(),
                    })
                .PrimaryKey(t => t.IdTurma)
                .ForeignKey("dbo.Curso", t => t.Curso_IdCurso)
                .Index(t => t.Curso_IdCurso);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        IdUsuario = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Pessoa_IdPessoa = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.IdUsuario)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_IdPessoa)
                .Index(t => t.Pessoa_IdPessoa);
            
            CreateTable(
                "dbo.TipoEmpresas",
                c => new
                    {
                        IdTipoEmpresa = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        TipoCracha_IdTipoCracha = c.Int(),
                    })
                .PrimaryKey(t => t.IdTipoEmpresa)
                .ForeignKey("dbo.TipoCrachas", t => t.TipoCracha_IdTipoCracha)
                .Index(t => t.TipoCracha_IdTipoCracha);
            
            CreateTable(
                "dbo.TipoCrachas",
                c => new
                    {
                        IdTipoCracha = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdTipoCracha);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        IdCurso = c.Int(nullable: false, identity: true),
                        Duracao = c.Int(nullable: false),
                        Observacao = c.String(),
                        DataVencimento = c.DateTime(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Area_IdArea = c.Int(),
                    })
                .PrimaryKey(t => t.IdCurso)
                .ForeignKey("dbo.Area", t => t.Area_IdArea)
                .Index(t => t.Area_IdArea);
            
            CreateTable(
                "dbo.EmpresaAeroportoes",
                c => new
                    {
                        Empresa_IdEmpresa = c.Int(nullable: false),
                        Aeroporto_IdAeroporto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Empresa_IdEmpresa, t.Aeroporto_IdAeroporto })
                .ForeignKey("dbo.Empresa", t => t.Empresa_IdEmpresa, cascadeDelete: true)
                .ForeignKey("dbo.Aeroporto", t => t.Aeroporto_IdAeroporto, cascadeDelete: true)
                .Index(t => t.Empresa_IdEmpresa)
                .Index(t => t.Aeroporto_IdAeroporto);
            
            CreateTable(
                "dbo.PessoaEmpresas",
                c => new
                    {
                        Pessoa_IdPessoa = c.String(nullable: false, maxLength: 128),
                        Empresa_IdEmpresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Pessoa_IdPessoa, t.Empresa_IdEmpresa })
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_IdPessoa, cascadeDelete: true)
                .ForeignKey("dbo.Empresa", t => t.Empresa_IdEmpresa, cascadeDelete: true)
                .Index(t => t.Pessoa_IdPessoa)
                .Index(t => t.Empresa_IdEmpresa);
            
            CreateTable(
                "dbo.TurmaPessoas",
                c => new
                    {
                        Turma_IdTurma = c.Int(nullable: false),
                        Pessoa_IdPessoa = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Turma_IdTurma, t.Pessoa_IdPessoa })
                .ForeignKey("dbo.Turma", t => t.Turma_IdTurma, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_IdPessoa, cascadeDelete: true)
                .Index(t => t.Turma_IdTurma)
                .Index(t => t.Pessoa_IdPessoa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Turma", "Curso_IdCurso", "dbo.Curso");
            DropForeignKey("dbo.Curso", "Area_IdArea", "dbo.Area");
            DropForeignKey("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha", "dbo.TipoCrachas");
            DropForeignKey("dbo.Empresa", "TipoEmpresa_IdTipoEmpresa", "dbo.TipoEmpresas");
            DropForeignKey("dbo.Veiculo", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.Credencial", "Veiculo_IdVeiculo", "dbo.Veiculo");
            DropForeignKey("dbo.Ocorrencia", "Credencial_IdCredencial", "dbo.Credencial");
            DropForeignKey("dbo.Ocorrencia", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Usuario", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.TurmaPessoas", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.TurmaPessoas", "Turma_IdTurma", "dbo.Turma");
            DropForeignKey("dbo.Solicitacao", "Veiculo_IdVeiculo", "dbo.Veiculo");
            DropForeignKey("dbo.Solicitacao", "TipoSolicitacao_IdTipoSolicitacao", "dbo.TipoSolicitacao");
            DropForeignKey("dbo.Schedule", "IdSchedule", "dbo.Solicitacao");
            DropForeignKey("dbo.Solicitacao", "PortaoAcesso_IdPortaoAcesso", "dbo.PortaoAcesso");
            DropForeignKey("dbo.Solicitacao", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Solicitacao", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.Solicitacao", "Credencial_IdCredencial", "dbo.Credencial");
            DropForeignKey("dbo.Solicitacao", "Contrato_IdContrato", "dbo.Contrato");
            DropForeignKey("dbo.Contrato", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.Solicitacao", "Area2_IdArea", "dbo.Area");
            DropForeignKey("dbo.Solicitacao", "Area1_IdArea", "dbo.Area");
            DropForeignKey("dbo.PessoaEmpresas", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.PessoaEmpresas", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Credencial", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.Credencial", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.Credencial", "Cargo_IdCargo", "dbo.Cargo");
            DropForeignKey("dbo.Credencial", "Area2_IdArea", "dbo.Area");
            DropForeignKey("dbo.Credencial", "Area1_IdArea", "dbo.Area");
            DropForeignKey("dbo.Credencial", "Aeroporto_IdAeroporto", "dbo.Aeroporto");
            DropForeignKey("dbo.Veiculo", "Apolice_IdApolice", "dbo.Apolice");
            DropForeignKey("dbo.Apolice", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.EmpresaAeroportoes", "Aeroporto_IdAeroporto", "dbo.Aeroporto");
            DropForeignKey("dbo.EmpresaAeroportoes", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.Area", "Aeroporto_IdAeroporto", "dbo.Aeroporto");
            DropIndex("dbo.TurmaPessoas", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.TurmaPessoas", new[] { "Turma_IdTurma" });
            DropIndex("dbo.PessoaEmpresas", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.PessoaEmpresas", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.EmpresaAeroportoes", new[] { "Aeroporto_IdAeroporto" });
            DropIndex("dbo.EmpresaAeroportoes", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.Curso", new[] { "Area_IdArea" });
            DropIndex("dbo.TipoEmpresas", new[] { "TipoCracha_IdTipoCracha" });
            DropIndex("dbo.Usuario", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.Turma", new[] { "Curso_IdCurso" });
            DropIndex("dbo.Schedule", new[] { "IdSchedule" });
            DropIndex("dbo.Contrato", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.Solicitacao", new[] { "Veiculo_IdVeiculo" });
            DropIndex("dbo.Solicitacao", new[] { "TipoSolicitacao_IdTipoSolicitacao" });
            DropIndex("dbo.Solicitacao", new[] { "PortaoAcesso_IdPortaoAcesso" });
            DropIndex("dbo.Solicitacao", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.Solicitacao", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.Solicitacao", new[] { "Credencial_IdCredencial" });
            DropIndex("dbo.Solicitacao", new[] { "Contrato_IdContrato" });
            DropIndex("dbo.Solicitacao", new[] { "Area2_IdArea" });
            DropIndex("dbo.Solicitacao", new[] { "Area1_IdArea" });
            DropIndex("dbo.Ocorrencia", new[] { "Credencial_IdCredencial" });
            DropIndex("dbo.Ocorrencia", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.Credencial", new[] { "Veiculo_IdVeiculo" });
            DropIndex("dbo.Credencial", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.Credencial", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.Credencial", new[] { "Cargo_IdCargo" });
            DropIndex("dbo.Credencial", new[] { "Area2_IdArea" });
            DropIndex("dbo.Credencial", new[] { "Area1_IdArea" });
            DropIndex("dbo.Credencial", new[] { "Aeroporto_IdAeroporto" });
            DropIndex("dbo.Veiculo", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.Veiculo", new[] { "Apolice_IdApolice" });
            DropIndex("dbo.Apolice", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.Empresa", new[] { "TipoEmpresa_IdTipoEmpresa" });
            DropIndex("dbo.Area", new[] { "Aeroporto_IdAeroporto" });
            DropTable("dbo.TurmaPessoas");
            DropTable("dbo.PessoaEmpresas");
            DropTable("dbo.EmpresaAeroportoes");
            DropTable("dbo.Curso");
            DropTable("dbo.TipoCrachas");
            DropTable("dbo.TipoEmpresas");
            DropTable("dbo.Usuario");
            DropTable("dbo.Turma");
            DropTable("dbo.TipoSolicitacao");
            DropTable("dbo.Schedule");
            DropTable("dbo.PortaoAcesso");
            DropTable("dbo.Contrato");
            DropTable("dbo.Solicitacao");
            DropTable("dbo.Pessoa");
            DropTable("dbo.Ocorrencia");
            DropTable("dbo.Cargo");
            DropTable("dbo.Credencial");
            DropTable("dbo.Veiculo");
            DropTable("dbo.Apolice");
            DropTable("dbo.Empresa");
            DropTable("dbo.Area");
            DropTable("dbo.Aeroporto");
        }
    }
}
