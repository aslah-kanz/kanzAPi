using System.Text;
using KanzApi.Utils;
using Microsoft.Data.SqlClient;

namespace KanzApi.Common.Exceptions;

public class DatabaseErrorException(Exception? innerException, string code, string message)
: CommonException(innerException, ErrorCode.DatabaseError, code, message)
{

    public static DatabaseErrorException From(Exception? innerException, SqlErrorCollection errors)
    {
        StringBuilder codes = new();
        StringBuilder messages = new();
        foreach (SqlError error in errors)
        {
            if (codes.Length > 0)
            {
                codes.Append(", ");
                messages.Append("; ");
            }

            codes.Append(error.Number);
            messages.Append(error.Message);
        }
        return new(innerException, codes.ToString(), messages.ToString());
    }
}
