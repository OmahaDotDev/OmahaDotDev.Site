using Microsoft.EntityFrameworkCore;
using OmahaDotDev.Model.Common;
using OmahaDotDev.Model.Exceptions;
using System.Linq.Expressions;

namespace OmahaDotDev.ResourceAccess.Database
{
    static class EfExtensions
    {
        public static void RemoveWhere<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> condition) where T : class
        {
            var range = dbSet.Where(condition);
            dbSet.RemoveRange(range);
        }

        public static async Task<SkipTakeSet<T>> AsSkipTakeSet<T>(this IQueryable<T> query, int skip, int take,
            CancellationToken cancellationToken)
        {
            var result = new SkipTakeSet<T>(await query.Skip(skip).Take(take).ToListAsync(cancellationToken))
            {
                TotalRecords = await query.CountAsync(),
                Skipped = skip,
                Taken = take
            };
            return result;
        }

        public static async Task<T> FirstOrDefaultThrowIfNotFound<T>(this DbSet<T> dbSet, Expression<Func<T, bool>> condition, string entityName, int entityIdentifier, CancellationToken cancellationToken) where T : class
        {
            var item = await dbSet.FirstOrDefaultAsync(condition, cancellationToken);

            if (item == null)
            {
                throw new NotFoundException(entityName, entityIdentifier);
            }

            return item;
        }
    }
}
