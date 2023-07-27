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

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDTO>> patchOrder(OrderPatchDTO orderPatchDTO)
        {
           var orderEntity =await _orderRepository.GetOrderById(orderPatchDTO.OrderId);
            if(orderEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(orderPatchDTO, orderEntity);
            _orderRepository.updateOrder(orderEntity);
            return Ok(orderEntity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var orderEntity =await _orderRepository.GetOrderById(id);
            if(orderEntity == null) {
            
            return NotFound();}

            _orderRepository.deleteOrder(orderEntity);
            return NoContent();
        }
    }
}
