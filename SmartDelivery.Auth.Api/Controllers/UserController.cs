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
    public class UserController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;

        public UserController(ILogger<WeatherForecastController> logger, ICommandHandler<CreateUserCommand> createUserCommandHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateUserCommand command)
        {
            _createUserCommandHandler.Handle(command);
            return Ok(command);
        }
    }
}
