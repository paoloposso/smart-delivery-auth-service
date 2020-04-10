using System;
using System.Security.Cryptography;
using System.Text;

namespace SmartDelivery.Auth.Domain.Model
{
    public class User : IModel
    {
        private string _password;
        public string FullName { get; }
        public string Document { get; }
        public string Email { get; }
        public string Password
        { 
            get { return ComputeSha256Hash(_password); }
            set { _password = value; } 
        }

        public string Id { get; private set; }

        public User (string fullname, string document, string email, string password, string id = null) 
        {
            FullName = fullname;
            Document = document;
            Email = email;
            Password = password;
            Id = id;
        }

        public void SetId (string id)
        {
            Id = id;
        }

        private string ComputeSha256Hash(string rawData)  
        {
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                var builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }

                return builder.ToString();  
            }  
        } 
    }
}
