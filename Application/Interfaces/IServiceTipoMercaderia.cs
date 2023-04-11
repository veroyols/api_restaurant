using Domain.Entities;

namespace Application.Interfaces
{
    public interface IServiceTipoMercaderia
    {
        public Task<List<TipoMercaderia>> GetAllTiposMercaderia();
        public Task<int> GetAmountOfType(int tipoMercaderiaId);
        public Task<string> GetType(int op);


    }
}
