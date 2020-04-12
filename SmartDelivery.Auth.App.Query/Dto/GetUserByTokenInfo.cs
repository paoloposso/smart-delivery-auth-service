namespace SmartDelivery.Auth.App.Query.Dto
{
    public class GetUserByTokenInfo : IInfo
    {
        public string Email { get; set; }
        public string Id { get; set; }
    }
}