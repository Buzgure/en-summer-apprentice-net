using Microsoft.EntityFrameworkCore;
using TMS.API.Exceptions;
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

        public Order addOrder(Order _order)
        {

            var order = _dbContext.Add(_order);
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
            return orders;
        }

        public List<Order> GetOrders()
        {
            var allOrders = _dbContext.Orders.ToList();
            return allOrders;
        }

        public async Task<Order> orderDTOToOrder(OrderDTO orderDTO)
        {
            var order = new Order();

            order.OrderId = orderDTO.OrderId;
            order.OrderedAt = orderDTO.OrderedAt;
            order.TotalPrice = orderDTO.TotalPrice;
            order.TicketCategory = _dbContext.TicketCategories.Where(t => t.TicketCategoryId == orderDTO.TicketCategoryId).FirstOrDefault();
            order.Customer = _dbContext.Customers.Where(c => c.CustomerName == orderDTO.CustomerName).FirstOrDefault();
            order.CustomerId = order.Customer.CustomerId;
            order.TicketCategoryId = orderDTO.TicketCategoryId;
            order.NumberOfTickets = orderDTO.NumberOfTickets;
            return order;

        }

        public void updateOrder(Order order)
        {
            _dbContext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public async Task<List<Order>> getAllOrdersByCustomer(int id)
        {
            var ordersByCustomer = await _dbContext.Orders.Where(o => o.CustomerId == id).ToListAsync();
            return ordersByCustomer;
        }
    }
}
