using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TipoMercaderia
    {
        public TipoMercaderia() { }
        [Key]
        public int TipoMercaderiaId { get; set; }
        [MaxLength(50)]
        public string Descripcion { get; set; }
        //RELACIONES
        public IList<Mercaderia> Mercaderias { get; set; }
    }
}
