using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace Infrastructure.cqrs_Command
{
    public class CommandMercaderia : ICommandMercaderia
    {
        private readonly AppDbContext _appDbContext;

        public CommandMercaderia(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Mercaderia?> InsertMercaderia(Mercaderia mercaderia)
        {
            await _appDbContext.AddAsync(mercaderia);
            await _appDbContext.SaveChangesAsync();
            return _appDbContext.MercaderiaDb
                        .Include(m => m.TipoMercaderia)
                        .FirstOrDefaultAsync(m => m.MercaderiaId == mercaderia.MercaderiaId).Result;
        }
    }
}
