using Application.Models;

namespace Application.Interfaces
{
    public interface IServiceComandaMercaderia
    {
        public Task InsertMercaderia(MerchandiseOrderDto comandaMercaderiaDto);
    }
}
