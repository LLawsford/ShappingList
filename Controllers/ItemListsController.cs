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
            var itemLists = _itemListService.GetAll();
            return Ok(itemLists);
        }

        //! [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ItemListUpdateModel model )
        {

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
            _itemListService.Delete(id);
            return Ok();
        }

        //! [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var itemList = _itemListService.GetById(id);
            return Ok(itemList);
        }
        
        [HttpPost("{id}/items/new")]
        public IActionResult AddItem(int id, [FromBody]ItemModel model)
        {
            var item = _mapper.Map<Item>(model);
            var list = _itemListService.GetById(id);

            item.List = list;


            _itemService.Create(item);


            return Ok(list);
        }
    }
}