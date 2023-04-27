using Application.Schemas;

namespace Application.Interfaces
{
    public interface IQueryTipoMercaderia
    {
        public Task<string> GetTipoMercaderia(int id);
    }
}
