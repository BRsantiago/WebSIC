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

        public Area Atualizar(Area area)
        {
            try
            {
                var check = ObterPorSigla(area.Sigla, false);
                if (check != null && check.IdArea == area.IdArea)
                {
                    areaRepository.Atualizar(area);
                    areaRepository.Salvar();
                    return area;
                }
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            Area area = null;

            try
            {
                area = Obter(id);
                areaRepository.Remover(area);
                areaRepository.Salvar();

                return area.IdArea;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public Area Incluir(Area area)
        {
            try
            {
                var check = ObterPorSigla(area.Sigla, false);
                if (check == null)
                {
                    areaRepository.Incluir(area);
                    areaRepository.Salvar();
                    return area;    
                }
            }
            catch (Exception ex)
            { }

            return null;
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
