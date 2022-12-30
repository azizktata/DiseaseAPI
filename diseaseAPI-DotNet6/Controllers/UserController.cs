using diseaseAPI_DotNet6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly diseaseDbContext _context;

        public UserController (diseaseDbContext context)
        {
            _context = context;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<User>>> Get(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return BadRequest("User not found");
            }
            return Ok(User);
        }

        [HttpGet("{email}/{password}")]
        public async Task<ActionResult<List<User>>> GetByEmail(string email, string password)
        {
            var Users = await _context.Users.ToListAsync();
            int i = 0;
            Users.ForEach(user =>
            {
                if (user.email == email && user.password == password)
                {
                    i = user.Id;
                }
            });
            var User = await _context.Users.FindAsync(i);
            if (User == null)
            {
                return NotFound("User not found");
            }


            return Ok(User);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();  
            return Ok(User);   
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User User)
        {
            var Usernw = await _context.Users.FindAsync(id);
            if (Usernw == null)
                return BadRequest("User not found");
            Usernw.firstName = User.firstName;
            Usernw.lastName = User.lastName;
            Usernw.email = User.email;
            Usernw.location= User.location;
            Usernw.password = User.password;
            _context.Users.Update(Usernw);
            await _context.SaveChangesAsync();
            return Ok(Usernw);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            _context.Users.Remove(_context.Users.Find(id));   
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
