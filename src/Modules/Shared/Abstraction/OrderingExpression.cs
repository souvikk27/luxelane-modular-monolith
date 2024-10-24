namespace Shared.Abstraction;
public class OrderingExpression<T>
{
    /// <summary>
    /// Gets or sets the expression used to select the key for ordering.
    /// </summary>
    public Expression<Func<T, object>>? OrderingKeySelector { get; set; }

    /// <summary>
    /// Gets or sets the direction of the ordering (ascending or descending).
    /// </summary>
    public OrderingDirection Direction { get; set; }
}

/// <summary>
/// Specifies the direction of ordering.
/// </summary>
public enum OrderingDirection
{
    /// <summary>
    /// Indicates that the results should be ordered in ascending order.
    /// </summary>
    Ascending,

    /// <summary>
    /// Indicates that the results should be ordered in descending order.
    /// </summary>
    Descending
    
}