using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommandMercaderia
    {
        //1
        public Task<int> InsertMercaderia(Mercaderia mercaderia);
        //5
        public Task UpdateMercaderia(int id, Mercaderia mercaderia);
        //6
        public Task DeleteMercaderia(int id);

    }
}
