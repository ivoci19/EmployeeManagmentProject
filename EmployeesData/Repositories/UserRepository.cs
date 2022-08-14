using EmployeesData.IRepositories;
using EmployeesData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesData.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
               _applicationDbContext = applicationDbContext;
        }
        public List<User> Users
        {
            get { return _applicationDbContext.Users.ToList(); }
        }
    }
}
