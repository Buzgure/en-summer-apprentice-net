using Microsoft.EntityFrameworkCore;
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

        public void deleteOrder(Order order)
        {
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
        }

        public async Task<Order> GetOrderById(long id)
        {
            var orders = await _dbContext.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();
            if (orders == null)
                return null;
            return orders;
        }

        public List<Order> GetOrders()
        {
            var allOrders = _dbContext.Orders.ToList();

            //return allOrders.Select(o => new OrderDTO
            //{
            //    OrderId = o.OrderId,
            //    TicketCategoryId = o.TicketCategoryId ?? 0,
            //    CustomerName = o.Customer.CustomerName,
            //    OrderedAt = o.OrderedAt,
            //    NumberOfTickets = o.NumberOfTickets ?? 0,
            //    TotalPrice = o.TotalPrice ?? 0

            //}).ToList();
            return allOrders;
        }

        public void updateOrder(Order order)
        {
            //var orderEntity = GetOrderById(order.OrderId);
            //orderEntity = order;
            _dbContext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
