using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShappingList.Entities;
using ShappingList.Helpers;
using ShappingList.Models.User;
using ShappingList.Services;

namespace ShappingList.Controllers {
    [Authorize]
    [ApiController]
    [Route ("[controller]")]
    public class UsersController : ControllerBase {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController (IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings) {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost ("authenticate")]
        public IActionResult Authenticate ([FromBody] AuthenticateModel model) {

            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest (new { message = "no such user in database" });

            

            //return basic user info and authentication token 
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user =  _userService.GetById(id);

            if (user == null)
                return NotFound();

            var result = _mapper.Map<UserModel>(user);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UpdateModel model)
        {

            //TODO: only admins and owners of these accounts should be able to update this data
            // map model to entity and set id
            var user = _mapper.Map<User>(model);
            user.Id = id;

            try
            {
                // update user 
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //TODO: only admins and owners of these accounts should be able remove them

            _userService.Delete(id);
            return Ok();
        }

     





    }
}