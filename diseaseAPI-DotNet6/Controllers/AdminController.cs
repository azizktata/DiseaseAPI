using diseaseAPI_DotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly diseaseDbContext _context;

        public AdminController (diseaseDbContext context)
        {
            _context = context;
        }
        // GET: api/<AdminController>
        [HttpGet]
        public async Task<ActionResult<List<Admin>>> Get()
        {
            return Ok(await _context.Admins.ToListAsync());
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Admin>>> Get(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return BadRequest("admin not found");
            }
            return Ok(admin);
        }

        // POST api/<AdminController>
        [HttpPost]
        public async Task<ActionResult<List<Admin>>> AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();  
            return Ok(admin);   
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Admin>>> UpdateAdmin(int id, Admin admin)
        {
            var adminnw = await _context.Admins.FindAsync(id);
            if (adminnw == null)
                return BadRequest("Admin not found");
            adminnw.username = admin.username;
            adminnw.password = admin.password;
            _context.Admins.Update(adminnw);
            await _context.SaveChangesAsync();
            return Ok(adminnw);
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Admin>>> DeleteAdmin(int id)
        {
            _context.Admins.Remove(_context.Admins.Find(id));   
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
