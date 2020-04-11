using SmartDelivery.Auth.App.Query.Dto;

namespace SmartDelivery.Auth.App.Query.Handlers
{
    public interface IQueryHandler<TQuery> where TQuery : IQuery
    {
        void Handle(TQuery query);
    }
}