using Entity.DTO;
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
    public class VeiculoService : IVeiculoService
    {
        public IVeiculoRepository veiculoRepository;

        public VeiculoService(IVeiculoRepository repository)
        {
            veiculoRepository = repository;
        }

        public ServiceReturn Atualizar(Veiculo veiculo)
        {
            try
            {
                var check1 = ObterPorChassi(veiculo.Chassi, false);
                var check2 = ObterPorPlaca(veiculo.Placa, false);
                if ((check1 == null || check1.IdVeiculo == veiculo.IdVeiculo) && (check2 == null || check2.IdVeiculo == veiculo.IdVeiculo))
                {
                    veiculoRepository.Atualizar(veiculo);
                    veiculoRepository.Salvar();

                    return new ServiceReturn() { success = true, title = "Sucesso", message = "Veículo atualizado com sucesso!" };
                }

                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Não foi possível atualizar o veículo pois já existe outro registro com {0}!", check1 != null ? "mesmo chassi" : check2 != null ? "mesma placa" : "causa não identificada") };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar o veículo! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            Veiculo veiculo = null;

            try
            {
                veiculo = veiculoRepository.ObterPorId(id);
                veiculoRepository.Remover(veiculo);
                veiculoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Veículo atualizado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar o veículo! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(Veiculo veiculo)
        {
            try
            {
                var check1 = ObterPorChassi(veiculo.Chassi, false);
                var check2 = ObterPorPlaca(veiculo.Placa, false);
                if (check1 == null && check2 == null)
                {
                    veiculoRepository.Incluir(veiculo);
                    veiculoRepository.Salvar();

                    return new ServiceReturn() { success = true, title = "Sucesso", message = "Veículo cadastrado com sucesso!" };
                }

                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Não foi possível cadastrar o veículo pois já existe outro registro com {0}!", check1 != null ? "mesmo chassi" : check2 != null ? "mesma placa" : "causa não identificada") };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar o veículo! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public IList<Veiculo> Listar()
        {
            IList<Veiculo> veiculos = null;

            try
            {
                return veiculoRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return veiculos;
        }

        public Veiculo Obter(int id)
        {
            Veiculo veiculo = null;

            try
            {
                veiculo = veiculoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return veiculo;
        }

        public IList<Veiculo> ObterPorAno(string ano)
        {
            IList<Veiculo> veiculos = null;

            try
            {
                veiculos = veiculoRepository.ObterPorAno(ano);
            }
            catch (Exception ex)
            { }

            return veiculos;
        }

        public IList<Veiculo> ObterPorApolice(int idApolice)
        {
            IList<Veiculo> veiculos = null;

            try
            {
                veiculos = veiculoRepository.ObterPorApolice(idApolice);
            }
            catch (Exception ex)
            { }

            return veiculos;
        }

        public Veiculo ObterPorChassi(string chassi, bool withTracking)
        {
            Veiculo veiculo = null;

            try
            {
                veiculo = veiculoRepository.ObterPorChassi(chassi, withTracking);
            }
            catch (Exception ex)
            { }

            return veiculo;
        }

        public IList<Veiculo> ObterPorEmpresa(int idEmpresa)
        {
            IList<Veiculo> veiculos = null;

            try
            {
                veiculos = veiculoRepository.ObterPorEmpresa(idEmpresa);
            }
            catch (Exception ex)
            { }

            return veiculos;
        }

        public IList<Veiculo> ObterPorMarca(string marca)
        {
            IList<Veiculo> veiculos = null;

            try
            {
                veiculos = veiculoRepository.ObterPorMarca(marca);
            }
            catch (Exception ex)
            { }

            return veiculos;
        }

        public IList<Veiculo> ObterPorModelo(string modelo)
        {
            IList<Veiculo> veiculos = null;

            try
            {
                veiculos = veiculoRepository.ObterPorModelo(modelo);
            }
            catch (Exception ex)
            { }

            return veiculos;
        }

        public Veiculo ObterPorPlaca(string placa, bool withTracking)
        {
            Veiculo veiculo = null;

            try
            {
                veiculo = veiculoRepository.ObterPorPlaca(placa, withTracking);
            }
            catch (Exception ex)
            { }

            return veiculo;
        }
    }
}
