using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShappingList.Entities;
using ShappingList.Helpers;
using ShappingList.Models.User;

namespace ShappingList.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

                var user = _userService.Authenticate(model.Username, model.Password);

                if (user == null)
                    return BadRequest(new { message = "no such user in the database" });

                //return basic user info and authentication token 
                //TODO: information returned below is all user data -> it contains hashed password and salted password. Map it to model without sensitive data.
                var result = _mapper.Map<UserModel>(user);
                return Ok(result);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userService.Create(user, model.Password);
                return Ok($"{user.Username} created");
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                var users = _userService.GetAll();
                var model = _mapper.Map<IList<UserModel>>(users);
                return Ok(model);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            try
            {
                var user = _userService.GetById(id);

                if (user == null)
                    return NotFound();

                var result = _mapper.Map<UserModel>(user);

                return Ok(user);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
            try
            {
                _userService.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost("{userId}/invitations/{invitationId}/accept")]
        public IActionResult AcceptInvitation(int invitationId)
        {
            try
            {
                _userService.AcceptInvitation(invitationId);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }

        }

        [HttpPost("{userId}/invitations/{invitationId}/decline")]
        public IActionResult DeclineInvitation(int invitationId)
        {
            try
            {
                _userService.DeclineInvitation(invitationId);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }

        }

        [HttpGet("{userId}/invitations")]
        public IActionResult ShowAllInvitations(int userId)
        {
            try
            {
                var invitations = _userService.ShowAllInvitations(userId);
                return Ok(invitations);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

    }
}