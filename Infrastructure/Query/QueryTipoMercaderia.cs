using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.cqrs_Query
{
    public class QueryTipoMercaderia : IQueryTipoMercaderia
    {
        private readonly AppDbContext _appDbContext;

        public QueryTipoMercaderia(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<string> GetTipoMercaderia(int id)
        {
            var query = await _appDbContext.TipoMercaderiaDb.FindAsync(id);
            return query.Descripcion;
        }

        public async Task<bool> TipeExists(int? id)
        {
            bool exist = await _appDbContext.TipoMercaderiaDb.AnyAsync(el => el.TipoMercaderiaId == id);
            return exist;
        }
    }
}
