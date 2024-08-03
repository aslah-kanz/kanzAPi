using KanzApi.Configurations.Contexts;
using KanzApi.Common.Repositories;
using KanzApi.Transaction.Entities;
using System.Linq.Expressions;
using KanzApi.Transaction.Models;
using KanzApi.Common.Models;
using KanzApi.Common.Models.Param;
using KanzApi.Extensions;
using MapsterMapper;
namespace KanzApi.Transaction.Repositories;

public class ProductReviewRepository(CommonDbContext context, IMapper mapper)
: CrudRepository<ProductReview, Guid?>(context, context.ProductReviews), IProductReviewRepository
{
	private readonly IMapper _mapper = mapper;

	public double RatingAverage(Expression<Func<ProductReview, bool>>? predicate)
    {
        IQueryable<ProductReview> query = _set;
        if (predicate != null) query = query.Where(predicate);

        double result = query.Where(p => p.Rating != null)
        .Select(p => (double)p.Rating!)
        .Average();
        return Math.Round(result, 2, MidpointRounding.AwayFromZero);
    }

    public PaginatedEntity<ProductReviewSummary> FindAllSummaries(
        Page page, Expression<Func<ProductReview, bool>>? predicate, Sort? sort)
    {
        IQueryable<ProductReview> query = _set;
        if (predicate != null) query = query.Where(predicate);

        IQueryable<ProductReviewSummary> querySummary = query
        .GroupBy(e => new { e.PurchaseQuote!.StoreId, e.PurchaseQuote.ProductId })
        .Select(g => new ProductReviewSummary()
        {
            StoreId = g.Key.StoreId,
            ProductId = g.Key.ProductId,
            TotalRating = g.Sum(p => p.Rating),
            ReviewerCount = g.Count(p => p.Rating != null),
            RatingAverage = g.Average(p => p.Rating)
        });

        int count = querySummary.Count();

        if (sort != null) querySummary = querySummary.Sort(sort);

        // fix wrong paging as its not start from 1 (found from UI)
        // if 0 is received, we still return the result from page 1
        var currentIndex = (page.Index < 1) ? 1 : page.Index;

        IEnumerable<ProductReviewSummary> entities = [.. querySummary
        .Skip((currentIndex - 1) * page.Size)
        .Take(page.Size)];

        return new PaginatedEntity<ProductReviewSummary>(entities, page.Index, page.Size, count);
    }

    public ProductReviewRatingsSummary? FindSummary(Expression<Func<ProductReview, bool>>? predicate)
    {
        IQueryable<ProductReview> query = _set;
        if (predicate != null) query = query.Where(predicate);

        return query
        .GroupBy(e => new { e.PurchaseQuote!.StoreId, e.PurchaseQuote.ProductId })
        .Select(g => new ProductReviewRatingsSummary()
        {
            TotalRating = g.Sum(p => p.Rating),
            ReviewerCount = g.Count(p => p.Rating != null),
            RatingAverage = Math.Round(g.Average(p => p.Rating) ?? 0.0d, 1, MidpointRounding.ToZero),
            Rating1 = g.Count(p => p.Rating == 1),
            Rating2 = g.Count(p => p.Rating == 2),
            Rating3 = g.Count(p => p.Rating == 3),
            Rating4 = g.Count(p => p.Rating == 4),
            Rating5 = g.Count(p => p.Rating == 5)
        }).SingleOrDefault();
    }

	public PaginatedEntity<ProductReviewItem> GetDetail(Page page, Expression<Func<ProductReview, bool>>? predicate, Sort? sort)
	{
		IQueryable<ProductReview> query = _set;
		if (predicate != null) query = query.Where(predicate);

		IQueryable<ProductReviewItem> result = query
		.Select(x => new ProductReviewItem()
		{
			Id = x.Id!.Value,
			Comment = x.Comment,
			CreatedAt = x.CreatedAt!.Value,
			UpdatedAt = x.UpdatedAt!.Value,
			Images = _mapper.Map<List<ImageResponse>>(x.Images),
			Rating = x.Rating ?? 0
		});

		int count = result != null ? result.Count() : 0;

		if (sort != null) result = result.Sort(sort);

		// fix wrong paging as its not start from 1 (found from UI)
		// if 0 is received, we still return the result from page 1
		var currentIndex = (page.Index < 1) ? 1 : page.Index;

		IEnumerable<ProductReviewItem> entities = [.. result
		.Skip((currentIndex - 1) * page.Size)
		.Take(page.Size)];

		return new PaginatedEntity<ProductReviewItem>(entities, page.Index, page.Size, count);
	}
}
