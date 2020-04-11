using System;
using MongoDB.Driver;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Services;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb;
using NUnit.Framework;

namespace SmartDelivery.Auth.Tests.UnitTests
{
    [TestFixture]
    public class UserTest_Creation
    {
        private CreateUserCommandHandler _createUserCommand;
        private LoginCommandHandler _loginCommandHandler;
        private UserRepository _userRepository;
        private IMongoDatabase _database;

        private string InsertedId = string.Empty;

        public UserTest_Creation()
        {
            var cnnString = "mongodb://192.168.99.100:27017/SmartDeliveryAuthTestDb";

            _userRepository = new UserRepository(cnnString);
            _createUserCommand = new CreateUserCommandHandler(_userRepository);
            _loginCommandHandler = new LoginCommandHandler(_userRepository, new LoginService());

            var mongoUrl = new MongoUrl(cnnString);

            _database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
        }

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void ShouldInsertUser()
        {
            _database.DropCollection("users");

            var command = new CreateUserCommand() {
                Document = "33880317895",
                Email = "pvictorsys@gmail.com",
                FullName = "Paolo Posso",
                Password = "123456"
            };

            _createUserCommand.Handle(command);

            Assert.AreNotEqual(null, command.Id);
        }

        [Test]
        public void ShouldThrowApplicationExceptionWhenUserNotComplete()
        {
            var command = new CreateUserCommand() {
                Document = "33880317895",
                Email = "pvictorsys2@gmail.com",
                Password = "123456"
            };

            Assert.That(() => _createUserCommand.Handle(command), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ShouldInsertSecondUser()
        {
            _database.DropCollection("users");

            var command = new CreateUserCommand() {
                Document = "01234567890",
                Email = "pvictorsys2@gmail.com",
                FullName = "John Rambo",
                Password = "12345678"
            };

            _createUserCommand.Handle(command);

            Assert.AreNotEqual(null, command.Id);
        }

        [Test]
        public void ShouldReturnToken()
        {
            var command = new LoginCommand() {
                Email = "pvictorsys@gmail.com",
                Password = "123456",
                Issuer = "delivery"
            };

            _loginCommandHandler.Handle(command);

            Assert.IsFalse(string.IsNullOrEmpty(command.Token));
        }

        [Test]
        public void ShouldThrowUnauthorizedException()
        {
            var command = new LoginCommand() {
                Email = "pvictorsys@gmail.com",
                Password = "12345",
                Issuer = "delivery"
            };

            Assert.That(() => _loginCommandHandler.Handle(command), Throws.TypeOf<UnauthorizedAccessException>());
        }

        [Test]
        public void ShouldReturnUser()
        {
            var user = _userRepository.Get(new User(null, null, "pvictorsys@gmail.com", "123456"));

            InsertedId = user.Id;

            Assert.AreNotEqual(null, user);
            Assert.AreEqual("pvictorsys@gmail.com", user.Email);
        }

        [Test]
        public void ShouldNotReturnUser_IncorrectPassword()
        {
            var user = _userRepository.Get(new User(null, null, "pvictorsys@gmail.com", "12345"));

            Assert.AreEqual(null, user);
        }

        [Test]
        public void ShouldReturnUserById()
        {
            var user = _userRepository.Get(InsertedId);

            Assert.AreNotEqual(null, user);
            Assert.AreEqual("pvictorsys@gmail.com", user.Email);
        }

        [Test]
        public void ShouldNotReturnUserByIdInvalidId()
        {
            var user = _userRepository.Get("5e91fca7909f081e34400462");

            Assert.AreEqual(null, user);
        }
    }
}
