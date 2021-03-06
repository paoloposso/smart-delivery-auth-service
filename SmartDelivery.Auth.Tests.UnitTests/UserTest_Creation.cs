using System;
using MongoDB.Driver;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.App.Query.Dto;
using SmartDelivery.Auth.App.Query.Handlers;
using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Services;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb;
using NUnit.Framework;
using Microsoft.IdentityModel.Tokens;
using SmartDelivery.Auth.Domain.Services.Strategies.TokenGeneration;

namespace SmartDelivery.Auth.Tests.UnitTests
{
    [TestFixture]
    public class UserTest_Creation
    {
        private CreateUserCommandHandler _createUserCommand;
        private LoginCommandHandler _loginCommandHandler;
        private UserRepository _userRepository;
        private IMongoDatabase _database;
        private string _jwtSecret = "d4s56d4ss7fsd8fsdf4fsdFFgkfpok45jioifn";

        public UserTest_Creation()
        {
            var cnnString = "mongodb://192.168.99.100:27017/TestDb";

            _userRepository = new UserRepository(new AppSettings{ MongoDbCnnString = cnnString });
            _createUserCommand = new CreateUserCommandHandler(_userRepository);
            _loginCommandHandler = new LoginCommandHandler(_userRepository, new LoginService(new JwtTokenGeneratorStrategy(new AppSettings { JwtSecret = _jwtSecret })));

            var mongoUrl = new MongoUrl(cnnString);

            _database = new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName);
        }

        [SetUp]
        public void SetUp() {}

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
        public void ShouldThrowAgumentExceptionWhenUserNotComplete()
        {
            var command = new CreateUserCommand() {
                Document = "33880317895",
                Email = "pvictorsys@gmail.com",
                Password = "123456"
            };

            Assert.That(() => _createUserCommand.Handle(command), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void ShouldInsertSecondUser()
        {
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
                Password = "123456"
            };

            _loginCommandHandler.Handle(command);

            Assert.IsFalse(string.IsNullOrEmpty(command.Token));
        }

        [Test]
        public void ShouldThrowUnauthorizedException()
        {
            var command = new LoginCommand() {
                Email = "pvictorsys@gmail.com",
                Password = "12345"
            };

            Assert.That(() => _loginCommandHandler.Handle(command), Throws.TypeOf<UnauthorizedAccessException>());
        }

        [Test]
        public void ShouldReturnUser()
        {
            var user = _userRepository.Get(new User(null, null, "pvictorsys@gmail.com", "123456"));

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
        public void ShouldNotReturnUserByIdInvalidId()
        {
            var user = _userRepository.Get("5e91fca7909f081e34400462");

            Assert.AreEqual(null, user);
        }

        [Test]
        public void ShouldThrowSecurityTokenExpiredException()
        {
            var handler = new GetUserByTokenQueryHandler(new LoginService(new JwtTokenGeneratorStrategy(new AppSettings { JwtSecret = _jwtSecret })));

            Assert.That(() => handler.Handle(new GetUserByTokenQuery {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InB2aWN0b3JzeXNAZ21haWwuY29tIiwic3ViIjoiNWU5MjhmNWZkNDkwYzgyZWY4NTQ1MDBjIiwianRpIjoiOGEzZGU5ZjQtZWFhOC00YzNkLWIxNWItMmIwOGE4N2UxMjliIiwiZXhwIjoxNTg2NjY1MTE0LCJpc3MiOiJkZWxpdmVyeSJ9.jC5FtPDwn4Qs9gz6hrgaXttoXA59y75N2mZbJjzg1oc"
            }), Throws.TypeOf<SecurityTokenExpiredException>());
        }
    }
}
