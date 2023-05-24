using Domain.Entities;

namespace Application.Interfaces
{
    public interface IQueryComanda
    {
        //3
        public Task<List<Guid>> GetAllComandaIds(DateTime? fecha);
        //8
        public Task<Comanda?> GetComandaById(Guid comandaId);
    }
}
