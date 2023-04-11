namespace Application.Schemas
{
    public class ComandaGetResponse
    {
        public List<MercaderiaGetResponse> mercaderias { get; set; }
        public FormaEntrega formaEntrega { get; set; }
        public int total { get; set; }
        public DateTime fecha { get; set; }
    }
}
