using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAcess;
using Domain.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: api/<UserController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> GetAll()
        {
            var users = unitOfWork.User.GetAll();
            return Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<User>> GetUserById(int id)
        {
            var User =unitOfWork.User.GetById(id);
            if(id<=0)
            {
                return BadRequest("invalid input");
            }
            if (User == null)
            {
                return NotFound("User not found");
            }
            return Ok(User);
        }

        [HttpGet("{email}/{password}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<User>> GetByEmail(string email, string password)
        {
            var Users = unitOfWork.User.GetAll();
            int i = 0;
            foreach(User user in Users)
            {
                if (user.email == email && user.password == password)
                {
                    i = user.Id;
                }
            };
            var User = unitOfWork.User.GetById(i);
            if (User == null)
            {
                return NotFound("User not found");
            }


            return Ok(User);
        }

        // POST api/<UserController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> AddUser(User User)
        {
            unitOfWork.User.Add(User);
            unitOfWork.Save();
            return Ok(User);   
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<User>> UpdateUser(int id, User User)
        {
            var Usernw = unitOfWork.User.GetById(id);
            if (Usernw == null)
                return BadRequest("User not found");
            Usernw.firstName = User.firstName;
            Usernw.lastName = User.lastName;
            Usernw.email = User.email;
            Usernw.location= User.location;
            Usernw.password = User.password;
            unitOfWork.User.Update(Usernw);
            unitOfWork.Save();
            return Ok(Usernw);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<User>>> DeleteUser(int id)
        {
            var user = unitOfWork.User.GetById(id);
            if (user == null)
                return NotFound("No user found");
            unitOfWork.User.Remove(user);
            unitOfWork.Save();
            return Ok();
        }
    }
}
