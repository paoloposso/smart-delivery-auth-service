using System;

namespace SmartDelivery.Auth.Domain.Model
{
    public class Login
    {
        public Payload Payload { get; private set; }

        public void SetPayload(string subject, DateTime expiration, string email)
        {
            Payload = new Payload(subject, expiration, email);
        }
    }

    public class Payload
    {
        public string Email { get; }
        public string Sub { get; }
        public  DateTime Exp { get; }

        public Payload(string subject, DateTime expiration, string email)
        {
            Sub = subject;
            Exp = expiration;
            Email = email;
        }
    }
}