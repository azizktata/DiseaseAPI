using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using DataAcess;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Doctor_infoController : ControllerBase
    {

        private readonly diseaseDbContext _context;

        public Doctor_infoController (diseaseDbContext context)
        {
            _context = context;
        }
        // GET: api/<Doctor_infoController>
        [HttpGet]
        public async Task<ActionResult<List<Doctor_info>>> Get()
        {
            return Ok(await _context.Doctors.ToListAsync());
        }

        
        // GET api/<Doctor_infoController>/diseaseName
        [HttpGet("{diseaseId:int}")]
        public async Task<ActionResult<List<Doctor_info>>> GetByDiseaseName(int diseaseId)
        {
            List<Doctor_info> Doctor_infos = await _context.Doctors.ToListAsync();
            List<Doctor_info> Selected = new List<Doctor_info>();
            foreach(Doctor_info Doctor_info in Doctor_infos)
            {
                if (Doctor_info.diseaseId == diseaseId)
                    Selected.Add(Doctor_info);
                    
            }
            if (Selected.Count == 0)
            {
                return BadRequest("Doctor_info not found");
            }
            return Ok(Selected);
        }

        // POST api/<Doctor_infoController>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<Doctor_info>>> AddDoctor_info(Doctor_info Doctor_info)
        {
            _context.Doctors.Add(Doctor_info);
            _context.SaveChanges();
            var disease =  await _context.Diseases.FindAsync(Doctor_info.diseaseId);
            if (disease == null)
                return BadRequest("disease not found");
            //var Doctor_infos = new List<Doctor_info>();
            //Doctor_infos.Add(Doctor_info);
            disease.doctors.Add(Doctor_info);
            

            //Doctor_infos.ToList<Doctor_info>().ForEach(tchr => _context.Entry(tchr).State = EntityState.Added);
            //_context.Entry(disease.Doctor_infos).State = EntityState.Added;
            //var entry = _context.Entry(disease);
            

            //disease.Doctor_infos.ForEach(item => _context.Entry(item).State = EntityState.Modified);
            foreach(Doctor_info item in disease.doctors)
            {
              
                _context.Entry(item).State = EntityState.Modified;
             

            }
            _context.Diseases.Entry(disease).State = EntityState.Added;
            _context.Diseases.Update(disease);
            _context.SaveChanges();
            
            await _context.SaveChangesAsync();  
            return Ok(disease);   
        }

        // PUT api/<Doctor_infoController>/5
        /* [HttpPut("{id}")]
         public async Task<ActionResult<List<Doctor_info>>> UpdateDoctor_info(int id, Doctor_info Doctor_info)
         {
             var Doctor_infonw = await _context.Doctor_infos.FindAsync(id);
             if (Doctor_infonw == null)
                 return BadRequest("Doctor_info not found");
             Doctor_infonw.firstName = Doctor_info.firstName;
             Doctor_infonw.lastName = Doctor_info.lastName;
             Doctor_infonw.email = Doctor_info.email;
             Doctor_infonw.location= Doctor_info.location;
             Doctor_infonw.password = Doctor_info.password;
             _context.Doctor_infos.Update(Doctor_infonw);
             await _context.SaveChangesAsync();
             return Ok(Doctor_infonw);
         }
        */
        // DELETE api/<Doctor_infoController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Doctor_info>>> DeleteDoctor_info(int id)
        {
            _context.Doctors.Remove(_context.Doctors.Find(id));   
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
