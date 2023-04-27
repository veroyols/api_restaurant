using Application.Interfaces;
using Infrastructure.Persistence;

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
    }
}
