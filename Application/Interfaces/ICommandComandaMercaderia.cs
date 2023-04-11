using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICommandComandaMercaderia
    {
        public Task InsertComandaMercaderia(ComandaMercaderia comandaMercaderia);
    }
}
