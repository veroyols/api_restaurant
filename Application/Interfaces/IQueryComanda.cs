using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryComanda
    {
        public Task<List<Comanda>> GetAllComandas();
    }
}
