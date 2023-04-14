using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommandMercaderia
    {
        public Task<Mercaderia?> InsertMercaderia(Mercaderia mercaderia);
    }
}
