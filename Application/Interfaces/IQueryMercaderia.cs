using Application.Schemas;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryMercaderia
    {
        //3
        public Task<List<MercaderiaComandaResponse>> GetListByIds(List<int> ids);
        //4
        public Task<List<Mercaderia>?> GetFilteredByNameAndTipe(int tipo, string nombre, string orden);
        public Task<List<Mercaderia>?> GetFilteredByTipe(int tipo, string orden);
        public Task<List<Mercaderia>?> GetFilteredByName(string nombre, string orden);
        public Task<List<Mercaderia>?> GetAll(string orden);
        //7
        public Task<Mercaderia?> GetMercaderiaById(int mercaderiaId);
        public Task<bool> ExistName(string name);
        //7
        public Task<bool> ExistId(int id);

    }
}
