using diseaseAPI_DotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {

        private readonly diseaseDbContext _context;

        public DiseaseController (diseaseDbContext context)
        {
            _context = context;
        }
        // GET: api/<DiseaseController>
        [HttpGet()]
        public async Task<ActionResult<List<Disease>>> Get()
        {
            return Ok(await _context.Diseases.ToListAsync());
           // await _db.Authors.Include(b => b.Books)
                  //      .FirstOrDefaultAsync(i => i.Id == id);
        }

        // GET api/<DiseaseController>/5
        [HttpGet("{id}/withArticles")]
        public async Task<ActionResult<List<Disease>>> GetWithArticles(int id)
        {
            /* var Disease = await _context.Diseases.FindAsync(id);
             if (Disease == null)
             {
                 return BadRequest("Disease not found");
             }
             return Ok(Disease);*/
            return Ok(await _context.Diseases.Include(d 
                => d.articles).FirstOrDefaultAsync(i => i.Id == id));
        }
        [HttpGet("{id}/withDoctors")]
        public async Task<ActionResult<List<Disease>>> GetWithDoctors(int id)
        {
            /* var Disease = await _context.Diseases.FindAsync(id);
             if (Disease == null)
             {
                 return BadRequest("Disease not found");
             }
             return Ok(Disease);*/
            return Ok(await _context.Diseases.Include(d
                => d.doctors).FirstOrDefaultAsync(i => i.Id == id));
        }

        // GET api/<DiseaseController>/diseaseName
        [HttpGet("{diseaseName:alpha}")]
        public async Task<ActionResult<List<Disease>>> GetByName(string diseaseName)
        {
            List<Disease> Diseases = await _context.Diseases.ToListAsync();
            foreach(Disease Disease in Diseases)
            {
                if (Disease.diseaseName == diseaseName)
                    return BadRequest(Disease);


            }
            
            return BadRequest("Disease not found");
            
            
        }

        // POST api/<DiseaseController>
        [HttpPost]
        public async Task<ActionResult<List<Disease>>> AddDisease(AddDiseaseDto request)
        {
            var disease = new Disease
            {
                diseaseName = request.diseaseName,
            };
            _context.Diseases.Add(disease);
            await _context.SaveChangesAsync();  
            return Ok(disease.Id);   
        }


        // DELETE api/<DiseaseController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Disease>>> DeleteDisease(int id)
        {
            _context.Diseases.Remove(_context.Diseases.Find(id));   
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
