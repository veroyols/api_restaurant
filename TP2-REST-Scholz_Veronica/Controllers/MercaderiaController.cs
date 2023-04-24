using Microsoft.AspNetCore.Mvc;
using Application.Schemas;
using Application.Interfaces;
using Azure.Core;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        private readonly IServiceMercaderia _serviceMercaderia;

        public MercaderiaController(IServiceMercaderia serviceMercaderia)
        {
            _serviceMercaderia = serviceMercaderia;
        }

        //4. Debe enlistar la información de la mercadería y permitir filtrar por nombre y/o tipo y ordenar por precio.
        [HttpGet]
        [ProducesResponseType(typeof(List<MercaderiaGetResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] int tipo, string? nombre, string orden = "ASC") //debug: tipo=0, string=null, orden="ASC"
        {
            List<MercaderiaGetResponse>? result;
            try
            {
                if(tipo != 0)
                {
                    if (nombre != null)
                    {
                        result = await _serviceMercaderia.GetFilteredByNameAndTipe(tipo, nombre, orden);
                    }
                    else
                    {
                        result = await _serviceMercaderia.GetFilteredByTipe(tipo, orden);
                    }
                }
                else 
                {
                    if(nombre != null)
                    {
                        result = await _serviceMercaderia.GetFilteredByName(nombre, orden);
                    }
                    else
                    {
                        result = await _serviceMercaderia.GetAll(orden);
                    }
                }
                return new JsonResult(result) { StatusCode = 200 };
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
        }

        //1. Debe permitir registrar la mercadería (platos, bebida o postre)OPTIONS
        [HttpPost]
        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] MercaderiaRequest body)
        {
            try
            {
                bool exists = _serviceMercaderia.Exists(body.nombre).Result;
                //no la captura
               
                if (body == null || body.tipo == null || body.precio == null || body.ingredientes == null || body.preparacion == null || body.imagen == null)
                {
                    throw new Exception();
                }

                if (!exists)
                {
                    MercaderiaResponse result = await _serviceMercaderia.Create(body);
                    return new JsonResult(result) { StatusCode = 201 };
                }
                else
                {
                    return new JsonResult(new BadRequest { mensaje = "Conflict" }) { StatusCode = 409 }; 
                }
            }
            catch (Exception) 
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 }; 
            }

        }
        //7.edit Agregar búsqueda de mercadería por id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MercaderiaGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _serviceMercaderia.GetMercaderiaById(id);
                if(result == null)
                {
                    return new JsonResult(new BadRequest { mensaje = "Not Found" }) { StatusCode = 404 };
                }
                else
                {
                    return new JsonResult(result) { StatusCode = 200 };
                }
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
        }

        //5. Debe permitir modificar la información de la mercadería.
        [HttpPut("{id}")] //OPTIONS
        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(int id, [FromBody] MercaderiaRequest body)
        {
            try
            {
                bool existsId = _serviceMercaderia.Exists(id).Result;
                if (!existsId)
                {
                    return new JsonResult(new BadRequest { mensaje = "Not Found" }) { StatusCode = 404 };
                }

                bool exists = _serviceMercaderia.Exists(body.nombre).Result;
                if (!exists)
                {
                    MercaderiaResponse result = await _serviceMercaderia.Update(id, body);
                    return new JsonResult(result) { StatusCode = 200 };
                }
                else
                {
                    return new JsonResult(new BadRequest { mensaje = "Conflict" }) { StatusCode = 409 };
                }
            }
            catch 
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
        }

        //6. Debe permitir eliminar la mercadería.
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _serviceMercaderia.GetMercaderiaById(id);
                if (result != null)
                {
                    await _serviceMercaderia.Delete(id);
                    return new JsonResult(result) { StatusCode = 200 };
                } else
                {
                    return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
                }
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Conflict" }) { StatusCode = 409 }; 
            }
        }
    }
}
