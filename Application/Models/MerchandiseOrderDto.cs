namespace Application.Models
{
    public class MerchandiseOrderDto
    {
        public Guid OrderId { get; set; }
        public int MerchandiseId{ get; set; }
        public Dictionary<int, AmountPriceDto> selectedMerchandise { get; set; }
    }
}
