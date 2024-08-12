using backend.Core;
using backend.Infrastructure.Extentions;
using backend.Interface;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using static backend.Enum.Constants;

namespace backend.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : BaseApiController<UserController>
    {
        private readonly IExcelMergerService _excelMergerService;
        private readonly IConfiguration _config;

        public UserController(IBaseApiControllerDependencies<UserController> dependencies, 
            IConfiguration config, IExcelMergerService excelMergerService): base(dependencies)
        {
            _excelMergerService = excelMergerService;
            _config = config;
        }

        /// <summary>
        /// Provides List of all users
        /// </summary>
        /// <param name="status">Filter Status Active = 0(Default), Inactive=1, All = 3</param>
        /// <returns>Array of Users</returns>
        [HttpGet()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<UserResponse>), 200)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers([FromQuery] USER_STATUS_FILTER status)
        {
            try
            {
                return (await _excelMergerService.GetUserAsync(status)).Pipe(res => Ok(res));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = "Error Ocurred when processing the request, Please Conttact the Administrator" });
            }
        }
       
    }
}
