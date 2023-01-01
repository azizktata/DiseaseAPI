using Domain.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.TypeRepository
{
    public class AdminRepository : GenericRepository<Admin> , IAdminRepository
    {
        public AdminRepository(diseaseDbContext context) : base(context) { }
    }
}
