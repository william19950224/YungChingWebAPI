using LogicService.Interface;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel.Request;
using RequestResponseModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YungChingWebAPI.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase {

        internal IUserService _service;

        public UserController(IUserService service) {
            this._service = service;
        }
        [HttpGet]
        public async Task<RServiceProvider<List<UserResponse>>> UserInfo() {
            return await _service.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<RServiceProvider<UserResponse>> UserInfo(int id) {
            return await _service.GetUser(id);
        }
        [HttpPost]
        public async Task<RServiceProvider<int>> UserInfo([FromBody] UserRequest request) {
            return await _service.InsertUser(request);
        }

        [HttpPut("{id}")]
        public async Task<RServiceProvider<int>> UserInfo(long id, [FromBody] UserRequest request) {
            return await _service.ModifyUser(id, request);
        }

        [HttpDelete("{id}")]
        public async Task<RServiceProvider<int>> UserInfo(long id) {
            return await _service.DeleteUser(id);
        }
    }

}