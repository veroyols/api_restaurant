using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryMercaderia
    {
        public Task<List<Mercaderia>> GetListMercaderia();
        public Task<List<Mercaderia>> GetListMercaderiaByType(int tipoMercaderiaId);
        public Task<int> GetAmountByType(int tipoMercaderiaId);
        public Task<Mercaderia> GetMercaderiaById(int mercaderiaId);
    }
}
