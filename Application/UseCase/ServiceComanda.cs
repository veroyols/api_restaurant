using Application.Interfaces;
using Application.Schemas;
using Domain.Entities;

namespace Application.UseCase
{
    public class ServiceComanda : IServiceComanda
    {
        private readonly ICommandComanda _command;
        private readonly IQueryComanda _query;
        private readonly IQueryMercaderia _queryMercaderia;
        private readonly IQueryFormaEntrega _queryFormaEntrega;

        public ServiceComanda(ICommandComanda command, IQueryComanda query, IQueryMercaderia queryMercaderia, IQueryFormaEntrega queryFormaEntrega)
        {
            _command = command;
            _query = query;
            _queryMercaderia = queryMercaderia;
            _queryFormaEntrega = queryFormaEntrega;
        }
        //2
        public async Task<ComandaResponse> InsertComanda(ComandaRequest body)
        {
            Guid comandaId = Guid.NewGuid();
            DateTime date = DateTime.Now;
            int amount = 0;
            List<ComandaMercaderia> comandaMercaderias = new();
            foreach (var id in body.mercaderias)
            {
                comandaMercaderias.Add(new ComandaMercaderia
                {
                    MercaderiaId = id,
                    ComandaId = comandaId
                });               
            }
            List<MercaderiaComandaResponse> mercaderiaComandaResponses = await _queryMercaderia.GetListByIds(body.mercaderias);
            foreach (var mercaderia in mercaderiaComandaResponses)
            {
                amount += mercaderia.precio;
            }

            await _command.InsertComanda(new Comanda
                {
                    FormaEntregaId = body.formaEntrega,
                    PrecioTotal = amount,
                    Fecha = date
                }, comandaMercaderias
            ); //EXCEPTION

            string delivery = await _queryFormaEntrega.GetFormaEntrega(body.formaEntrega);
            return new ComandaResponse
            {
                mercaderias = mercaderiaComandaResponses,
                formaEntrega = new Schemas.FormaEntrega { id = body.formaEntrega, descripcion = delivery },
                total = amount,
                fecha = date
            };
        }
        //3
        public async Task<List<ComandaResponse>> GetAll(string fecha)
        {
            DateTime date = new();
            var response = await _query.GetAllComandas(date);
            return new List<ComandaResponse>();
        }
        //8
        public async Task<ComandaGetResponse?> GetComandaById(string id)
        {
            Guid idGuid = new (id);
            var comanda = await _query.GetComandaById(idGuid);
            if (comanda != null)
            {
                List<MercaderiaGetResponse> list = new();
                foreach(var item in comanda.ComandaMercaderias)
                {
                    list.Add(new MercaderiaGetResponse()
                    {
                        id = item.MercaderiaId,
                        nombre = item.Mercaderia.Nombre,
                        precio = item.Mercaderia.Precio,
                        tipo = new TipoMercaderiaResponse() 
                        {
                            id = item.Mercaderia.TipoMercaderiaId,
                            descripcion = item.Mercaderia.TipoMercaderia.Descripcion
                        },
                        imagen = item.Mercaderia.Imagen
                    });

                }
                return new()
                {
                    mercaderias = list,
                    formaEntrega = new Schemas.FormaEntrega()
                    {
                        id = comanda.FormaEntregaId,
                        descripcion = comanda.FormaEntrega.Descripcion
                    },
                    total = comanda.PrecioTotal,
                    fecha = comanda.Fecha,
                };
            }
            return null;
        }
    }
}
