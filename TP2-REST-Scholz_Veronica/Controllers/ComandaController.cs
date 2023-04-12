using Application.Schemas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        //3. Debe enlistar las comandas con el detalle de los platos según la fecha que se le ingrese
        [HttpGet]
        public IActionResult GetAll([FromQuery] string fecha)
        {
            try
            {
                return new JsonResult(new ComandaResponse()); //200
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }); //400
            }
        }

        //2. Debe permitir registrar las comandas
        [HttpPost]
        public IActionResult Add([FromBody] ComandaRequest body)
        {
            try
            {
                return new JsonResult(new ComandaResponse()); //201
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }); //400
            }
        }

        //8. Agregar búsqueda de comanda por id.
        [HttpPut("{id}")]
        public IActionResult GetComanda(string id) //"3fa85f64-5717-4562-b3fc-2c963f66afa6",
        {
            try
            {
                return new JsonResult(new ComandaGetResponse()); //201
            }
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Not Found" }); //404
            //}
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request"}); //400
            }
        }
    }
}
