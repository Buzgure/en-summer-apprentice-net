namespace TMS.API.Model.Dto
{
    public class EventDTO
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public string EventType { get; set; }

        public string Venue { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
