using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.API.Controller;
using TMS.API;
using TMS.API.Repository;
using AutoMapper;
using TMS.API.Model;
using TMS.API.Model.Dto;
using System.Collections;
using Microsoft.AspNetCore.Mvc;

namespace TMS.Testing
{
    [TestClass]
    public class OrderControllerTest
    {
        Mock<IOrderRepository> _orderRepositoryMock;
        Mock<IMapper> _mapperMoq;
        List<Order> _moqList;
        List<OrderDTO> _dtoMoq;

        [TestInitialize]
        public void SetupMoqData()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _mapperMoq = new Mock<IMapper>();
            _moqList = new List<Order>
            {
                new Order
                {
                    OrderId =1,
                    CustomerId = 1,
                    TicketCategoryId = 1,
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 1,
                    TotalPrice = 1,
                    Customer = new Customer
                    {
                        CustomerId = 1,
                        CustomerName = "name",
                        Email = "email",
                        Orders = { }
                    },
                    TicketCategory = new TicketCategory {
                    TicketCategoryId = 1,
                    EventId = 1,
                    Description = "desc",
                    Price = 10,
                    Event = new Event()

                    }

                }
            };
            _dtoMoq = new List<OrderDTO>
            {
                new OrderDTO
                {
                    OrderId = 1,
                    TicketCategoryId= 1,
                    CustomerName = "name",
                    OrderedAt = DateTime.Now,
                    NumberOfTickets = 1,
                    TotalPrice = 1
                }
            };

        }

        [TestMethod]

        public async Task GettAllOrderReturnListOfOrders()
        {
            List<Order> moqOrders = _moqList;
            List<Order> moqtask = moqOrders;
            _orderRepositoryMock.Setup(moq => moq.GetOrders()).Returns(moqtask);

            _mapperMoq.Setup(moq => moq.Map<IEnumerable<OrderDTO>>(It.IsAny<List<Order>>())).Returns(_dtoMoq);

            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);


            //Act

            var orders = controller.GetAll();
            var orderResult = orders.Result as OkObjectResult;
            var orderCount = orderResult.Value as IList;

            //Assert

            Assert.AreEqual(_moqList.Count, orderCount.Count);



        }

        [TestMethod]
        public async Task GetOrderByIdReturnNotFoundWhenNoRecordFound()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetOrderById(It.IsAny<long>())).Returns(Task.Run(() => (Order)null));
            //_mapperMoq.Setup(moq => moq.Map<List<OrderDTO>>(It.IsAny<List<Order>>())).Returns((List<OrderDTO>)null);
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);
            
            
            //Act
            var result = await controller.GetById(1);
            var orderResult = result.Result as NotFoundResult;
            
            
            //Assert
            Assert.IsTrue(orderResult.StatusCode == 404);
        }
        [TestMethod]
        public async Task GetOrderByIdReturnNotFoundWhenDTOIsNull()
        {
            //Arrange
            _orderRepositoryMock.Setup(moq => moq.GetOrderById(It.IsAny<long>())).Returns(Task.Run(() => _moqList.First()));
            _mapperMoq.Setup(moq => moq.Map<List<OrderDTO>>(It.IsAny<List<Order>>())).Returns((List<OrderDTO>)null);
            var controller = new OrderController(_orderRepositoryMock.Object, _mapperMoq.Object);


            //Act
            var result = await controller.GetById(1);
            var orderResult = result.Result as NotFoundResult;


            //Assert
            Assert.IsTrue(orderResult.StatusCode == 404);
        }
    }
}
