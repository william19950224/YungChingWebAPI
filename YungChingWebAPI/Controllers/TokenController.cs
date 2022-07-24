using LogicService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YungChingWebAPI.Helper;

namespace YungChingWebAPI.Controllers {
    [ApiController]
    [Route("[controller]/[action]")]
    public class TokenController : ControllerBase {

        internal JwtHelper _jwt;
        internal IUserService _service;
        public TokenController(IUserService service, JwtHelper jwt) {
            this._jwt = jwt;
            this._service = service;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> SignInAsync([FromBody] LoginViewModel login) {
            if (await ValidateUserAsync(login)) {
                return _jwt.GenerateToken(login.Account);
            } else {
                return BadRequest();
            }
        }
        private async Task<bool> ValidateUserAsync(LoginViewModel login) {
            var val = await _service.GetUser(login.Account, login.Password);
            if (val != null && val.Success && val.Result != null) {
                return true;
			} else {
                return false;
            }
        }

        [HttpGet]
        public IActionResult GetClaims() {
            var x = User.Claims.ToList();
            var xf = User.Identity;
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [HttpGet]
        public IActionResult GetUserName() {
            return Ok(User.Identity.Name);
        }

        [HttpGet]
        public IActionResult GetUniqueId() {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            return Ok(jti.Value);
        }
    }
}
