using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCase
{
    public class ServiceComandaMercaderia : IServiceComandaMercaderia
    {
        private readonly ICommandComandaMercaderia _command;

        public ServiceComandaMercaderia(ICommandComandaMercaderia command)
        {
            _command = command;
        }
        public async Task InsertMercaderia(MerchandiseOrderDto comandaMercaderiaDto)
        {
            var list = comandaMercaderiaDto.selectedMerchandise;
            foreach (var item in list)
            {
                for (int i = 0; i < item.Value.Amount; i++ )
                {
                    ComandaMercaderia comandaMercaderia = new ()
                    {
                        ComandaId = comandaMercaderiaDto.OrderId,
                        MercaderiaId = item.Key,
                    };
                    await _command.InsertComandaMercaderia(comandaMercaderia);
                }
            }
        }
    }
}
