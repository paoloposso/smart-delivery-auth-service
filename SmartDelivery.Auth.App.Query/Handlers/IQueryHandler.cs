using SmartDelivery.Auth.App.Query.Dto;

namespace SmartDelivery.Auth.App.Query.Handlers
{
    public interface IQueryHandler<TQuery, TInfo> where TQuery : IQuery where TInfo : IInfo
    {
        TInfo Handle(TQuery query);
    }
}