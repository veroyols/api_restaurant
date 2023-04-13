using Application.Interfaces;
using Application.Schemas;
using Domain.Entities;

namespace Application.UseCase
{
    public class ServiceMercaderia : IServiceMercaderia
    {
        private readonly IQueryMercaderia _query;

        public ServiceMercaderia(IQueryMercaderia query)
        {
            _query = query;
        }
        public async Task<List<Mercaderia>> GetAllMercaderias()
        {
            var list = await _query.GetListMercaderia();
            return list;
        }

        public async Task<int> GetAmountByType(int tipoMercaderiaId)
        {
            int cdad = await _query.GetAmountByType(tipoMercaderiaId);
            return cdad;
        }

        public async Task<List<Mercaderia>> GetMercaderiasByType(int tipoMercaderiaId)
        {
            var list = await _query.GetListMercaderiaByType(tipoMercaderiaId);
            return list;
        }
        public async Task<MercaderiaResponse?> GetMercaderiaById(int id)
        {
            var mercaderia = await _query.GetMercaderiaById(id);
            if(mercaderia != null)
            {
                TipoMercaderiaResponse tipo = new ()
                {
                    id = mercaderia.TipoMercaderiaId,
                    descripcion = mercaderia.TipoMercaderia.Descripcion,
                };
                return new()
                {
                    id = mercaderia.MercaderiaId,
                    nombre = mercaderia.Nombre,
                    tipo = tipo,
                    precio = mercaderia.Precio,
                    ingredientes = mercaderia.Ingredientes,
                    preparacion = mercaderia.Preparacion,
                    imagen = mercaderia.Imagen
                };
            }
            return null;
        }

        public async Task<int> GetPrice(int id)
        {
            Mercaderia mercaderia = await _query.GetMercaderiaById(id);
            return mercaderia.Precio;
        }
    }
}
