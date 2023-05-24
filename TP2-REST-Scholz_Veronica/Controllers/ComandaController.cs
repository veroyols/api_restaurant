using Application.Interfaces;
using Application.Schemas;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IServiceComanda _serviceComanda;
        private readonly IServiceMercaderia _serviceMercaderia;

        public ComandaController(IServiceComanda serviceComanda, IServiceMercaderia serviceMercaderia)
        {
            _serviceComanda = serviceComanda;
            _serviceMercaderia = serviceMercaderia;
        }
        //3. Debe enlistar las comandas con el detalle de los platos según la fecha que se le ingrese
        [HttpGet]
        [ProducesResponseType(typeof(List<ComandaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] string? fecha) //mm/dd/yyyy o mm-dd-yyyy, yyyy o yy  parsear fecha para dd/mm
        {
            List<ComandaResponse> result = new();
            DateTime dateFormat;

            if (fecha != null)
            {
                if (DateTime.TryParse(fecha, out DateTime date))
                {
                    dateFormat = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    result = await _serviceComanda.GetAll(dateFormat);
                    return new JsonResult(result) { StatusCode = 200 };
                }

                else
                {
                    return new JsonResult(new BadRequest { message = "No ha ingresado una fecha válida." }) { StatusCode = 400 };
                }
            } 
            else
            {
                result = await _serviceComanda.GetAll(null);
                return new JsonResult(result) { StatusCode = 200 };
            }
        }

        //2. Debe permitir registrar las comandas
        [HttpPost]
        [ProducesResponseType(typeof(ComandaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] ComandaRequest body)
        {
            string message = "";

            try
            {

                bool count = body.mercaderias != null && body.mercaderias.Count > 0;
                bool formaEntrega = body.formaEntrega > 0 && body.formaEntrega <= 3;

                if (count && formaEntrega)
                {
                    bool mercaderiaExistID = await _serviceMercaderia.Exist(body.mercaderias);
                    if (mercaderiaExistID)
                    {
                        var response = await _serviceComanda.InsertComanda(body);
                        return new JsonResult(response) { StatusCode = 201 }; 
                    }
                    else
                    {
                        return new JsonResult(new BadRequest { message = "Ha ingresado uno o mas id de mercaderia no validos. " }) { StatusCode = 400 };
                    }
                }
                else
                {
                    if(!count)
                    {
                        message = "Ingrese una lista de ids de mercaderias valida. ";
                    }
                    if (!formaEntrega)
                    {
                        message += "Ha seleccionado una forma de entrega no valida. ";
                    }
                    return new JsonResult(new BadRequest { message = message }) { StatusCode = 400 };
                }
            }
            catch
            {
                return new JsonResult(new BadRequest { message = "Ha ocurrido un error al crear la comanda." }) { StatusCode = 400 };
            }
        } 

        //8. Agregar búsqueda de comanda por id.
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ComandaGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) //"3fa85f64-5717-4562-b3fc-2c963f66afa6",
        {
            Guid idGuid;
            if (!Guid.TryParseExact(id, "D", out idGuid))
            {
                return new JsonResult(new BadRequest { message = "Ha ingresado un id invalido." }) { StatusCode = 400 };
            }

            var result = await _serviceComanda.GetComandaById(idGuid);
            if (result == null)
            {
                return new JsonResult(new BadRequest { message = "No se encuentra una comanda con el id ingresado" }) { StatusCode = 404 };
            }
            else
            {
                return new JsonResult(result) { StatusCode = 200 };
            }

        }
    }
}
