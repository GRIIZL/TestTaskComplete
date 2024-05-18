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
            //return _dbContext.Users.Where(user => user.Orders.Any(order => order.CreatedAt.Year == 2003)).Where(user => user.Orders.MaxBy(order => order.Quantity * order.Price)).FirstAsync();
            return _dbContext.Users.Select(user => new
            {
                User = user,
                MaxOrderValue = user.Orders.Where(order => order.CreatedAt.Year == 2003).Max(order => order.Quantity * order.Price)
            })
                               .OrderByDescending(u => u.MaxOrderValue)
                               .Select(u => u.User)
                               .FirstAsync();
        }

        public Task<List<User>> GetUsers()
        {
            return _dbContext.Users.Where(user => user.Orders.Any(order => order.CreatedAt.Year == 2010 && order.Status == Enums.OrderStatus.Paid)).ToListAsync();
        }
    }
}
