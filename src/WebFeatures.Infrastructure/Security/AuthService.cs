using System;
using System.Data;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;
using WebFeatures.Infrastructure.DataAccess.QueryExecutors;

namespace WebFeatures.Infrastructure.Security
{
    internal class AuthService : IAuthService
    {
        private readonly IWriteDbContext _db;
        private readonly IDbExecutor _executor;

        public AuthService(IWriteDbContext db, IDbExecutor executor)
        {
            _db = db;
            _executor = executor;
        }

        public Task<bool> UserHasPermissionAsync(Guid userId, string permission)
        {
            Guard.ThrowIfNullOrEmpty(permission, nameof(permission));

            return _executor.ExecuteScalarAsync<bool>(
                connection: _db.Connection,
                sql: "user_has_permission",
                param: new { user_id = userId, permission },
                commandType: CommandType.StoredProcedure);
        }
    }
}