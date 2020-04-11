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
        public string Sub { get; }
        public string Iss { get; }
        public  DateTime Exp { get; }

        public Payload(string subject, string issuer, DateTime expiration, string email)
        {
            Sub = subject;
            Iss = issuer;
            Exp = expiration;
            Email = email;
        }
    }
}