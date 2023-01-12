using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using DataAcess;
using Microsoft.AspNetCore.Authorization;
using Domain.Interfaces;
using DataAcess.UnitOfWork;
using Azure.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Doctor_infoController : ControllerBase
    {

        private readonly IUnitOfWork _UnitOfWork;

        public Doctor_infoController (IUnitOfWork UnitOfWork)
        {
            _UnitOfWork= UnitOfWork;
        }
        // GET: api/<Doctor_infoController>
        [HttpGet]
        public  ActionResult<List<Doctor_info>> Get()
        {
            return Ok(_UnitOfWork.Doctor_Info.GetAll());
        }

        
        // GET api/<Doctor_infoController>/diseaseName
        [HttpGet("{diseaseId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Doctor_info>> GetByDiseaseId(int diseaseId)
        {
            var Doctors = _UnitOfWork.Doctor_Info.GetAll();
            List<Doctor_info> Selected = new List<Doctor_info>();
            if (diseaseId <= 0)
            {
                return BadRequest();
            }
            foreach (Doctor_info doctor in Doctors)
            {
                if (doctor.diseaseId == diseaseId)
                    Selected.Add(doctor);

            }

            /*if (Selected.Count == 0)
            {
                return NotFound("Article not found");
            }*/

            return Ok(Selected);
        }

        // POST api/<Doctor_infoController>
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Doctor_info>> AddDoctor_info(AddDoctor_InfoDTO request)
        {
            if (request.diseaseId <= 0)
            {
                return BadRequest("invalid request");
            }

            var Doctor_info = new Doctor_info
            {
                name = request.name,
                speciality= request.speciality,
                location= request.location,
                diseaseId= request.diseaseId


            };


            _UnitOfWork.Doctor_Info.Add(Doctor_info);
            _UnitOfWork.Save();

            return Ok(Doctor_info);
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
        public ActionResult<List<Doctor_info>> DeleteDoctor_info(int id)
        {
            if (id <= 0)
                return BadRequest("invalid input");
            var doctor_info =_UnitOfWork.Doctor_Info.GetById(id);
            if (doctor_info == null)
                return NotFound();
            _UnitOfWork.Doctor_Info.Remove(doctor_info);
            _UnitOfWork.Save();
            return Ok();
        }
    }
}
