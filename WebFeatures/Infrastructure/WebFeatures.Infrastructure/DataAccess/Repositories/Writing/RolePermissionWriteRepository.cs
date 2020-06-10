using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebFeatures.Application.Interfaces.DataAccess.Repositories.Writing;
using WebFeatures.Common.Extensions;
using WebFeatures.Domian.Entities.Permissions;
using WebFeatures.Infrastructure.DataAccess.Extensions;
using WebFeatures.Infrastructure.DataAccess.Mappings.Common;
using WebFeatures.Infrastructure.DataAccess.Mappings.Profiles;

namespace WebFeatures.Infrastructure.DataAccess.Repositories.Writing
{
    internal class RolePermissionWriteRepository : WriteRepository<RolePermission>, IRolePermissionWriteRepository
    {
        public RolePermissionWriteRepository(IDbConnection connection, IEntityProfile profile) : base(connection, profile)
        {
        }

        public Task<IEnumerable<RolePermission>> GetByUserIdAsync(Guid userId)
        {
            IEntityMap<UserRole> userRole = Profile.GetMap<UserRole>();
            IEntityMap<Role> role = Profile.GetMap<Role>();
            IEntityMap<RolePermission> permission = Entity;

            string sql =
                @$"SELECT 
                    {permission.Properties.Select(x => $"p.{x.ColumnName}").JoinString()} 
                FROM 
                    {permission.Table.NameWithSchema()} p 
                JOIN 
                    {role.Table.NameWithSchema()} r ON r.{role.Column(x => x.Id)} = p.{permission.Column(x => x.RoleId)} 
                JOIN 
                    {userRole.Table.NameWithSchema()} ur ON ur.{userRole.Column(x => x.RoleId)} = r.{role.Column(x => x.Id)} 
                WHERE 
                    ur.{userRole.Column(x => x.UserId)} = @userId";

            return Connection.QueryAsync<RolePermission>(sql, new { userId });
        }
    }
}