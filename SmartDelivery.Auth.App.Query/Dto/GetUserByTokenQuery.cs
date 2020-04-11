namespace SmartDelivery.Auth.App.Query.Dto
{
    public class GetUserByTokenQuery : IQuery
    {
        public int Token { get; set; }
    }
}