using System.Collections.Generic;
using AutoMapper;
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

        //! [Authorize]
        [HttpPost("new")]
        public IActionResult Create(UserGroupModel model)
        {
            var userGroup = _mapper.Map<UserGroupModel, UserGroup>(model);

            try
            {
                _userGroupService.Create(userGroup);

                return Ok(userGroup);
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }

        //! [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_userGroupService.GetAll());
        }

        //! [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_userGroupService.GetById(id));
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserGroupModel model)
        {
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
            _userGroupService.Delete(id);
            return Ok();
        }

        //! [Authorize]
        [HttpPost("{userGroupId}/users/{userId}")]
        public IActionResult AddUser(int userGroupId, int userId)
        {
            _userGroupService.AddUser(userGroupId, userId);

            var group = _userGroupService.GetById(userGroupId);

            return Ok(group);
        }

        [HttpDelete("{userGroupId}/users/{userId}")]
        public IActionResult RemoveUser(int userGroupId, int userId)
        {
            _userGroupService.RemoveUser(userGroupId, userId);

            var group = _userGroupService.GetById(userGroupId);

            return Ok(group);
        }

        [HttpPost("{userGroupId}/itemlists/new")]
        public IActionResult AddItemList(int userGroupId, [FromBody] ItemListModel model)
        {
            var itemList = _mapper.Map<ItemList>(model);
            var group = _userGroupService.GetById(userGroupId);

            _itemListService.Create(itemList);
            group.ItemList = itemList;

            return Ok(group);


        }
    }
}