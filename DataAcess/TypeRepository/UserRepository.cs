using Domain.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.TypeRepository
{
    public class UserRepository : GenericRepository<User> , IUserRepository 
    {
        public UserRepository(diseaseDbContext context) : base(context) { }
    }
}
