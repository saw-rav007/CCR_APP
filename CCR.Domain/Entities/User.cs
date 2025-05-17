namespace CCR.Domain.Entities;

public class User(string name, string email, string phoneNumber, string passwordHash)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string PasswordHash { get; private set; } = passwordHash;
}
