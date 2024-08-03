namespace KanzApi.Common.Services;

public interface ICodeGenerator
{

    string Generate(string prefix, int length);
}
