using TMS.API.Model.Dto;

namespace TMS.API.Repository
{
    public interface IOrderRepository
    {
        public List<OrderDTO> GetOrders();

        public OrderDTO GetOrderById(long id);

        public OrderDTO addOrder(OrderDTO orderDTO);

        public OrderDTO updateOrder(OrderDTO orderDTO);

        public void deleteOrder(long id);  


    }
}
