using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShappingList.Entities;
using ShappingList.Helpers;
using ShappingList.Models.Item;

namespace ShappingList.Controllers
{


    //! [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {

        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IItemService _itemService;

        public ItemsController(IMapper mapper, IOptions<AppSettings> appSettings, IItemService itemService)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _itemService = itemService;
        }

        //! [Authorize]
        [HttpPost("new")]
        public IActionResult Create([FromBody]ItemModel model)
        {


            // map model to entity
            var item = _mapper.Map<Item>(model);


            try
            {
                // create item
                _itemService.Create(item);
                return Ok(item);
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
            var items = _itemService.GetAll();
            var model = _mapper.Map<IList<ItemModel>>(items);
            return Ok(model);
        }

        //! [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ItemUpdateModel model )
        {

            // map model to entity and set id
            var item = _mapper.Map<Item>(model);
            item.Id = id;

            try
            {
                // update item 
                _itemService.Update(item);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }



        //TODO: HttpDelete to remove item based on id
        //! [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _itemService.Delete(id);
            return Ok();
        }
        


    }
}