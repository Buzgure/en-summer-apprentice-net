namespace TMS.API.Model.Dto
{
    public class OrderPatchDTO
    {
        public int OrderId { get; set; }
        public int NumberOfTickets { get; set; }
        public float TotalPrice { get; set; }

    }
}
