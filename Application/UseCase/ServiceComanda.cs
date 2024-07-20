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
        private readonly IQueryComandaMercaderia _queryComandaMercaderia;
        private readonly IQueryTipoMercaderia _queryTipoMercaderia;


        public ServiceComanda(ICommandComanda command, IQueryComanda query, IQueryMercaderia queryMercaderia, IQueryFormaEntrega queryFormaEntrega, IQueryComandaMercaderia queryComandaMercaderia, IQueryTipoMercaderia queryTipoMercaderia)
        {
            _command = command;
            _query = query;
            _queryMercaderia = queryMercaderia;
            _queryFormaEntrega = queryFormaEntrega;
            _queryComandaMercaderia = queryComandaMercaderia;
            _queryTipoMercaderia = queryTipoMercaderia;
        }
        //2
        public async Task<ComandaResponse> InsertComanda(ComandaRequest body)
        {
            Guid comandaId = Guid.NewGuid();
            DateTime date = DateTime.Now;
            DateTime dateFormat = new(date.Year, date.Month, date.Day, 0, 0, 0);

            int amount = 0;
            List<ComandaMercaderia> comandaMercaderias = new();
            foreach (var id in body.mercaderias)
            {
                if(await _queryMercaderia.ExistId(id))
                {
                    comandaMercaderias.Add(new ComandaMercaderia
                    {
                        MercaderiaId = id,
                        ComandaId = comandaId
                    });
                }
                else
                {
                    throw new Exception();
                }
            }
            List<MercaderiaComandaResponse> mercaderiaComandaResponses = await _queryMercaderia.GetListByIds(body.mercaderias);
            foreach (var mercaderia in mercaderiaComandaResponses)
            {
                amount += mercaderia.precio;
            }

            await _command.InsertComanda(new Comanda
                {
                    ComandaId = comandaId,
                    FormaEntregaId = body.formaEntrega,
                    PrecioTotal = amount,
                    Fecha = dateFormat
            }, comandaMercaderias
            ); //EXCEPTION

            string delivery = await _queryFormaEntrega.GetFormaEntrega(body.formaEntrega);
            return new ComandaResponse
            {
                id = comandaId,
                mercaderias = mercaderiaComandaResponses,
                formaEntrega = new Schemas.FormaEntrega { id = body.formaEntrega, descripcion = delivery },
                total = amount,
                fecha = dateFormat
            };
        }
        //3
        public async Task<List<ComandaResponse>> GetAll(DateTime? fecha)
        {
            List<Guid> comandaIds = new();
            List<ComandaResponse> response = new();

            if (fecha != null)
            {
                comandaIds = await _query.GetAllComandaIds(fecha);
            }
            else
            {
                comandaIds = await _query.GetAllComandaIds(null);
            }

            if (comandaIds.Count > 0)
            {
                response = await _queryComandaMercaderia.GetListByIds(comandaIds);
            }
            return response;
        }
        //8
        public async Task<ComandaGetResponse?> GetComandaById(Guid id)
        {
            var comanda = await _query.GetComandaById(id);
            if (comanda != null)
            {
                List<MercaderiaGetResponse> list = new();
                foreach(ComandaMercaderia item in comanda.ComandaMercaderias)
                {
                    if (!list.Exists(el => el.id == item.MercaderiaId))
                    {
                        list.Add(new MercaderiaGetResponse()
                        {
                            id = item.MercaderiaId,
                            nombre = item.Mercaderia.Nombre,
                            precio = item.Mercaderia.Precio,
                            tipo = new TipoMercaderiaResponse() 
                            {
                                id = item.Mercaderia.TipoMercaderiaId,
                                descripcion = await _queryTipoMercaderia.GetTipoMercaderia(item.Mercaderia.TipoMercaderiaId)
                            },
                            imagen = item.Mercaderia.Imagen
                        });
                    }
                }

                return new ComandaGetResponse()
                {
                    id = id,
                    mercaderias = list,
                    formaEntrega = new Schemas.FormaEntrega()
                    {
                        id = comanda.FormaEntregaId,
                        descripcion = await _queryFormaEntrega.GetFormaEntrega(comanda.FormaEntregaId)
                    },
                    total = comanda.PrecioTotal,
                    fecha = comanda.Fecha,
                };
            }
            return null;
        }
    }
}
