using System.Security.Cryptography;

namespace KanzApi.Common.Services;

public class CodeGenerator : ICodeGenerator
{

    public string Generate(string prefix, int length)
    {
        RandomNumberGenerator generator = RandomNumberGenerator.Create();

        byte[] randomNumber = new byte[length];
        generator.GetBytes(randomNumber);

        return prefix + Convert.ToHexString(randomNumber);
    }
}
