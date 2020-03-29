using System;

namespace SmartDelivery.Auth.Domain.Model
{
    public class User
    {
        public string FullName { get; }
        public string Document { get; }

        public User (string fullname, string document) {
            FullName = fullname;
            Document = document;
        }      
    }
}
