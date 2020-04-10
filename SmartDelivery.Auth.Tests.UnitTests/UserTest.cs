using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartDelivery.Auth.App.Command;
using SmartDelivery.Auth.App.Command.Dto;
using SmartDelivery.Auth.App.Command.Handlers;
using SmartDelivery.Auth.Domain.Model;
using SmartDelivery.Auth.Domain.Services;
using SmartDelivery.Auth.Infrastructure.Repositories.MongoDb;
using InfraDb = SmartDelivery.Auth.Infrastructure.Repositories.MongoDb;

namespace SmartDelivery.Auth.Tests.UnitTests
{
    [TestClass]
    public class UserTest
    {
        private CreateUserCommandHandler _createUserCommand;
        private LoginCommandHandler _loginCommandHandler;
        private UserRepository _userRepository;

        public UserTest()
        {
            _createUserCommand = new CreateUserCommandHandler(new UserRepository());
            _loginCommandHandler = new LoginCommandHandler(new UserRepository(), new LoginService());
            _userRepository = new UserRepository();
        }

        [TestMethod]
        public void ShouldInsertUser()
        {
            var command = new CreateUserCommand() {
                Document = "33880317895",
                Email = "pvictorsys@gmail.com",
                FullName = "Paolo Posso",
                Password = "123456"
            };

            _createUserCommand.Handle(command);

            Assert.AreNotEqual(null, command.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void ShouldThrowApplicationExceptionWhenUserNotComplete()
        {
            var command = new CreateUserCommand() {
                Document = "33880317895",
                Email = "pvictorsys@gmail.com",
                Password = "123456"
            };

            _createUserCommand.Handle(command);

            Assert.AreNotEqual(null, command.Id);
        }

        [TestMethod]
        public void ShouldReturnToken()
        {
            var command = new LoginCommand() {
                Email = "pvictorsys@gmail.com",
                Password = "123456"
            };

            _loginCommandHandler.Handle(command);

            Assert.IsFalse(string.IsNullOrEmpty(command.Token));
        }

        [TestMethod]
        public void ShouldReturnUser()
        {
            var user = _userRepository.Get(new User(null, null, "pvictorsys@gmail.com", null));

            Assert.AreNotEqual(null, user);
            Assert.AreEqual("pvictorsys@gmail.com", user.Email);
        }
    }
}
