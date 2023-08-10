namespace TMS.API.Model.Dto
{
    public class TicketCategoryDTO
    {
        public int TicketCategoryId { get; set; }

        public int? EventId { get; set; }

        public string? Description { get; set; }

        public float? Price { get; set; }
    }
}
