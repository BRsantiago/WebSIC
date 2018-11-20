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
            if (credencial.DataVencimento.HasValue && credencial.DataVencimento < DateTime.Now)
                throw new Exception("Nao é possível emitir uma credencial com data menor que hoje.");
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
