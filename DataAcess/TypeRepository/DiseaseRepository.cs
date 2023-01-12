using Domain.Models;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAcess.UnitOfWork;

namespace DataAcess.TypeRepository
{
    public class DiseaseRepository : GenericRepository<Disease> , IDiseaseRepository
    {
        public DiseaseRepository(diseaseDbContext context) : base(context) { }

        public List<Disease> GetDiseases()
        {
            return (context.Diseases.Include("articles").Include("doctors").ToList());
        }

        public List<Disease> GetDiseasesByName(string name)
        {
            return context.Diseases.Where(d => d.diseaseName == name).Include("articles").Include(d => d.doctors).ToList();
        }
    }
}
