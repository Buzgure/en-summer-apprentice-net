using TMS.API.Model;
using TMS.API.Model.Dto;

namespace TMS.API.Repository
{
    public class OrderRepository : IOrderRepository
    {

        private readonly TicketManagementSystemContext _dbContext;

        public OrderRepository() {
            _dbContext = new TicketManagementSystemContext();
        }

        public OrderDTO addOrder(OrderDTO orderDTO)
        {
            
            var order = _dbContext.Add(orderDTO);
            _dbContext.SaveChanges();
            return order.Entity;
        }

        public void deleteOrder(long id)
        {
            _dbContext.Remove(id);
            _dbContext.SaveChanges();
        }

        public OrderDTO GetOrderById(long id)
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetOrders()
        {
            var allOrders = _dbContext.Orders.ToList();

            return allOrders.Select(o => new OrderDTO
            {
                OrderId = o.OrderId,
                TicketCategoryId = o.TicketCategoryId ?? 0,
                CustomerName = o.Customer.CustomerName,
                OrderedAt = o.OrderedAt,
                NumberOfTickets = o.NumberOfTickets ?? 0,
                TotalPrice = o.TotalPrice ?? 0

            }).ToList();
        }

        public OrderDTO updateOrder(OrderDTO orderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
