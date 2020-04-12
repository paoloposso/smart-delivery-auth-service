namespace SmartDelivery.Auth.App.Query.Dto
{
    public class GetUserByTokenQuery : IQuery
    {
        public string Token { get; set; }
    }
}