using Application.Schemas;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryMercaderia
    {
        public Task<Mercaderia?> GetMercaderiaById(int mercaderiaId);
        //4
        public Task<List<Mercaderia>?> GetFilteredByNameAndTipe(int tipo, string nombre, string orden);
        public Task<List<Mercaderia>?> GetFilteredByTipe(int tipo, string orden);
        public Task<List<Mercaderia>?> GetFilteredByName(string nombre, string orden);
        public Task<List<Mercaderia>?> GetAll(string orden);
        public Task<bool> ExistName(string name);
        //7
        public Task<bool> ExistId(int id);

    }
}
