using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Repository
{
    public interface IOrderRepository
    {
        public List<Order> GetOrders();

        public Task<Order> GetOrderById(long id);

        public Order addOrder(Order _order);

        public void updateOrder(Order order);

        public void deleteOrder(Order order);

        public Task<Order> orderDTOToOrder(OrderDTO orderDTO);


    }
}
