using System;
using BrMobi.ApplicationServices;
using BrMobi.Core.Entities;
using BrMobi.Core.RepositoryInterfaces;
using BrMobi.Core.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BrMobi.Tests
{
    [TestClass]
    public class PreUserServiceTest
    {
        [TestMethod]
        public void Create_PreUser_EmailIsEmpty_ReturnError()
        {
            // Arrange
            var mockRepository = new MockRepository(MockBehavior.Strict);

            var preUser = new PreUser(DateTime.Now);
            string error;

            // Act
            var preUserService = new PreUserService(null);
            preUserService.Create(preUser, out error);

            // Assert
            Assert.AreEqual<string>(ResourceHelper.InvalidEmail(), error);
        }

        [TestMethod]
        public void Create_PreUser_EmailIsInvalid_ReturnError()
        {
            // Arrange
            var mockRepository = new MockRepository(MockBehavior.Strict);

            var preUser = new PreUser(DateTime.Now);
            preUser.Email = "@domain.com";
            string error;

            // Act
            var preUserService = new PreUserService(null);
            preUserService.Create(preUser, out error);

            // Assert
            Assert.AreEqual<string>(ResourceHelper.InvalidEmail(), error);
        }

        [TestMethod]
        public void Create_PreUser_EmailAlreadyRegistered_ReturnError()
        {
            // Arrange
            var mockRepository = new MockRepository(MockBehavior.Strict);

            var preUser = new PreUser(DateTime.Now);
            preUser.Email = "email@domain.com";
            string error;

            var preUserRepository = mockRepository.Create<IPreUserRepository>();
            preUserRepository.Setup(handler => handler.Count(preUser.Email)).Returns(1);

            // Act
            var preUserService = new PreUserService(preUserRepository.Object);
            preUserService.Create(preUser, out error);

            // Assert
            Assert.AreEqual<string>(ResourceHelper.AlreadyRegisteredEmail(), error);
        }
    }
}