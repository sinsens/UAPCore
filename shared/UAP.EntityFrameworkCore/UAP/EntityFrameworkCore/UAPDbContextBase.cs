using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq.Expressions;
using UAP.Shared;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace UAP.EntityFrameworkCore
{
    public class UAPDbContextBase<TDbContext> : AbpDbContext<TDbContext>
        where TDbContext : DbContext
    {
        public UAPDbContextBase(DbContextOptions<TDbContext> options) : base(options)
        {
        }

        protected virtual bool IsMultiAppFilterEnabled => DataFilter?.IsEnabled<IMultiApp>() ?? false;

        protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
        {
            if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            if (typeof(IMultiApp).IsAssignableFrom(typeof(TEntity)))
            {
                return true;
            }

            return false;
        }

        protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
        {
            Expression<Func<TEntity, bool>> expression = null;


            if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
            {
                expression = e => !IsSoftDeleteFilterEnabled || !EF.Property<bool>(e, "IsDeleted");
            }

            if (typeof(IMultiTenant).IsAssignableFrom(typeof(TEntity)))
            {
				if(IsMultiTenantFilterEnabled && CurrentTenantId != null)
				{
					Expression<Func<TEntity, bool>> multiTenantFilter = e => EF.Property<Guid>(e, "TenantId") == CurrentTenantId;
                expression = expression == null ? multiTenantFilter : CombineExpressions(expression, multiTenantFilter);
				}
            }

            if (typeof(IMultiApp).IsAssignableFrom(typeof(TEntity)))
            {
                var currentApp = LazyServiceProvider?.LazyGetService<ICurrentApp>();
                if (currentApp != null && currentApp.AppId.HasValue)
                {
                    Expression<Func<TEntity, bool>> multiAppFilter =
                                        e => IsMultiAppFilterEnabled && ((IMultiApp)e).AppId == currentApp.AppId;
                    expression = expression == null
                        ? multiAppFilter
                        : CombineExpressions(expression, multiAppFilter);
                }
            }

            return expression;
        }
    }
}
