using AutoMapper;
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
        private readonly IMapper _mapper;
        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(_eventRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        [HttpGet]
        public ActionResult<List<EventDTO>> GetAll() 
            {
            var events = _eventRepository.GetAll();
            var eventsDto = _mapper.Map<List<EventDTO>>(events);
            return Ok(eventsDto);
        }


        [HttpGet]
        public ActionResult<EventDTO> GetById(int id)
        {
            var e = _eventRepository.GetEventById(id).Result;
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
            //return Ok(_eventRepository.getEventByEventType(eventType));
            if (eventType == null) return Ok(_eventRepository.GetAll());
            else
            {
                var events = _eventRepository.getEventByEventType(eventType);
                var dtoEvents = _mapper.Map<List<EventDTO>>(events);
                return Ok(dtoEvents);
            }
            

        }

        [HttpPost]
        public async Task<ActionResult<Event>> AddEvent(EventDTO eventDto)
        {
            try
            {
                var _event = await _eventRepository.EventDTOToEvent(eventDto);
                //_mapper.Map(eventDto, _event);

                _eventRepository.AddEvent(_event);
                return Ok(_event);

                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete]
        public async Task<ActionResult> DeleteEvent(int id)
        {
            try
            {
                var eventEntity = await _eventRepository.GetEventById(id);
                if (eventEntity == null)
                {
                    return NotFound();
                }
                _eventRepository.DeleteEvent(eventEntity);
                return NoContent();


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}