using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.cqrs_Command
{
    public class CommandMercaderia : ICommandMercaderia
    {
        private readonly AppDbContext _appDbContext;

        public CommandMercaderia(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //1
        public async Task<int> InsertMercaderia(Mercaderia mercaderia)
        {
            await _appDbContext.AddAsync(mercaderia);
            await _appDbContext.SaveChangesAsync();
            return mercaderia.MercaderiaId;
        }
        //5
        public async Task UpdateMercaderia(int id, Mercaderia mercaderia)
        {
            var up = await _appDbContext.MercaderiaDb.FindAsync(id);
            if (up != null)
            {
                up.Nombre = mercaderia.Nombre;
                up.TipoMercaderiaId = mercaderia.TipoMercaderiaId;
                up.Precio = mercaderia.Precio;
                up.Ingredientes = mercaderia.Ingredientes;
                up.Preparacion = mercaderia.Preparacion;
                up.Imagen = mercaderia.Imagen;
            }
            await _appDbContext.SaveChangesAsync();
        }
        //6
        public async Task DeleteMercaderia(int id)
        {
            var mercaderia = await _appDbContext.MercaderiaDb.FindAsync(id);
            if(mercaderia != null)
            {
                _appDbContext.Remove(mercaderia);
            }
            await _appDbContext.SaveChangesAsync();
        }
    }
}
