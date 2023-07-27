using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Repository
{
    public interface IOrderRepository
    {
        public List<Order> GetOrders();

        public Task<Order> GetOrderById(long id);

        public OrderDTO addOrder(OrderDTO orderDTO);

        public void updateOrder(Order order);

        public void deleteOrder(Order order);  


    }
}
