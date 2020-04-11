using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;

namespace SmartDelivery.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ICommandHandler<LoginCommand> _loginCommandHandler;

        public LoginController(ILogger<LoginController> logger, ICommandHandler<LoginCommand> createUserCommandHandler)
        {
            _loginCommandHandler = createUserCommandHandler;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]LoginCommand command)
        {
            try
            {
                _loginCommandHandler.Handle(command);
                return Ok(command);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }
    }
}
