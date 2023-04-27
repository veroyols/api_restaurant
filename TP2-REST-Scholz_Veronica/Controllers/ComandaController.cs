using Application.Interfaces;
using Application.Schemas;
using Microsoft.AspNetCore.Mvc;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IServiceComanda _serviceComanda;

        public ComandaController(IServiceComanda serviceComanda)
        {
            _serviceComanda = serviceComanda;
        }
        //3. Debe enlistar las comandas con el detalle de los platos según la fecha que se le ingrese
        [HttpGet]
        [ProducesResponseType(typeof(List<ComandaResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] string fecha) //mm/dd/yyyy o mm-dd-yyyy, yyyy o yy 
        {
            try
            {
                List<ComandaResponse> result = new();
                result = await _serviceComanda.GetAll(fecha);
                return new JsonResult(result) { StatusCode = 200 };  
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
        public async Task<IActionResult> Add([FromBody] ComandaRequest body)
        {
            try
            {
                var response = await _serviceComanda.InsertComanda(body);
                return new JsonResult(response) { StatusCode = 201 }; 
            }
            catch
            {
                return new JsonResult(new BadRequest { mensaje = "Bad Request" }) { StatusCode = 400 };
            }
        } //deberia agregar el id al response?? 

        //8. Agregar búsqueda de comanda por id.
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ComandaGetResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) //"3fa85f64-5717-4562-b3fc-2c963f66afa6",
        {
            try
            {
                var result = await _serviceComanda.GetComandaById(id.ToLower());
                if (result == null)
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
    }
}
