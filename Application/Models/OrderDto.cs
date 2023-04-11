namespace Application.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public int MethodId { get; set; }
        public int Price{ get; set; }
        public DateTime Date{ get; set; }
        public OrderDto() 
        {
            OrderId = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}
