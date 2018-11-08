using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class TipoCrachaService : ITipoCrachaService
    {
        private ITipoCrachaRepository tipoCrachaRepository;

        public TipoCrachaService(ITipoCrachaRepository repository)
        {
            tipoCrachaRepository = repository;
        }

        public TipoCracha Atualizar(TipoCracha tipoCracha)
        {
            try
            {
                tipoCrachaRepository.Atualizar(tipoCracha);
                tipoCrachaRepository.Salvar();

                return tipoCracha;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            TipoCracha tipoCracha = null;

            try
            {
                tipoCracha = tipoCrachaRepository.ObterPorId(id);
                tipoCrachaRepository.Remover(tipoCracha);
                tipoCrachaRepository.Salvar();

                return id;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public TipoCracha Incluir(TipoCracha tipoCracha)
        {
            try
            {
                tipoCrachaRepository.Incluir(tipoCracha);
                tipoCrachaRepository.Salvar();

                return tipoCracha;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public IList<TipoCracha> Listar()
        {
            IList<TipoCracha> tipoCrachas = null;

            try
            {
                return tipoCrachaRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return tipoCrachas;
        }

        public TipoCracha Obter(int id)
        {
            TipoCracha tipoCracha = null;

            try
            {
                tipoCracha = tipoCrachaRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return tipoCracha;
        }

        public TipoCracha ObterTipoCrachaTemporario()
        {
           return this.tipoCrachaRepository.ObterTipoCrachaTemporario();
        }
    }
}
