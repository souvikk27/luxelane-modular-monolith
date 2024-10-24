using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Shared.DDD;

namespace Shared.Data.Interceptors;

public class AuditLoggingInterceptor : SaveChangesInterceptor
{
    private readonly ILogger<AuditLoggingInterceptor> _logger;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuditLoggingInterceptor(
        IHttpContextAccessor contextAccessor,
        ILogger<AuditLoggingInterceptor> logger)
    {
        _contextAccessor = contextAccessor;
        _logger = logger;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null)
        {
            return;
        }

        var user = _contextAccessor.HttpContext?.User.Identities
            .FirstOrDefault()?
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value;

        if (user is null or "Anonymous")
        {
            _logger.LogWarning("Unable to get user id from claims setting user as empty");
            user = "system";
        }

        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = user;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = user;
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    break;
            }
        }
    }
}