using System.Numerics;
using System.Text;

namespace KanzApi.Utils;

public sealed class GuidBase62
{

    private const string Base62Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static string Encode(Guid guid)
    {
        byte[] guidBytes = guid.ToByteArray();
        BigInteger number = new(guidBytes, isUnsigned: true, isBigEndian: false);
        StringBuilder builder = new();
        do
        {
            int remainder = (int)(number % 62);
            builder.Insert(0, Base62Chars[remainder]);
            number /= 62;
        } while (number > 0);

        return builder.ToString();
    }

    public static Guid Decode(string encoded)
    {
        BigInteger number = new();
        foreach (char c in encoded)
        {
            number = number * 62 + Base62Chars.IndexOf(c);
        }

        byte[] guidBytes = number.ToByteArray(isUnsigned: true, isBigEndian: false);
        if (guidBytes.Length > 16)
        {
            Array.Resize(ref guidBytes, 16);
        }
        else if (guidBytes.Length < 16)
        {
            Array.Resize(ref guidBytes, 16);
        }

        return new Guid(guidBytes);
    }

    public static Guid GenerateGuidByString(string value)
    {
		return new Guid(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)).Take(16).ToArray());
	}
}
