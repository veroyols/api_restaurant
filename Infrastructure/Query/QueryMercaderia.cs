using Application.Interfaces;
using Application.Schemas;
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
        //3
        public async Task<List<MercaderiaComandaResponse>> GetListByIds(List<int> ids)
        {
            List<MercaderiaComandaResponse> list = await _appDbContext.MercaderiaDb
                .Where(m => ids.Contains(m.MercaderiaId))
                .Select(m => new MercaderiaComandaResponse 
                    {
                        id = m.MercaderiaId,
                        nombre = m.Nombre,
                        precio = m.Precio,
                    })
                .ToListAsync();
            return list;
        }
        //4
        public async Task<List<Mercaderia>?> GetFilteredByNameAndTipe(int? tipo, string nombre, string orden)
        {
            if (orden == "DESC")
            {
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .Where(el => el.TipoMercaderiaId == tipo && el.Nombre == nombre)
                    .OrderByDescending(el => el.Precio)
                    .ToListAsync();
                return list;
            }
            else
            {
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .Where(el => el.TipoMercaderiaId == tipo && el.Nombre == nombre)
                    .OrderBy(el => el.Precio)
                    .ToListAsync();
                return list;
            }
        }
        //4
        public async Task<List<Mercaderia>?> GetFilteredByTipe(int? tipo, string orden)
        {
            if (orden == "DESC")
            {
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .Where(el => el.TipoMercaderiaId == tipo)
                    .OrderByDescending(el => el.Precio)
                    .ToListAsync();
                return list;
            }
            else
            {
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .Where(el => el.TipoMercaderiaId == tipo)
                    .OrderBy(el => el.Precio)
                    .ToListAsync();
                return list;
            }
        }
        //4
        public async Task<List<Mercaderia>?> GetFilteredByName(string nombre, string orden)
        {
            if (orden == "DESC")
            {
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .Where(el => el.Nombre == nombre)
                    .OrderByDescending(el => el.Precio)
                    .ToListAsync();
                return list;
            }
            else
            {
                var list = await _appDbContext.MercaderiaDb
                    .Include(el => el.TipoMercaderia)
                    .Where(el => el.Nombre == nombre)
                    .OrderBy(el => el.Precio)
                    .ToListAsync();
                return list;
            }
        }
        //4
        public async Task<List<Mercaderia>?> GetAll(string orden)
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
        //6
        public async Task<bool> ComandaMercaderiaExist(int mercaderiaId)
        {
            bool exist = await _appDbContext.ComandaMercaderiaDb
                .AnyAsync(el => el.MercaderiaId == mercaderiaId);
            return exist;
        }
        //7
        public async Task<Mercaderia?> GetMercaderiaById(int mercaderiaId)
        {
            var mercaderia = await _appDbContext.MercaderiaDb
                .Include(el => el.TipoMercaderia)
                .FirstOrDefaultAsync(el => el.MercaderiaId == mercaderiaId);
            return mercaderia;
        }
        public async Task<bool> ExistName(string name)
        {
            bool exist = await _appDbContext.MercaderiaDb.AnyAsync(el => el.Nombre == name);
            return exist;

        }
        public async Task<bool> ExistId(int id)
        {
            bool exist = await _appDbContext.MercaderiaDb.AnyAsync(el => el.MercaderiaId == id);
            return exist;
        }
    }
}
