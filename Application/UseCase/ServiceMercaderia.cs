using Application.Interfaces;
using Domain.Entities;

namespace Application.UseCase
{
    public class ServiceMercaderia : IServiceMercaderia
    {
        private readonly IQueryMercaderia _query;

        public ServiceMercaderia(IQueryMercaderia query)
        {
            _query = query;
        }
        public async Task<List<Mercaderia>> GetAllMercaderias()
        {
            var list = await _query.GetListMercaderia();
            return list;
        }

        public async Task<int> GetAmountByType(int tipoMercaderiaId)
        {
            int cdad = await _query.GetAmountByType(tipoMercaderiaId);
            return cdad;
        }

        public async Task<List<Mercaderia>> GetMercaderiasByType(int tipoMercaderiaId)
        {
            var list = await _query.GetListMercaderiaByType(tipoMercaderiaId);
            return list;
        }
        public async Task<string> GetMercaderiaById(int id)
        {
            Mercaderia mercaderia = await _query.GetMercaderiaById(id);
            return mercaderia.Nombre;
        }

        public async Task<int> GetPrice(int id)
        {
            Mercaderia mercaderia = await _query.GetMercaderiaById(id);
            return mercaderia.Precio;
        }
    }
}
