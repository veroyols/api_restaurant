using Application.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        //3. Debe enlistar las comandas con el detalle de los platos según la fecha que se le ingrese
        [HttpGet]
        [ProducesResponseType(typeof(List<ComandaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public IActionResult GetAll([FromQuery] string fecha)
        {
            try
            {
                return new JsonResult(new ComandaResponse()) { StatusCode = 200 };  
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
        }

        //2. Debe permitir registrar las comandas
        [HttpPost]
        [ProducesResponseType(typeof(ComandaResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] ComandaRequest body)
        {
            try
            {
                return new JsonResult(new ComandaResponse()) { StatusCode = 201 };
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
        }

        //8. Agregar búsqueda de comanda por id.
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ComandaGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        public IActionResult GetComanda(string id) //"3fa85f64-5717-4562-b3fc-2c963f66afa6",
        {
            try
            {
                return new JsonResult(new ComandaGetResponse()) { StatusCode = 200 };
            }
            //catch
            //{
            //    return new JsonResult(new BadRequest { mensaje = "Not Found" }) { StatusCode = 404 }; 
            //}
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request"}) { StatusCode = 400 }; 
            }
        }
    }
}
