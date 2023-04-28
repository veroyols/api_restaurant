namespace Application.Interfaces
{
    public interface IQueryTipoMercaderia
    {
        public Task<string> GetTipoMercaderia(int id);
        public Task<bool> TipeExists(int? id);

    }
}
