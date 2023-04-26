using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommandComanda
    {
        //3
        public Task InsertComanda(Comanda comanda, List<ComandaMercaderia> comandaMercaderias);
    }
}
