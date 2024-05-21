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
        }
    }
}
