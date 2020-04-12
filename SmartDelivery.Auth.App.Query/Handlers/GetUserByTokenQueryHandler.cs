using SmartDelivery.Auth.App.Query.Dto;
using SmartDelivery.Auth.Domain.Services;

namespace SmartDelivery.Auth.App.Query.Handlers
{
    public class GetUserByTokenQueryHandler : IQueryHandler<GetUserByTokenQuery, GetUserByTokenInfo>
    {
        LoginService _loginService;

        public GetUserByTokenQueryHandler(LoginService loginService)
        {
            _loginService = loginService;
        }

        public GetUserByTokenInfo Handle(GetUserByTokenQuery query)
        {
            var payload = _loginService.GetUserInfoByToken(query.Token);
            
            return new GetUserByTokenInfo() {
                Email = payload.Email,
                Id = payload.Sub
            };
        }
    }
}