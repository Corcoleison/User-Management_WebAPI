using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using UserManagement.Application.Business.ServiceInterfaces;
using UserManagement.Domain.Models;
using UserManagement.Presentation.WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<ICollection<User>>> Get()
        {
            return Ok(await _userService.GetAllUsers());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userService.GetUser(id);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserDto userDto)
        {
            var newUser = _mapper.Map<User>(userDto);
            return Ok(await _userService.CreateUser(newUser));
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserDto userDto)
        {
            var newUpdatedUser = _mapper.Map<User>(userDto);
            newUpdatedUser.Id = id;

            var updatedUser = await _userService.UpdateUser(newUpdatedUser);
            if(updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult DeleteAsync(int id)
        {
            var newDeletedUser = _userService.DeleteUser(id);
            if(newDeletedUser == null)
            {
                return NotFound();
            }

            return Ok("Deletion completed on: " +newDeletedUser);
        }
    }
}
