using Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceMercaderia
    {
        public Task<List<Mercaderia>> GetAllMercaderias();
        public Task<List<Mercaderia>> GetMercaderiasByType(int tipoMercaderiaId);
        public Task<int> GetAmountByType(int tipoMercaderiaId);
        public Task<string> GetMercaderiaById(int id);
        public Task<int> GetPrice(int id); 
    }
}
