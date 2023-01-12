using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAcess;

using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Domain.Interfaces;
using DataAcess.UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {

        private readonly IUnitOfWork _UnitOfWork;

        public DiseaseController (IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;
        }
        // GET: api/<DiseaseController>
        
        [HttpGet()]
        public  ActionResult<List<Disease>> Get()
        {
            return Ok( _UnitOfWork.Disease.GetDiseases());
           
        }

        
        // GET api/<DiseaseController>/diseaseName
        [HttpGet("{diseaseName:alpha}")]
        public ActionResult<List<Disease>> GetDiseaseByName(string diseaseName)
        {
            //List<Disease> Diseases = await _context.Diseases.ToListAsync();
            var disease = _UnitOfWork.Disease.Find(d => d.diseaseName== diseaseName).FirstOrDefault();

            if (disease == null)
                return NotFound();

            return Ok(_UnitOfWork.Disease.GetDiseasesByName(diseaseName));

           

        }

        [HttpGet("{id:int}")]
        public ActionResult<List<Disease>> GetById(int id)
        {
            //List<Disease> Diseases = await _context.Diseases.ToListAsync();
            if (id<= 0)
                return BadRequest("Invalid input");

            var disease = _UnitOfWork.Disease.GetById(id);
            if (disease == null)
                return NotFound("disease not found");

            return Ok(disease);


        }

        // POST api/<DiseaseController>
        [HttpPost]
        public ActionResult<List<Disease>> AddDisease(AddDiseaseDto request)
        {
            if (request.diseaseName == null)
                return BadRequest("invalid Name");
            var disease = new Disease
            {
                diseaseName = request.diseaseName,
            };
            _UnitOfWork.Disease.Add(disease);
            _UnitOfWork.Save();  
            return Ok(disease);   
        }


        // DELETE api/<DiseaseController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Disease>>> DeleteDisease(int id)
        {
            if (id <= 0)
                return BadRequest("invalid input");
            var disease = _UnitOfWork.Disease.GetById(id);
            if (disease == null)
                return NotFound();
            _UnitOfWork.Disease.Remove(disease);
            _UnitOfWork.Save();
            return Ok();
        }
    }
}
