using Microsoft.EntityFrameworkCore;

namespace SGULibraryBE.Utilities
{
    public static class QueryableExtension
    {
        /// <summary>
        /// Include all reference from referenceCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="referenceCollection">Collection which hold reference expressions</param>
        /// <returns></returns>
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> queryable,
            ReferenceCollection<T> referenceCollection) where T : class
        {
            foreach (var expression in referenceCollection)
            {
                queryable = queryable.Include(expression);
            }

            return queryable;
        }
    }
}
