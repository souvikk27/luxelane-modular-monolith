namespace Shared.Abstraction;

public static class ExpressionExtensions
{
    /// <summary>
    /// Combines two boolean expressions using a logical AND operation.
    /// </summary>
    /// <typeparam name="T">The type of the input expression.</typeparam>
    /// <param name="left">The first expression to combine.</param>
    /// <param name="right">The second expression to combine.</param>
    /// <returns>A new expression that represents the logical AND of the two input expressions.</returns>
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.AndAlso(Expression.Invoke(left, parameter), Expression.Invoke(right, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Combines two boolean expressions using a logical OR operation.
    /// </summary>
    /// <typeparam name="T">The type of the input expression.</typeparam>
    /// <param name="left">The first expression to combine.</param>
    /// <param name="right">The second expression to combine.</param>
    /// <returns>A new expression that represents the logical OR of the two input expressions.</returns>
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T));
        var body = Expression.OrElse(Expression.Invoke(left, parameter), Expression.Invoke(right, parameter));
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}