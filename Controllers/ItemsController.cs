// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using AutoMapper;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Options;
// using Microsoft.IdentityModel.Tokens;
// using ShappingList.Entities;
// using ShappingList.Helpers;
// using ShappingList.Models.User;
// using ShappingList.Services;

// namespace ShappingList.Controllers {
//     [Authorize]
//     [ApiController]
//     [Route ("[controller]")]
//     public class ItemsController : ControllerBase {

//         private IUserService _userService;
//         private IMapper _mapper;
//         private readonly AppSettings _appSettings;

//         public ItemsController (IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings) 
//         {
//             _userService = userService;
//             _mapper = mapper;
//             _appSettings = appSettings.Value;
//         }

//         [Authorize]
//         [HttpGet]
//         public IActionResult Create([FromBody] model)
//         {
//             var item = 
//         } 
     



//     }
// }