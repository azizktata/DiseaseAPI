using Domain.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.TypeRepository
{
    public class Doctor_infoRepository : GenericRepository<Doctor_info> , IDoctor_infoRepository
    {
        public Doctor_infoRepository(diseaseDbContext context) : base(context) { }
    }
}
