using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.cqrs_Query
{
    public class QueryMercaderia : IQueryMercaderia
    {
        private readonly AppDbContext _appDbContext;

        public QueryMercaderia(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<int> GetAmountByType(int tipoMercaderiaId)
        {
            int amount = await Task.Run(() => _appDbContext.MercaderiaDb
                .Where(el => el.TipoMercaderiaId == tipoMercaderiaId)
                .Count());
            return amount;
        }

        public async Task<List<Mercaderia>> GetListMercaderia()
        {
            var list = await Task.Run(() => _appDbContext.MercaderiaDb.ToList<Mercaderia>());
            return list;
        }

        public async Task<List<Mercaderia>> GetListMercaderiaByType(int tipoMercaderiaId)
        {
            var list = await Task.Run(() => _appDbContext.MercaderiaDb
                .Where(el => el.TipoMercaderiaId == tipoMercaderiaId)
                .ToList<Mercaderia>());
            return list;
        }

        public async Task<Mercaderia> GetMercaderiaById(int mercaderiaId)
        {
            var mercaderia = await Task.Run(() => _appDbContext.MercaderiaDb.FirstOrDefault(el => el.MercaderiaId == mercaderiaId));
            return mercaderia;
        }
    }
}
