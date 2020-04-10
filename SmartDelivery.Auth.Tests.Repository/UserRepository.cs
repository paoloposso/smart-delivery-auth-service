using System;
using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Repositories;

namespace SmartDelivery.Auth.Tests.Repository
{
    public class UserRepository : IUserRepository
    {
        public User Get(User user)
        {
            return new User("Teste", "01234567890", "teste@auth.com", "1234fiohfu8y744", "b78ejdwj");
        }

        public void Insert(User user)
        {
            user.SetId("1");
        }
    }
}
