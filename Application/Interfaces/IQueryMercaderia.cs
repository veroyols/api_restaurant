using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryMercaderia
    {
        public Task<Mercaderia?> GetMercaderiaById(int mercaderiaId);
        //4
        public Task<List<Mercaderia>?> GetAll(int tipo, string? nombre, string orden);
        public Task<bool> ExistName(string name);
        //7
        public Task<bool> ExistId(int id);

    }
}
