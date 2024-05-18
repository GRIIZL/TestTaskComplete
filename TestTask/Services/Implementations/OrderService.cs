using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestTask.Enums;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Task<Order> GetOrder()
        {
           return _dbContext.Orders.Where(order => order.Quantity > 1).OrderByDescending(order => order.CreatedAt).FirstAsync();
        }

        public Task<List<Order>> GetOrders()
        {
            return _dbContext.Orders.Where(order => order.User.Status == UserStatus.Active).OrderBy(order => order.CreatedAt).ToListAsync();
            //return _dbContext.Orders.Join(_dbContext.Users, order => order.UserId, user => user.Id, (order, user) => new {order, user}).Where(x => x.user.Status == UserStatus.Active).Select(x=>x.order).OrderBy(order =>order.CreatedAt).ToListAsync();
        }
    }
}
