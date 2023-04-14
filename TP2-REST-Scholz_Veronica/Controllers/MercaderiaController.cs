using Microsoft.AspNetCore.Mvc;
using Application.Schemas;
using MercaderiaRequest = Application.Schemas.MercaderiaRequest; //ambiguo
using Application.Interfaces;

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
        public IActionResult GetAll([FromQuery] int tipo, string nombre, string orden = "ASC")
        {
            try
            {
                return new JsonResult(new List<MercaderiaGetResponse>()) { StatusCode = 200 };
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
        }

        //1. Debe permitir registrar la mercadería (platos, bebida o postre)
        [HttpPost]//OPTIONS
        public async Task<IActionResult> Create([FromBody] MercaderiaRequest body)
        {
            try
            {
                var result = await _serviceMercaderia.Create(body);

                return new JsonResult(result) { StatusCode = 201 };
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 }; 
            }
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Conflict" }) { StatusCode = 409 }; 
            //}
        }

        //7.edit Agregar búsqueda de mercadería por id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _serviceMercaderia.GetMercaderiaById(id);
                if(result == null)
                {
                    return new JsonResult(new BadRequest { mensaje = "Not Found" }) { StatusCode = 404 };
                    //return NotFound();
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
        public IActionResult Update(int id, [FromBody] MercaderiaRequest body)
        {
            try
            {
                return new JsonResult(new MercaderiaResponse()) { StatusCode = 200 };
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Not Found" }) { StatusCode = 404 }; 
            //}
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Conflict" }) { StatusCode = 409 }; 
            //}
        }

        //6. Debe permitir eliminar la mercadería.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return new JsonResult(new MercaderiaResponse()) { StatusCode = 200 };
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Conflict" }) { StatusCode = 409 }; 
            //}

        }
    }
}
