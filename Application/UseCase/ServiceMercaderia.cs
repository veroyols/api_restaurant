using Application.Interfaces;
using Application.Schemas;
using Domain.Entities;

namespace Application.UseCase
{
    public class ServiceMercaderia : IServiceMercaderia
    {
        private readonly IQueryMercaderia _query;
        private readonly ICommandMercaderia _command;

        public ServiceMercaderia(IQueryMercaderia query, ICommandMercaderia command)
        {
            _query = query;
            _command = command;
        }
        //1,5
        public async Task<bool> Exists(string name)
        {
            return await _query.ExistName(name);
        }
        //1
        public async Task<MercaderiaResponse> Create(MercaderiaRequest body)
        {
            int mercaderiaId = await _command.InsertMercaderia(new Mercaderia
            {
                Nombre = body.nombre,
                TipoMercaderiaId = body.tipo,
                Precio = body.precio,
                Ingredientes = body.ingredientes,
                Preparacion = body.preparacion,
                Imagen = body.imagen,
            });
            var mercaderia = await _query.GetMercaderiaById(mercaderiaId);

            var response = new MercaderiaResponse
            {
                id = mercaderia.MercaderiaId,
                nombre = mercaderia.Nombre,
                tipo = new TipoMercaderiaResponse()
                {
                    id = mercaderia.TipoMercaderiaId,
                    descripcion = mercaderia.TipoMercaderia.Descripcion,
                },
                precio = mercaderia.Precio,
                ingredientes = mercaderia.Ingredientes,
                preparacion = mercaderia.Preparacion,
                imagen = mercaderia.Imagen,
            };
            return response;
        }
        //4
        public async Task<List<MercaderiaGetResponse>>? GetFiltered(int tipo, string? nombre, string orden) //debug: tipo=0, string=null, orden="ASC"
        {
            List<MercaderiaGetResponse>? response = new();

            var mercaderias = await _query.GetAll(tipo, nombre, orden);
            if (mercaderias != null)
            {
                foreach (var item in mercaderias)
                {
                    response.Add(new MercaderiaGetResponse
                    {
                        id = item.MercaderiaId,
                        nombre = item.Nombre,
                        precio = item.Precio,
                        tipo = new TipoMercaderiaResponse()
                        {
                            id = item.TipoMercaderiaId,
                            descripcion = item.TipoMercaderia.Descripcion
                        },
                        imagen = item.Imagen,
                    });
                }
            }
            return response;
        }
        //5 PUT
        public async Task<bool> Exists(int id)
        {
            return await _query.ExistId(id);
        }
        public async  Task<MercaderiaResponse> Update(int id, MercaderiaRequest body)
        {
            await _command.UpdateMercaderia(id, new Mercaderia
            {
                Nombre = body.nombre,
                TipoMercaderiaId = body.tipo,
                Precio = body.precio,
                Ingredientes = body.ingredientes,
                Preparacion = body.preparacion,
                Imagen = body.imagen,
            });
            var mercaderia = await _query.GetMercaderiaById(id);

            var response = new MercaderiaResponse
            {
                id = mercaderia.MercaderiaId,
                nombre = mercaderia.Nombre,
                tipo = new TipoMercaderiaResponse()
                {
                    id = mercaderia.TipoMercaderiaId,
                    descripcion = mercaderia.TipoMercaderia.Descripcion,
                },
                precio = mercaderia.Precio,
                ingredientes = mercaderia.Ingredientes,
                preparacion = mercaderia.Preparacion,
                imagen = mercaderia.Imagen,
            };
            return response;
        }
        //6
        public async Task Delete(int id)
        {
            await _command.DeleteMercaderia(id);
        }

        //7
        public async Task<MercaderiaResponse?> GetMercaderiaById(int id)
        {
            var mercaderia = await _query.GetMercaderiaById(id);
            if(mercaderia != null)
            {
                return new()
                {
                    id = mercaderia.MercaderiaId,
                    nombre = mercaderia.Nombre,
                    tipo = new TipoMercaderiaResponse()
                    {
                        id = mercaderia.TipoMercaderiaId,
                        descripcion = mercaderia.TipoMercaderia.Descripcion,
                    },
                    precio = mercaderia.Precio,
                    ingredientes = mercaderia.Ingredientes,
                    preparacion = mercaderia.Preparacion,
                    imagen = mercaderia.Imagen
                };
            }
            return null;
        }
    }
}
