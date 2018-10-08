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

        public Cargo Atualizar(Cargo cargo)
        {
            try
            {
                cargoRepository.Atualizar(cargo);
                cargoRepository.Salvar();

                return cargo;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            Cargo cargo = null;

            try
            {
                cargo = cargoRepository.ObterPorId(id);
                cargoRepository.Remover(cargo);
                cargoRepository.Salvar();

                return id;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public Cargo Incluir(Cargo cargo)
        {
            try
            {
                cargoRepository.Incluir(cargo);
                cargoRepository.Salvar();

                return cargo;
            }
            catch (Exception ex)
            { }

            return null;
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
