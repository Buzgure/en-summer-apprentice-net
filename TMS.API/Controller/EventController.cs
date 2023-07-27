using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TMS.API.Model;
using TMS.API.Model.Dto;
using TMS.API.Repository;

namespace TMS.API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public ActionResult<List<EventDTO>> GetAll() 
            {
            var events = _eventRepository.GetAll();


            var eventsDTO = events.Select(e => new EventDTO()
            {
                EventId = e.EventId,
                EventDescription = e.EventDescription,
                EventName = e.EventName,
                EventType = e.EventType?.EventTypeName ?? string.Empty,
                Venue = e.Venue?.Location ?? string.Empty
            });


            var filterEvents = events.Where(x=>x.EventDescription.Equals("Muzica Electronica si nu numai")).FirstOrDefault();
            return Ok(eventsDTO);
        }


        [HttpGet]
        public ActionResult<EventDTO> GetById(long id)
        {
            var e = _eventRepository.GetEventById(id);
            EventDTO result = new EventDTO();
            result.EventId = e.EventId;
            result.EventName = e.EventName;
            result.EventType = e.EventType.EventTypeName;
            result.EventDescription = e.EventDescription;
            result.Venue = e.Venue.Location;
            
            
            var @dtoEvent = new EventDTO
            {
                EventId = e.EventId,
                EventDescription = e.EventDescription,
                EventName = e.EventName,
                EventType = e.EventType.EventTypeName ?? string.Empty,
                Venue = e.Venue.Location ?? string.Empty
            };
                                           
            return Ok(@dtoEvent);
        }

        [HttpGet]
        public ActionResult<List<EventDTO>> GetEventsByEventType(string eventType) {
            return Ok(_eventRepository.getEventByEventType(eventType));


        }

    }
}