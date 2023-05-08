using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Business.ServiceInterfaces;
using UserManagement.Domain.Models;
using UserManagement.Presentation.WebAPI.Filter;
using UserManagement.Presentation.WebAPI.Helpers;
using UserManagement.Presentation.WebAPI.Models;
using UserManagement.Presentation.WebAPI.Services;
using UserManagement.Presentation.WebAPI.Wrappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagement.Presentation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public UserManagementController(IUserService userService, IMapper mapper, IUriService uriService)
        {
            _userService = userService;
            _mapper = mapper;
            _uriService = uriService;
        }


        // GET: api/<UserManagementController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            if (route == null)
            {
                return BadRequest();
            }
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var result = await _userService.GetPaginatedUsers(validFilter.PageNumber, validFilter.PageSize);
            var pagedUsers = result.Item1;
            var totalRecords = result.Item2;

            var pagedResponse = PaginationHelper.CreatePagedReponse(pagedUsers, validFilter, totalRecords, _uriService, route);

            return Ok(pagedResponse);
        }

    }
}
