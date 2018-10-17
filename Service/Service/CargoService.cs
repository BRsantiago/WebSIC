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
    public class CargoService : ICargoService
    {
        private ICargoRepository cargoRepository;

        public CargoService(ICargoRepository repository)
        {
            cargoRepository = repository;
        }

        public ServiceReturn Atualizar(Cargo cargo)
        {
            try
            {
                cargoRepository.Atualizar(cargo);
                cargoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Cargo atualizado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar o cargo! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            Cargo cargo = null;

            try
            {
                cargo = cargoRepository.ObterPorId(id);
                cargoRepository.Remover(cargo);
                cargoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Cargo deletado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar o cargo! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(Cargo cargo)
        {
            try
            {
                cargoRepository.Incluir(cargo);
                cargoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Cargo cadastrado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar o cargo! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public IList<Cargo> Listar()
        {
            IList<Cargo> cargos = null;

            try
            {
                return cargoRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return cargos;
        }

        public Cargo Obter(int id)
        {
            Cargo cargo = null;

            try
            {
                cargo = cargoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return cargo;
        }
    }
}
