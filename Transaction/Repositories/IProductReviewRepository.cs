using System.Linq.Expressions;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;
using KanzApi.Transaction.Models;

namespace KanzApi.Transaction.Repositories;

public interface IProductReviewRepository : ICrudRepository<ProductReview, Guid?>
{

    double RatingAverage(Expression<Func<ProductReview, bool>> predicate);

    PaginatedEntity<ProductReviewSummary> FindAllSummaries(Page page, Expression<Func<ProductReview, bool>>? predicate, Sort? sort);

    ProductReviewRatingsSummary? FindSummary(Expression<Func<ProductReview, bool>>? predicate);

    PaginatedEntity<ProductReviewItem> GetDetail(Page page, Expression<Func<ProductReview, bool>>? predicate, Sort? sort);
}
