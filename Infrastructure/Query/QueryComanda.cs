using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.cqrs_Query
{
    public class QueryComanda : IQueryComanda
    {
        private readonly AppDbContext _appDbContext;

        public QueryComanda(AppDbContext context)
        {
            _appDbContext = context;
        }
        //3
        public async Task<List<Guid>> GetAllComandaIds(DateTime? fecha) 
        {
            List<Guid> list = new();

            if (fecha != null)
            {
                list = await _appDbContext.ComandaDb
                    .Where(el => el.Fecha == fecha)
                    .Select(el => el.ComandaId)
                    .ToListAsync();
            } else
            {
                list = await _appDbContext.ComandaDb
                    .Select(el => el.ComandaId)
                    .ToListAsync();
            }
            return list;
        }

        //8
        public async Task<Comanda?> GetComandaById(Guid comandaId)
        {
            var comanda = await _appDbContext.ComandaDb
                .Include(el => el.FormaEntrega)
                .Include(el => el.ComandaMercaderias)
                .ThenInclude(el => el.Mercaderia)
                
                .FirstOrDefaultAsync(el => el.ComandaId == comandaId);
            return comanda;
        }
    }
}
