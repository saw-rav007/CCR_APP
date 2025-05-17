using CCR.Application.DTOs;
using CCR.Application.Interfaces;
using CCR.Domain.Entities;
using CCR.Domain.Interfaces;

namespace CCR.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> RegisterUserAsync(RegisterUserRequest request)
        {
            if (await _userRepository.ExistsAsync(request.Email))
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            // Just for demo: hash is not secure here, use proper hashing in production
            var passwordHash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(request.Password));

            var user = new User(
                name: request.Name,
                email: request.Email,
                phoneNumber: request.PhoneNumber,
                passwordHash: passwordHash
            );

            var savedUser = await _userRepository.AddAsync(user);

            return new UserDto
            {
                Id = savedUser.Id,
                Name = savedUser.Name,
                Email = savedUser.Email
            };
        }
    }
}
