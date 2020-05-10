using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShappingList.Entities;
using ShappingList.Helpers;
using ShappingList.Models.Item;
using ShappingList.Models.ItemList;
using ShappingList.Models.UserGroup;

namespace ShappingList.Controllers
{

    //! [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserGroupsController : ControllerBase
    {
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IUserGroupService _userGroupService;
        private readonly IItemListService _itemListService;

        public UserGroupsController(
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IUserGroupService userGroupService,
            IItemListService itemListService)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _userGroupService = userGroupService;
            _itemListService = itemListService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("new")]
        public IActionResult Create(UserGroupModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userGroup = _mapper.Map<UserGroupModel, UserGroup>(model);

            try
            {
                _userGroupService.Create(userGroup);

                return Ok(userGroup);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        //TODO: User groups controller can be accessed only by loged in users. 
        //TODO: Users can manipulate data here if: 
        // They have admin role
        // They have manager role (beside manipulating items) and list.Users contains current user with that role

        //! [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {

            try
            {
                return Ok(_userGroupService.GetAll());
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //! [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                return Ok(_userGroupService.GetById(id));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserGroupModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userGroup = _mapper.Map<UserGroup>(model);
            userGroup.Id = id;

            try
            {
                // update item list
                _userGroupService.Update(userGroup);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }

        //! [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            //TODO: some kind of ListManager role to handle stuff like this
            try
            {
                _userGroupService.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //! [Authorize]
        [HttpPost("{userGroupId}/users/{userId}")]
        public IActionResult InviteUser(int userGroupId, int userId)
        {
            //? maybe there should be some form of invitations/requests to join groups
            try
            {
                _userGroupService.AddInvitation(userId, userGroupId);
                

                return Ok($"Invitation sent to user with Id: {userId}");
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{userGroupId}/users/{userId}")]
        public IActionResult RemoveUser(int userGroupId, int userId)
        {
            try
            {

                _userGroupService.RemoveUser(userGroupId, userId);

                var group = _userGroupService.GetById(userGroupId);

                return Ok(group);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        [HttpPost("{userGroupId}/itemlists/new")]
        public IActionResult AddItemList(int userGroupId, [FromBody] ItemListModel model)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var itemList = _mapper.Map<ItemList>(model);

            try
            {

                var group = _userGroupService.GetById(userGroupId);

                _itemListService.Create(itemList);
                group.ItemList = itemList;

                return Ok(group);
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex });
            }

        }

    }
}