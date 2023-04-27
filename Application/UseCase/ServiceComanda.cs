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
            DateTime date = DateTime.Parse(fecha);
            List<ComandaResponse> response = new();
            List<Guid> comandaIds = await _query.GetAllComandaIds(date);
            if(comandaIds.Count > 0)
            {
                response = await _queryComandaMercaderia.GetListByIds(comandaIds);
            }
            return response;
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
                    int tipeId = item.Mercaderia.TipoMercaderiaId;
                    string tipeDescription = await _queryTipoMercaderia.GetTipoMercaderia(tipeId);

                    list.Add(new MercaderiaGetResponse()
                    {
                        id = item.MercaderiaId,
                        nombre = item.Mercaderia.Nombre,
                        precio = item.Mercaderia.Precio,
                        tipo = new TipoMercaderiaResponse() 
                        {
                            id = tipeId,
                            descripcion = tipeDescription
                },
                        imagen = item.Mercaderia.Imagen
                    });
                }
                int deliveryId = comanda.FormaEntregaId;
                string deliveryDescription = await _queryFormaEntrega.GetFormaEntrega(deliveryId);

                return new()
                {
                    mercaderias = list,
                    formaEntrega = new Schemas.FormaEntrega()
                    {
                        id = deliveryId,
                        descripcion = deliveryDescription
                    },
                    total = comanda.PrecioTotal,
                    fecha = comanda.Fecha,
                };
            }
            return null;
        }
    }
}
