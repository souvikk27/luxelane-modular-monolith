namespace Shared.Abstraction;

public interface ISpecification<T>
{
    /// <summary>
    /// Gets the criteria for filtering the data.
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Gets the related entities to include in the query results.
    /// </summary>
    IEnumerable<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Gets the expressions for ordering the query results.
    /// </summary>
    IEnumerable<OrderingExpression<T>> OrderingExpressions { get; }

    /// <summary>
    /// Gets the maximum number of items to return from the query.
    /// </summary>
    int? Take { get; }

    /// <summary>
    /// Gets the number of items to skip in the query results for pagination.
    /// </summary>
    int? Skip { get; }
}