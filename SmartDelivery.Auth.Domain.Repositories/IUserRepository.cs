using System;
using SmartDelivery.Auth.Domain.Model;

namespace SmartDelivery.Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        void Insert(User user);
        User Get(User user);
    }
}
