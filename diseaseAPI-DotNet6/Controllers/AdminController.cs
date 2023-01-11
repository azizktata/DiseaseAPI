using Domain.Models;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public AdminController (IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: api/<AdminController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public  ActionResult<List<Admin>> GetAllAdmins()
        {
            return Ok(unitOfWork.Admin.GetAll());
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Admin>> GetAdmin(int id)
        {
           /* var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return BadRequest("admin not found");
            }
            return Ok(admin);*/
           var admin = unitOfWork.Admin.GetById(id);
            if (admin == null)
            {
                return NotFound("admin not found");
            }
            return Ok(admin);

        }

        // POST api/<AdminController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Admin>> AddAdmin(Admin admin)
        {
           /* _context.Admins.Add(admin);
            await _context.SaveChangesAsync();  */
            unitOfWork.Admin.Add(admin);
            unitOfWork.Save();
            return Ok(admin);   
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Admin>> UpdateAdmin(int id, Admin admin)
        {
            var adminnw = unitOfWork.Admin.GetById(id);
            if (adminnw == null)
                return NotFound("Admin not found");
            adminnw.username = admin.username;
            adminnw.password = admin.password;
           // _context.Admins.Update(adminnw);
           // await _context.SaveChangesAsync();
            unitOfWork.Admin.Update(adminnw);
            unitOfWork.Save();
            return Ok(adminnw);
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Admin>> DeleteAdmin(int id)
        {
            var admin =  unitOfWork.Admin.GetById(id);
            if(admin == null)
            {
                return NotFound("Admin not found");
            }
            unitOfWork.Admin.Remove(admin);
            unitOfWork.Save();
            //_context.Admins.Remove(_context.Admins.Find(id));   
            //await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
