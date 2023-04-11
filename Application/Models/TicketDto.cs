namespace Application.Models
{
    public class TicketDto
    {
        public Guid ComandaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaEntregaDescripcion { get; set; }
        public List<MerchandiseDto> Mercaderias { get; set; }

    }
}
