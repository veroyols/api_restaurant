using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.UseCase
{
    public class ServiceComanda : IServiceComanda
    {
        private readonly ICommandComanda _command;
        private readonly IQueryComanda _query;

        public ServiceComanda(ICommandComanda command, IQueryComanda query)
        {
            _command = command;
            _query = query;
        }
        public async Task<Guid> InsertComanda(OrderDto comandaDto) 
        {
            Comanda comanda = new () 
            { 
                ComandaId = comandaDto.OrderId,
                FormaEntregaId = comandaDto.MethodId,
                PrecioTotal = comandaDto.Price,
                Fecha = comandaDto.Date
            };
            await _command.InsertComanda(comanda);
            return comandaDto.OrderId;
        }
        public async Task<List<TicketDto>> GetAllComandas()
        {
            List<Comanda> allComandas = await _query.GetAllComandas();
            List<TicketDto> ticketList = new ();
            
            foreach (Comanda comanda in allComandas)
            {
                List<MerchandiseDto> mercaderias = new ();
                MerchandiseDto merchandiseDto = new();
                foreach (var subItem in comanda.ComandaMercaderias.ToList())
                {
                    merchandiseDto = new()
                    {
                        Type = subItem.Mercaderia.TipoMercaderia.Descripcion,
                        MerchandiseName = subItem.Mercaderia.Nombre,
                        Price = subItem.Mercaderia.Precio,
                        Amount = 1
                    };
                    mercaderias.Add(merchandiseDto);
                }
                ticketList.Add(new TicketDto
                {
                    ComandaId = comanda.ComandaId,
                    PrecioTotal = comanda.PrecioTotal,
                    Fecha = comanda.Fecha,
                    FormaEntregaDescripcion = comanda.FormaEntrega.Descripcion,
                    Mercaderias = mercaderias,
                });
            }
            return ticketList;
        }
    }
}
