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
    public class CredencialService : ICredencialService
    {
        public ICredencialRepository CredencialRepository;

        public CredencialService(ICredencialRepository _CredencialRepository)
        {
            CredencialRepository = _CredencialRepository;
        }

        public List<Credencial> ObterTodos()
        {
            return this.CredencialRepository.ObterTodos();
        }

        public List<Credencial> ObterTodosParaImpressao()
        {
            return this.CredencialRepository.ObterTodosParaImpressao();
        }

        public Credencial ObterPorId(int idCredencial)
        {
            return this.CredencialRepository.ObterPorId(idCredencial);
        }

        public List<Credencial> ObterTodasCredenciaisAtivasDeFuncionario()
        {
            return this.CredencialRepository.ObterTodasCredenciaisAtivasDeFuncionario();
        }

        public void Atualizar(Credencial credencial)
        {
            this.ValidarParaSalvar(credencial);
            this.CredencialRepository.Atualizar(credencial);
            this.CredencialRepository.Salvar();
        }

        private void ValidarParaSalvar(Credencial credencial)
        {
            if (credencial.DataVencimento.HasValue && credencial.DataVencimento < DateTime.Now.Date)
                throw new Exception("Nao é possível emitir uma credencial com data menor que hoje.");

            if (credencial.FlgTemporario && credencial.DataVencimento.Value > credencial.Solicitacoes.OrderByDescending(x => x.IdSolicitacao).First().DataAutorizacao.Value.AddDays(90))
                throw new Exception("Esta credencial não pode ser impressa pois o vencimento dela é maior que o permitido para uma credencial temporária.");

            if (!credencial.FlgTemporario && credencial.PessoaId != null && credencial.DataVencimento.Value > credencial.Solicitacoes.OrderByDescending(x => x.IdSolicitacao).First().DataAutorizacao.Value.AddYears(2))
                throw new Exception("Esta credencial não pode ser impressa pois o vencimento dela é maior que o permitido para uma credencial definitiva.");

            if (!credencial.FlgTemporario && credencial.VeiculoId != null && credencial.DataVencimento.Value > credencial.Solicitacoes.OrderByDescending(x => x.IdSolicitacao).First().DataAutorizacao.Value.AddYears(1))
                throw new Exception("Esta credencial não pode ser impressa pois o vencimento dela é maior que o permitido para uma credencial definitiva de veículo.");
        }

        public List<Credencial> ObterATIVs()
        {
            List<Credencial> ativs = new List<Credencial>();

            try
            {
                ativs = CredencialRepository.ObterATIVs();
            }
            catch (Exception ex)
            {
            }

            return ativs;
        }
    }
}
