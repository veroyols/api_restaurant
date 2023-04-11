using Microsoft.AspNetCore.Mvc;
using Application.Models;

namespace TP2_REST_Scholz_Veronica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercaderiaController : ControllerBase
    {
        //1. Debe permitir registrar la mercadería (platos, bebida o postre)
        [HttpPost]
        public IActionResult Create (MercaderiaRequest mercaderiaRequest)
        {
            return Ok();
        }
        //4. Debe enlistar la información de la mercadería y permitir filtrar por nombre y/o tipo y ordenar por precio.
        //7. Agregar búsqueda de mercadería por nombre y/o tipo y ordenar por precio.
        //[HttpGet]
        //public IActionResult Get(int id)
        //{
        //    return new JsonResult(new { name = "get" });
        //}
        [HttpGet]
        public IActionResult GetAll()
        {
            /*query int tipo, string nombre, string orden (ASC, DESC)*/
            return new JsonResult(new { name = "getall" });
        }
        //5. Debe permitir modificar la información de la mercadería.
        [HttpPut]//[HttpOption]
        public IActionResult Update()
        {
            /*requestBody: MercaderiaRequest*/
            return new JsonResult(new { update = "update" });
        }
        //6. Debe permitir eliminar la mercadería.
        [HttpDelete]
        public IActionResult Remove(int id)
        {
            return new JsonResult(new { del = "del" });
        }
    }
}
