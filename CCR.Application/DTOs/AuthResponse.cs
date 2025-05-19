namespace CCR.Application.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public UserDto User { get; set; } = default!;

    }
}
