using System.Security.Cryptography;

namespace KanzApi.Security.Services;

public class PasswordService() : IPasswordService
{

    const string RandomChars = "1234567890qwertyuiopasdfghjklzxcvbnm";

    private void Generate(Span<char> password, string symbols)
    {
        HashSet<char> availableChars = new(symbols);

        Span<byte> randomBytes = stackalloc byte[password.Length];
        RandomNumberGenerator.Fill(randomBytes);

        for (int i = 0; i < password.Length; i++)
        {
            char randomAvailableChar = availableChars.ElementAt(randomBytes[i] % availableChars.Count);
            password[i] = randomAvailableChar;
            availableChars.Remove(randomAvailableChar);
        }
    }

    public string Generate()
    {
        return String.Create(10, RandomChars, Generate);
    }

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
