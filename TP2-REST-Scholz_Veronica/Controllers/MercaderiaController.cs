using Microsoft.AspNetCore.Mvc;
using Application.Schemas;
using Application.Interfaces;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/v1/[controller]")]
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
        public async Task<IActionResult> GetAll([FromQuery] int? tipo, string? nombre, string? orden)
        {
            if(orden != null)
            {
                orden = orden.ToUpper();
            }
            //devuelve [] si no encuentra
            List<MercaderiaGetResponse>? result = new();
            string message = "";

            //si mando algo debe ser "ASC" o "DESC" sino bad request
            if (orden != null && orden != "ASC" && orden != "DESC")
            {
                message = "El parámetro 'orden' debe ser 'ASC' o 'DESC'.";
                return new JsonResult(new BadRequest { message = message }) { StatusCode = 400 };
            }
            else //ASC DESC null
            {
                try
                {
                    if(tipo == null)
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
                    else 
                    {
                        bool exist = await _serviceMercaderia.TipeExists(tipo);
                        if (!exist)
                        {
                            message = "El valor ingresado del parámetro 'tipo' no existe en la base de datos.";
                            return new JsonResult(new BadRequest { message = message }) { StatusCode = 400 };
                        }
                        if (nombre != null)
                        {
                            result = await _serviceMercaderia.GetFilteredByNameAndTipe(tipo, nombre, orden);
                        }
                        else
                        {
                            result = await _serviceMercaderia.GetFilteredByTipe(tipo, orden);
                        } 
                    }
                    return new JsonResult(result) { StatusCode = 200 };
                }
                catch
                {
                    message = "Ha ocurrido un error. ";
                    return new JsonResult(new BadRequest { message = "Bad Request" }) { StatusCode = 400 };
                }
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
               if (!exists)
                {
                    MercaderiaResponse result = await _serviceMercaderia.Create(body);
                    return new JsonResult(result) { StatusCode = 201 };
                }
                else
                {
                    return new JsonResult(new BadRequest { message = "Ya existe una mercadería con ese nombre." }) { StatusCode = 409 }; 
                }
            }
            catch (Exception) 
            {
                return new JsonResult(new BadRequest { message = "Ha ocurrido un error al crear la mercadería." }) { StatusCode = 400 }; 
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
                    return new JsonResult(new BadRequest { message = "No se encontro una mercaderia con ese id." }) { StatusCode = 404 };
                }
                else
                {
                    return new JsonResult(result) { StatusCode = 200 };
                }
            }
            catch
            {
                return new JsonResult(new BadRequest { message = "El id ingrasado es invalido." }) { StatusCode = 400 };
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
                    return new JsonResult(new BadRequest { message = "No se encontro una mercaderia con ese id." }) { StatusCode = 404 };
                }

                bool exists = _serviceMercaderia.Exists(body.nombre).Result;
                if (!exists)
                {
                    MercaderiaResponse result = await _serviceMercaderia.Update(id, body);
                    return new JsonResult(result) { StatusCode = 200 };
                }
                else
                {
                    return new JsonResult(new BadRequest { message = "No se puede editar, ya existe una mercadería con ese nombre." }) { StatusCode = 409 };
                }
            }
            catch 
            {
                return new JsonResult(new BadRequest { message = "Ha ocurrido un error al editar la mercadería." }) { StatusCode = 400 };
            }
        }

        //6. Debe permitir eliminar la mercadería.
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MercaderiaResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool comandaExist = await _serviceMercaderia.ComandaMercaderiaExist(id);
                if (comandaExist)
                {
                    return new JsonResult(new BadRequest { message = "No se puede eliminar la mercadería, existe una encomienda que dependa de esta." }) { StatusCode = 409 };
                }
                else
                {
                    var result = await _serviceMercaderia.GetMercaderiaById(id);
                    if (result != null)
                    {
                        await _serviceMercaderia.Delete(id);
                        return new JsonResult(result) { StatusCode = 200 };
                    } else
                    {
                        return new JsonResult(new BadRequest { message = "No se encontro una mercaderia con ese id." }) { StatusCode = 404 };
                    }
                }
            }
            catch
            {
                return new JsonResult(new BadRequest { message = "Ha ocurrido un error al eliminar la mercadería." }) { StatusCode = 400 };
            }
        }
    }
}
