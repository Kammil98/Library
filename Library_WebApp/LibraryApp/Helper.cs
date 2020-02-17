using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp {

    public static class Helper {

        public static IOrderedQueryable<T> OrderByQuery<T>(this IQueryable<T> queryable, string orderQuery) {
            bool descending = false;
            if (orderQuery.EndsWith("_desc")) {
                orderQuery = orderQuery[0..^5];
                descending = true;
            }
            if (descending) {
                return queryable.OrderByDescending(e => EF.Property<object>(e, orderQuery));
            }
            else {
                return queryable.OrderBy(e => EF.Property<object>(e, orderQuery));
            }
        }

        public static IQueryable<Models.Author> BookAuthors(this LibraryContext context, int bookId) {
            return context.Author.FromSqlInterpolated($"SELECT * FROM BookAuthors({bookId})");
        }

        public static IQueryable<Models.Book> FilterBooks(this LibraryContext context,
            string titleFilter, string publishingHouseFilter, string genre, int? branchNumber, string state) {
            genre = genre == "" ? null : genre;
            state = state == "" ? null : state;
            return context.Book.FromSqlInterpolated($"EXEC FilterBooks {titleFilter}, {publishingHouseFilter}, {genre}, {branchNumber}, {state}");
        }

        public static async Task BorrowCopy(this LibraryContext context, int copyId, string login) {
            await context.Database.ExecuteSqlInterpolatedAsync($"EXEC BorrowCopy {copyId}, {login}");
        }

        public static async Task ReserveCopy(this LibraryContext context, int copyId, string login) {
            await context.Database.ExecuteSqlInterpolatedAsync($"EXEC ReserveCopy {copyId}, {login}");
        }

        public static async Task ReturnCopy(this LibraryContext context, int copyId) {
            await context.Database.ExecuteSqlInterpolatedAsync($"EXEC ReturnCopy {copyId}");
        }
    }
}
