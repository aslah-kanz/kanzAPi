namespace KanzApi.Security.Services;

public interface IPasswordService
{

    string Generate();

    string Hash(string password);

    bool Verify(string password, string hashedPassword);
}
