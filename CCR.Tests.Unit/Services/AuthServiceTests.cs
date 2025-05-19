using CCR.Application.DTOs;
using CCR.Domain.Entities;
using CCR.Application.Interfaces;
using CCR.Application.Services;
using CCR.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

using Moq;

namespace CCR.Tests.Unit.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly IAuthService _authService;
        public AuthServiceTests()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _configMock = new Mock<IConfiguration>();

            _configMock.Setup(c => c["Jwt:Key"]).Returns("TestSuperSecretKeyHereThatIsAtLeast32Bytes");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("CCR");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("CCRUsers");
            _configMock.Setup(c => c["Jwt:ExpireMinutes"]).Returns("10");

            _authService = new AuthService(_userRepoMock.Object, _configMock.Object);
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsAuthResponse()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "Password123"
            };

            var fakeUser = new User(
            name: "Test User",
            email: request.Email,
            phoneNumber: "0000000000", // Use a dummy value
            passwordHash: BCrypt.Net.BCrypt.HashPassword(request.Password)
            );


            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync(fakeUser);

            // Act
            var result = await _authService.LoginAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
            Assert.Equal(fakeUser.Email, result.User.Email);
        }

        [Fact]
        public async Task LoginAsync_InvalidEmail_ThrowsUnauthorized()
        {
            var request = new LoginRequest
            {
                Email = "wrong@example.com",
                Password = "any"
            };

            _userRepoMock.Setup(r => r.GetByEmailAsync(request.Email)).ReturnsAsync((User?)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.LoginAsync(request));
        }


    }
}