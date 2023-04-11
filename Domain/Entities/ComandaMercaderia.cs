using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ComandaMercaderia
    {
        public ComandaMercaderia() { }
        [Key]
        public int ComandaMercaderiaId { get; set; }
        public int MercaderiaId { get; set; }
        public Guid ComandaId { get; set; }
        //RELACIONES
        public Comanda Comanda { get; set; }
        public Mercaderia Mercaderia { get; set; } 
    }
}
