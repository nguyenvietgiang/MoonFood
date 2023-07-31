using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MoonBussiness.Interface;
using MoonFood.Controllers;
using MoonModels.DTO.RequestDTO;
using MoonModels.DTO.ResponseDTO;

namespace MoonTest.UnitTest
{
    public class AuthTests
    {
        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var fakeAccountRepository = A.Fake<IAccountRepository>();
            var controller = new AccountController(fakeAccountRepository);
            var loginRequest = new LoginRequest
            {
                Username = "invalid_username",
                Password = "invalid_password"
            };
            A.CallTo(() => fakeAccountRepository.Login(loginRequest)).Returns(null);
            // Act
            var result = controller.Login(loginRequest);
            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }


    }
}
