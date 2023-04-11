using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Comanda
    {
        public Comanda() { }
        [Key]
        public Guid ComandaId { get; set; }
        public int FormaEntregaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
        
        //RELACIONES
        public FormaEntrega FormaEntrega { get; set; }
        public IList<ComandaMercaderia> ComandaMercaderias { get; set; }
    }
}
