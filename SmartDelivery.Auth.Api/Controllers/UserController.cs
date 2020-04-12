using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.App.Query.Dto;
using SmartDelivery.Auth.App.Query.Handlers;

namespace SmartDelivery.Auth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly ICommandHandler<CreateUserCommand> _createUserCommandHandler;
        private readonly ICommandHandler<LoginCommand> _loginCommandHandler;
        private readonly IQueryHandler<GetUserByTokenQuery, GetUserByTokenInfo> _getUserByTokenHandler;

        public UserController(ILogger<UserController> logger, ICommandHandler<CreateUserCommand> createUserCommandHandler, 
            ICommandHandler<LoginCommand> loginCommandHandler, IQueryHandler<GetUserByTokenQuery, GetUserByTokenInfo> getUserByTokenHandler)
        {
            _createUserCommandHandler = createUserCommandHandler;
            _loginCommandHandler = loginCommandHandler;
            _getUserByTokenHandler = getUserByTokenHandler;
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Post([FromBody]LoginCommand command)
        {
            try
            {
                _loginCommandHandler.Handle(command);
                return Ok(command.Token);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateUserCommand command)
        {
            _createUserCommandHandler.Handle(command);
            return Ok(command);
        }

        [HttpGet]
        [Route("Me/{token}")]
        public IActionResult Get(string token)
        {
            var info = _getUserByTokenHandler.Handle(new GetUserByTokenQuery {
                Token = token
            });

            return Ok(info);
        }
    }
}
