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

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
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
            try
            {
                var order = await _orderRepository.GetOrderById(id);

                if (order == null)
                {
                    return NotFound();
                }
                var orderDTO = _mapper.Map<OrderDTO>(order);

                return Ok(orderDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
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
