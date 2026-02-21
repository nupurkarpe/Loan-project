using LosLms.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LosLms.Application.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<PagedResult<T>> ToPagedAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 10;

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
    }
}
