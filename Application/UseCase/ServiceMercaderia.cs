using Application.Interfaces;
using Application.Schemas;
using Domain.Entities;

namespace Application.UseCase
{
    public class ServiceMercaderia : IServiceMercaderia
    {
        private readonly IQueryMercaderia _query;
        private readonly ICommandMercaderia _command;
        private readonly IQueryTipoMercaderia _queryTipoMercaderia;

        public ServiceMercaderia(IQueryMercaderia query, ICommandMercaderia command, IQueryTipoMercaderia queryTipoMercaderia)
        {
            _query = query;
            _command = command;
            _queryTipoMercaderia = queryTipoMercaderia;
        }
        public static List<MercaderiaGetResponse>?  MapList(List<Mercaderia>? mercaderias)
        {
            List<MercaderiaGetResponse>? list = new ();
            if (mercaderias != null)
            {
                foreach (var item in mercaderias)
                {
                    list.Add(new MercaderiaGetResponse
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
            return list;
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
        public async Task<List<MercaderiaGetResponse>?> GetFilteredByNameAndTipe(int? tipo, string nombre, string? orden)
        {
            var mercaderias = await _query.GetFilteredByNameAndTipe(tipo, nombre, orden);
            return MapList(mercaderias);
        }

        public async Task<List<MercaderiaGetResponse>?> GetFilteredByTipe(int? tipo, string? orden)
        {
            var mercaderias = await _query.GetFilteredByTipe(tipo, orden);
            return MapList(mercaderias);
        }

        public async Task<List<MercaderiaGetResponse>?> GetFilteredByName(string nombre, string? orden)
        {
            List<Mercaderia>? mercaderias = new();
            mercaderias = await _query.GetFilteredByName(nombre, orden);
            return MapList(mercaderias);
        }
        public async Task<List<MercaderiaGetResponse>?> GetAll(string? orden) //debug: tipo=0, string=null, orden="ASC"
        {
            var mercaderias = await _query.GetAll(orden);
            return MapList(mercaderias);
        }
        //5 PUT
        public async Task<bool> Exists(int id)
        {
            return await _query.ExistId(id);
        }
        public async Task<bool> TipeExists(int? id)
        {
            return await _queryTipoMercaderia.TipeExists(id);
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
        public async Task<bool> ComandaMercaderiaExist(int mercaderiaId)
        {
            var exist = await _query.ComandaMercaderiaExist(mercaderiaId);
            return exist;
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

        public async Task<bool> Exist(List<int> mercaderias)
        {
            return await _query.ExistIds(mercaderias);

        }
    }
}
