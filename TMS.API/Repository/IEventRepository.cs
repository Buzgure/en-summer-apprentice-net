using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Repository
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event> GetEventById(int id);

        Event AddEvent(Event @event);

        Event UpdateEvent(Event @event);

        void DeleteEvent(Event @event);

        public List<Event> getEventByEventType(string eventType);
        public Task<Event> EventDTOToEvent(EventDTO eventDTO);
    }
}
