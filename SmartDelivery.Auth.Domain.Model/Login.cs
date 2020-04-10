using System;

namespace SmartDelivery.Auth.Domain.Model
{
    public class Login
    {
        // public string Email { get; }
        // public string Password { get; }
        
        public Payload Payload { get; private set; }

        // public Login (string email, string password)
        // {
        //     Email = email;
        //     Password = password;
        // }

        public void SetPayload(string subject, string issuer, DateTime expiration, string email)
        {
            Payload = new Payload (subject, issuer, expiration, email);
        }
    }

    public class Payload
    {
        public string Email { get; }
        public string Subject { get; }
        public string Issuer { get; }
        public  DateTime Expiration { get; }

        public Payload(string subject, string issuer, DateTime expiration, string email)
        {
            Subject = subject;
            Issuer = issuer;
            Expiration = expiration;
            Email = email;
        }
    }
}