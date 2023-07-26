namespace TMS.API.Model.Dto
{
    public class OrderDTO
    {
        public long Id { get; set; }

        public long TicketCategoryId { get; set; }

        public string CustomerName { get; set; }
        
        public DateTime? OrderedAt { get; set; }

        public int NumberOfTickets { get; set; }

        public float TotalPrice { get; set; }
        
    }
}
