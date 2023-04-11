using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class FormaEntrega
    {
        public FormaEntrega() { }
        [Key]
        public int FormaEntregaId { get; set; }
        [MaxLength(50)]
        public string Descripcion { get; set; }
        //RELACIONES
        public IList<Comanda> Comandas { get; set; }
    }
}
