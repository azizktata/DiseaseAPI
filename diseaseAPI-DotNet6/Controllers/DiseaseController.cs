using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAcess;

using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
        public  ActionResult<List<Disease>> Get()
        {
            return Ok( _context.Diseases.Include("articles").Include(d => d.doctors).ToList());
           // await _db.Authors.Include(b => b.Books)
                  //      .FirstOrDefaultAsync(i => i.Id == id);
        }

        
        // GET api/<DiseaseController>/diseaseName
        [HttpGet("{diseaseName:alpha}")]
        public async Task<ActionResult<List<Disease>>> GetByName(string diseaseName)
        {
            //List<Disease> Diseases = await _context.Diseases.ToListAsync();
            var disease = _context.Diseases.Where(d => d.diseaseName== diseaseName).FirstOrDefault();

            if (disease == null)
                return NotFound();

            return Ok(_context.Diseases.Where(d => d.diseaseName == diseaseName).Include("articles").Include(d => d.doctors).ToList());

            /*foreach(Disease Disease in Diseases)
            {
                if (Disease.diseaseName == diseaseName)

                    return BadRequest(Disease);


            }
            
            return BadRequest("Disease not found");*/


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
            return Ok(disease);   
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
