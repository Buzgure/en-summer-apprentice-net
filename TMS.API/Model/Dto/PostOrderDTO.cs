namespace TMS.API.Model.Dto
{
    public class PostOrderDTO
    {
        public int OrderId { get; set; }
        public int NumberOfTickets { get; set; }
        public float TotalPrice { get; set; }

        public TicketCategoryDTO TicketCategory { get; set; }


    }
}
