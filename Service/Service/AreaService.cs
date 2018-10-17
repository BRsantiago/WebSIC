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
    public class AreaService : IAreaService
    {
        private IAreaRepository areaRepository;

        public AreaService(IAreaRepository repository)
        {
            areaRepository = repository;
        }

        public ServiceReturn Atualizar(Area area)
        {
            try
            {
                var check = ObterPorSigla(area.Sigla, false);
                if (check != null && check.IdArea == area.IdArea)
                {
                    areaRepository.Atualizar(area);
                    areaRepository.Salvar();

                    return new ServiceReturn() { success = true, title = "Sucesso", message = "Área atualizada com sucesso!" };
                }

                return new ServiceReturn() { success = false, title = "Erro", message = "Não foi possível atualizar a área pois já existe um registro com a mesma sigla!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar a área! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            Area area = null;

            try
            {
                area = Obter(id);
                areaRepository.Remover(area);
                areaRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Área deletada com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar a área! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(Area area)
        {
            try
            {
                var check = ObterPorSigla(area.Sigla, false);
                if (check == null)
                {
                    areaRepository.Incluir(area);
                    areaRepository.Salvar();

                    return new ServiceReturn() { success = true, title = "Sucesso", message = "Área cadastrada com sucesso!" };
                }

                return new ServiceReturn() { success = false, title = "Erro", message = "Não foi possível cadastrar a área pois já existe um registro com a mesma sigla!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar a área! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public IList<Area> Listar()
        {
            IList<Area> areas = null;

            try
            {
                areas = areaRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return areas;
        }

        public Area Obter(int id)
        {
            Area area = null;

            try
            {
                area = areaRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return area;
        }

        public Area ObterPorSigla(string sigla, bool withTracking)
        {
            Area area = null;

            try
            {
                area = areaRepository.ObterPorSigla(sigla, withTracking);
            }
            catch (Exception ex)
            { }

            return area;
        }
    }
}
