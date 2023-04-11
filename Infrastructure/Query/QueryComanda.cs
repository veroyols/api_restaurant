using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.cqrs_Query
{
    public class QueryComanda : IQueryComanda
    {
        private readonly AppDbContext _appDbContext;

        public QueryComanda(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task<List<Comanda>> GetAllComandas()
        {
            var list = await _appDbContext.ComandaDb
            .Include(el => el.ComandaMercaderias)
            .ThenInclude(el => el.Mercaderia)     
            .ThenInclude(el => el.TipoMercaderia)
            .Include(el => el.FormaEntrega)
            .ToListAsync();
            return list;
        }
    }
}
