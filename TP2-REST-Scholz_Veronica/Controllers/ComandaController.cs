using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        //2. Debe permitir registrar las comandas
        [HttpPost]
        public IActionResult Create()
        {
            return Ok();
        }
        //3. Debe enlistar las comandas con el detalle de los platos según la fecha que se le ingrese
        [HttpGet]
        public IActionResult GetAll() 
        { 
            return new JsonResult(new {fecha = "fecha"});
        }
        //8. Agregar búsqueda de comanda por id.
        [HttpPut]
        public IActionResult GetComanda(int comandaId)
        {
            return Ok();
        }
    }
}
