﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RequestResponseModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace YungChingWebAPI.Controllers {
    [ApiController]
    [Route("api/error")]

	public class ErrorController : Controller {
		public IActionResult Index() {
			return View();
		}
        [AllowAnonymous]
        public ActionResult<ErrorResponseVM> Error() {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (ex != null) {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorResponseVM() {
                        errorno = 999,
                        message = ex.Error.Message
                    });
            } else {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorResponseVM() {
                        errorno = 999,
                        message = "ERROR OCCURRED!"
                    });
            }

        }
    }
}
