using KanzApi.Utils;

namespace KanzApi.Common.Exceptions;

public class ReviewSubmittedException() : CommonException(ErrorCode.ReviewSubmitted) { }
