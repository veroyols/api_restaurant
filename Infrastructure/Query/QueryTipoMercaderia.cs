using Application.Interfaces;
using Domain.Entities;
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
        public async Task<TipoMercaderia> GetType(int tipoMercaderia)
        {
            var type = await Task.Run(() => _appDbContext.TipoMercaderiaDb
                .FirstOrDefault(item => item.TipoMercaderiaId == tipoMercaderia)
                );
            return type;
        }
        public async Task<int> GetAmount(int tipoMercaderia)
        {
            int amount = await Task.Run(() => _appDbContext.TipoMercaderiaDb.Count());
            return amount;
        }
        public async Task<List<TipoMercaderia>> GetListTiposMercaderia()
        {
            var list = await Task.Run(() => _appDbContext.TipoMercaderiaDb.ToList());
            return list;
        }
    }
}
