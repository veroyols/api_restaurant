using Microsoft.AspNetCore.Mvc;
using Application.Schemas;
using MercaderiaRequest = Application.Schemas.MercaderiaRequest; //ambiguo

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        //4. Debe enlistar la información de la mercadería y permitir filtrar por nombre y/o tipo y ordenar por precio.
        [HttpGet]
        public IActionResult GetAll([FromQuery] int tipo, string nombre, string orden = "ASC")
        {
            try
            {
                return new JsonResult(new List<MercaderiaGetResponse>()); //200
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }); //400
            }
        }

        //1. Debe permitir registrar la mercadería (platos, bebida o postre)
        [HttpPost]//OPTIONS
        public IActionResult Create([FromBody] MercaderiaRequest body)
        {
            try
            {
                return new JsonResult(new MercaderiaResponse()); //201
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }); //400
            }
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Conflict" }); //409
            //}
        }

        //7.edit Agregar búsqueda de mercadería por id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(new MercaderiaResponse()); //200
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }); //400
            }
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Not Found" }); //404
            //}
        }

        //5. Debe permitir modificar la información de la mercadería.
        [HttpPut("{id}")] //OPTIONS
        public IActionResult Update(int id, [FromBody] MercaderiaRequest body)
        {
            try
            {
                return new JsonResult(new MercaderiaResponse()); //200
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }); //400
            }
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Not Found" }); //404
            //}
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Conflict" }); //409
            //}
        }

        //6. Debe permitir eliminar la mercadería.
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return new JsonResult(new MercaderiaResponse()); //200
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }); //400
            }
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Conflict" }); //409
            //}

        }
    }
}
