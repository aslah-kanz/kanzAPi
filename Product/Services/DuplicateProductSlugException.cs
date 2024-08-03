using KanzApi.Common.Exceptions;
using KanzApi.Utils;

namespace KanzApi.Product.Services;

public class DuplicateProductSlugException(string slug) : CommonException(ErrorCode.DuplicateProductSlug, slug) { }
