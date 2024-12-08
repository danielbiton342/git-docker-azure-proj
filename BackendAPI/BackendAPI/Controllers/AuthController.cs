using BackendAPI.Logic;
using BackendAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LogicService _logicService;
        private readonly ILogger<AuthController> _logger; 
        public AuthController(LogicService logicService, ILogger<AuthController> logger)
        {
            _logicService = logicService;            _logger = logger;

        }


        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserDto userLoginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(userLoginDto.Username) || string.IsNullOrEmpty(userLoginDto.Password))
                {
                    return BadRequest();
                }
                var isUserLogin = await _logicService.UserLogin(userLoginDto.Username, userLoginDto.Password);
                if (isUserLogin)
                {
                    return Ok(new { username = userLoginDto.Username });
                }
                else
                {
                    return NotFound(new { message = "Username is already taken." });
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Error while user login.");
                return StatusCode(500, "An error occured, please try again later.");

            }
            
            
        }

        [HttpPost("register")] 
        public async Task<IActionResult> UserRegister([FromBody] UserDto userRegisterDto)
        {
            try
            {
                if (string.IsNullOrEmpty(userRegisterDto.Username) || string.IsNullOrEmpty(userRegisterDto.Password))
                {
                    return BadRequest();
                }
                var resultUserRegistration = await _logicService.UserRegister(userRegisterDto.Username, userRegisterDto.Password);
                if (resultUserRegistration)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while user register.");
                return StatusCode(500, "An error occured, please try again later.");
               
            }
            
        }
    }
}
