using Application.Schemas;
using Domain.Entities;

namespace Application.Schemas
{
    public class ComandaResponse
    {
        public Guid id { get; set; }
        public List<MercaderiaComandaResponse> mercaderias { get; set; }
        public FormaEntrega formaEntrega { get; set; }
        public int total { get; set; }
        public DateTime fecha { get; set; }
    }
}
