using Application.Interfaces;
using Application.Schemas;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.cqrs_Query
{
    public class QueryComandaMercaderia : IQueryComandaMercaderia
    {
        private readonly AppDbContext _appDbContext;

        public QueryComandaMercaderia(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //3
        public async Task<List<ComandaResponse>> GetListByIds(List<Guid> guids)
        {
            List<ComandaResponse> response = new();
            foreach (Guid guid in guids)
            {
                ComandaResponse comandaResponse = await _appDbContext.ComandaMercaderiaDb
                    .Include(cm => cm.Comanda)
                    .ThenInclude(cm => cm.FormaEntrega)
                    .Include(cm => cm.Mercaderia)
                    .Where(cm => guid == cm.ComandaId)
                    .Select(cm => new ComandaResponse
                    {
                        fecha = cm.Comanda.Fecha,
                        total = cm.Comanda.PrecioTotal,
                        formaEntrega = new Application.Schemas.FormaEntrega
                        {
                            id = cm.Comanda.FormaEntrega.FormaEntregaId,
                            descripcion = cm.Comanda.FormaEntrega.Descripcion
                        },
                        mercaderias = cm.Comanda.ComandaMercaderias
                            .Select(x => new MercaderiaComandaResponse
                            {
                                id = x.MercaderiaId,
                                nombre = x.Mercaderia.Nombre,
                                precio = x.Mercaderia.Precio,
                            }).ToList()
                    }).FirstAsync();

                response.Add(comandaResponse);
            }
            return response;
        }
    }
}
