using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.API.Model.Dto;
using TMS.API.Repository;

namespace TMS.API.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(_orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(_orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<List<OrderDTO>> GetAll()
        {
            var orders = _orderRepository.GetOrders();

            var dtoOrder = _mapper.Map<List<OrderDTO>>(orders);

            return Ok(dtoOrder);
        }

        [HttpGet]
        public async Task<ActionResult<OrderDTO>> GetById(long id)
        {
                var order = await _orderRepository.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }
            var orderDTO = _mapper.Map<OrderDTO>(order);

            if (orderDTO == null)
            {
                return NotFound();
            }

                return Ok(orderDTO);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> patchOrder(OrderPatchDTO orderPatchDTO)
        {
            try
            {
                var orderEntity = await _orderRepository.GetOrderById(orderPatchDTO.OrderId);
                if (orderEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(orderPatchDTO, orderEntity);
                _orderRepository.updateOrder(orderEntity);
                return Ok(orderEntity);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var orderEntity = await _orderRepository.GetOrderById(id);
                if (orderEntity == null)
                {

                    return NotFound();
                }

                _orderRepository.deleteOrder(orderEntity);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
