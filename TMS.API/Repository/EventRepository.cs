using Microsoft.AspNetCore.Mvc;
using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementSystemContext _dbContext;
        
        public EventRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }
        
        public Event AddEvent(Event @event)
        {
            var entityEntry = _dbContext.Add(@event);
            _dbContext.SaveChanges();
            var e = entityEntry.Entity;
            return e;

            
        }

        public async Task<Event> EventDTOToEvent(EventDTO eventDTO)
        {
            var _event = new Event();
            _event.Venue = _dbContext.Venues.FirstOrDefault(v => v.Location == eventDTO.Venue);
            _event.VenueId = _event.Venue.VenueId;
            _event.EventType = _dbContext.EventTypes.FirstOrDefault(eT => eT.EventTypeName == eventDTO.EventType);
            _event.EventTypeId = _event.EventType.EventTypeId;
            _event.StartDate = eventDTO.StartDate;
            _event.EndDate = eventDTO.EndDate;
            _event.EventDescription = eventDTO.EventDescription;
            _event.EventName = eventDTO.EventName;
            _event.EventId = eventDTO.EventId;
            return _event;

        }
        public void DeleteEvent(Event @event)
        {
            var entityEntry = _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var @events = _dbContext.Events.ToList();
            return @events;
        }

        public async Task<Event> GetEventById(int id)
        {
            var @event = _dbContext.Events.FirstOrDefault(e => e.EventId == id);
            if (@event == null)
            {
                return null;
            }

            return @event;
        }

        public Event UpdateEvent(Event @event)
        {
            var eventEntry = _dbContext.Update(@event);
            _dbContext.SaveChanges();
            Event e = eventEntry.Entity;
            return e;

        }

        public List<EventDTO> getEventByEventType(string eventType)
        {
            var allEvents = _dbContext.Events.ToList();
            var eventsByType = allEvents.FindAll(e => (e.EventType?.EventTypeName ?? string.Empty).Equals(eventType)).ToList();
            var dtoEventsByType = eventsByType.Select(e => new EventDTO
            {
                EventId = e.EventId,
                EventName = e.EventName,
                EventType = e.EventType.EventTypeName,
                EventDescription = e.EventDescription,
                Venue = e.Venue.Location

            });
            return dtoEventsByType.ToList();
        }
    }
}
