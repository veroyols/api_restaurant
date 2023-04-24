using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.cqrs_Query
{
    public class QueryMercaderia : IQueryMercaderia
    {
        private readonly AppDbContext _appDbContext;

        public QueryMercaderia(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Mercaderia?> GetMercaderiaById(int mercaderiaId)
        {
            var mercaderia = await _appDbContext.MercaderiaDb
                .Include(el => el.TipoMercaderia)
                .FirstOrDefaultAsync(el => el.MercaderiaId == mercaderiaId);
            return mercaderia;
        }

        public async Task<List<Mercaderia>?> GetAll(int tipo, string? nombre, string orden)
        {
            if (orden == "DESC") 
            { 
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .OrderByDescending(el => el.Precio)
                    .ToListAsync();
                return list;
            }
            else
            {
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .OrderBy(el => el.Precio)
                    .ToListAsync();
                return list;
            }
        }

        public async Task<bool> ExistName(string name)
        {
            var exist = await _appDbContext.MercaderiaDb.AnyAsync(el => el.Nombre == name);
            return exist;

        }
        public async Task<bool> ExistId(int id)
        {
            var exist = await _appDbContext.MercaderiaDb.AnyAsync(el => el.MercaderiaId == id);
            return exist;
        }

    }
}
