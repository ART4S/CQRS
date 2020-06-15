using Dapper;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Contexts;
using WebFeatures.Application.Interfaces.Security;
using WebFeatures.Common;

namespace WebFeatures.Infrastructure.Security
{
    internal class AuthService : IAuthService
    {
        private readonly IWriteDbContext _db;

        public AuthService(IWriteDbContext db)
        {
            _db = db;
        }

        public Task<bool> UserHasPermissionAsync(Guid userId, string permission)
        {
            Guard.ThrowIfNullOrEmpty(permission, nameof(permission));

            return _db.Connection.ExecuteScalarAsync<bool>(
                sql: "user_has_permission",
                param: new { user_id = userId, permission },
                commandType: CommandType.StoredProcedure);
        }
    }
}