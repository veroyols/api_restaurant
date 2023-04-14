using Application.Schemas;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceMercaderia
    {
        public Task<MercaderiaResponse?> GetMercaderiaById(int id);
        public Task<List<Mercaderia>> GetAllMercaderias();
        public Task<List<Mercaderia>> GetMercaderiasByType(int tipoMercaderiaId);
        public Task<int> GetAmountByType(int tipoMercaderiaId);
        public Task<int> GetPrice(int id);
        public Task<MercaderiaResponse> Create(MercaderiaRequest body);
    }
}
