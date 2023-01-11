using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace diseaseAPI_DotNet6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLogin : ControllerBase
        
    {
        private readonly IJwtTokenManager _JwtTokenManager;
        private readonly IUnitOfWork _unitOfWork;
        public AdminLogin(IJwtTokenManager jwtTokenManager, IUnitOfWork unitOfWork)
        {
            _JwtTokenManager = jwtTokenManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] AdminCredential credential)
        {
            
            var token = _JwtTokenManager.Authenticate(credential.username, credential.password);
            if(string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok(token);
        }

        
    }
}
