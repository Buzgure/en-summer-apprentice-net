using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Repository
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Event GetEventById(long id);

        Event AddEvent(Event @event);

        Event UpdateEvent(Event @event);

        void DeleteEvent(long id);

        public List<EventDTO> getEventByEventType(string eventType);
    }
}
