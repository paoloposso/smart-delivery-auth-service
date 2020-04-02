using System;

namespace SmartDelivery.Auth.Domain.Model
{
    public class User
    {
        public string FullName { get; }
        public string Document { get; }
        public string Email { get; }
        public string Password { get; }

        public string Id { get; private set; }

        public User (string fullname, string document, string email, string password) {
            FullName = fullname;
            Document = document;
            Email = email;
            Password = password;
        }

        public void SetId (string id)
        {
            Id = id;
        }
    }
}
