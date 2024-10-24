namespace Shared.Abstraction;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}