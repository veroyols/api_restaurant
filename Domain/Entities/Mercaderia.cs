using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Mercaderia
    {
        public Mercaderia() { }

        [Key]
        public int MercaderiaId { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        public int TipoMercaderiaId { get; set; }
        public int Precio { get; set; }
        [MaxLength(255)]
        public string Ingredientes { get; set; }
        [MaxLength(255)]
        public string Preparacion { get; set; }
        [MaxLength(255)]
        public string Imagen { get; set; }
        //RELACIONES
        public IList<ComandaMercaderia> ComandaMercaderias { get; set; }
        public TipoMercaderia TipoMercaderia { get; set; }
    }
}
