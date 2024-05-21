using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<User> GetUser()
        {
            return _dbContext.Users.Where(user => user.Orders.Any(order => order.CreatedAt.Year == 2003 && order.Status == Enums.OrderStatus.Delivered))
                .OrderByDescending(user => user.Orders.Max(order => order.Quantity * order.Price)).FirstAsync();
        }

        public Task<List<User>> GetUsers()
        {
            return _dbContext.Users.Where(user => user.Orders.Any(order => order.CreatedAt.Year == 2010 && order.Status == Enums.OrderStatus.Paid)).ToListAsync();
        }
    }
}
