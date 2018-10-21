//-------------------------------------------------------------------------------------------------
// <copyright file="BaseDbContext.cs" company="Nootus">
//  Copyright (c) Nootus. All rights reserved.
// </copyright>
// <description>
//  Base class for DBContext.
// </description>
//-------------------------------------------------------------------------------------------------
namespace Nootus.Fabric.Web.Core.SqlServer.Repositories
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Nootus.Fabric.Web.Core.SqlServer.Entities;

    public class BaseDbContext<TContext> : DbContext
        where TContext : DbContext
    {
        public BaseDbContext(DbContextOptions<TContext> options)
            : base(options)
        {
        }

        public virtual IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters)
            where TEntity : class
        {
            return this.Set<TEntity>().FromSql(sql, parameters);
        }

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return await this.Database.ExecuteSqlCommandAsync(sql, default(CancellationToken), parameters);
        }

        protected void IgnoreAuditFields<TEntity>(ModelBuilder builder)
            where TEntity : BaseEntity
        {
            builder.Entity<TEntity>(e =>
            {
                e.Ignore(p => p.CreatedDate);
                e.Ignore(p => p.CreatedUserId);
                e.Ignore(p => p.LastModifiedUserId);
            });
        }
    }
}
