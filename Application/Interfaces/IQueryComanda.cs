using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryComanda
    {
        //3
        public Task<List<Comanda>> GetAllComandas(DateTime fecha);
        //8
        public Task<Comanda?> GetComandaById(Guid comandaId);
    }
}
