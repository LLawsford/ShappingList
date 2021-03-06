using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShappingList.Entities;
using ShappingList.Helpers;
using ShappingList.Models.Item;
using ShappingList.Models.ItemList;

namespace ShappingList.Controllers
{


    //! [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemListsController : ControllerBase
    {

        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IItemListService _itemListService;
        private readonly IItemService _itemService;

        public ItemListsController(IMapper mapper, IOptions<AppSettings> appSettings, IItemListService itemListService, IItemService itemService)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _itemListService = itemListService;
            _itemService = itemService;
        }

        //! [Authorize]
        [HttpPost("new")]
        public IActionResult Create([FromBody]ItemListModel model)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // map model to entity
            var itemList = _mapper.Map<ItemList>(model);


            try
            {
                // create item
                _itemListService.Create(itemList);
                return Ok(itemList);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }


        }

        //! [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var itemLists = _itemListService.GetAll();
                return Ok(itemLists);
            }
            catch(AppException ex) 
            { 
                return BadRequest(new { message = ex }); 
            }
        }

        //! [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ItemListUpdateModel model)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // map model to entity and set id
            var itemList = _mapper.Map<ItemList>(model);
            itemList.Id = id;

            try
            {
                // update item list
                _itemListService.Update(itemList);
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
            

            try
            {
                _itemListService.Delete(id);
                return Ok();
            }
            catch(AppException ex) 
            { 
                return BadRequest(new { message = ex }); 
            }
        }

        //! [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            

            try
            {
                var itemList = _itemListService.GetById(id);
                return Ok(itemList);
            }
            catch(AppException ex) 
            { 
                return BadRequest(new { message = ex }); 
            }
        }


        //! [Authorize]
        [HttpPost("{id}/items/new")]
        public IActionResult AddItem(int id, [FromBody]ItemModel model)
        {


            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var item = _mapper.Map<Item>(model);

            try
            {
                var list = _itemListService.GetById(id);


                //? Maybe there's better way to assign list to item than on a controller side. 
                //? Something better than override for Create method that takes list argument?

                //TODO: Cover all endpoints, nulls etc. in controllers and services later on! 
                //TODO: Try-catches! 
                item.List = list;


                _itemService.Create(item);


                return Ok(list);
            }
            catch(AppException ex) 
            { 
                return BadRequest(new { message = ex }); 
            }

            
        }

        [HttpDelete]
        public IActionResult DeleteItem([FromQuery]int listId, [FromQuery]int itemId)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {

            var list = _itemListService.GetById(listId);
            _itemService.Delete(itemId);

            return Ok(list);
            }
            catch(AppException ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}