using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDiseaseRepository : IGenericRepository <Disease> 
    {
        List<Disease> GetDiseases();

        List<Disease> GetDiseasesByName(string name);

    }
    
    
}
