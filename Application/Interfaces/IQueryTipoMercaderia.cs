using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryTipoMercaderia
    {
        public Task<List<TipoMercaderia>> GetListTiposMercaderia();
        public Task<int> GetAmount(int tipoMercaderia);
        public Task<TipoMercaderia> GetType(int tipoMercaderia);
    }
}
