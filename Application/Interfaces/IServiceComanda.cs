using Application.Models;

namespace Application.Interfaces
{
    public interface IServiceComanda
    {
        public Task<Guid> InsertComanda(OrderDto comandaDto);
        public Task<List<TicketDto>> GetAllComandas();

    }
}
