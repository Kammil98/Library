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

        public static IQueryable<Models.Book> FilterBooks(this LibraryContext context,
            string titleFilter, string publishingHouseFilter, string genre, int? branchNumber, string state) {
            genre = genre == "" ? null : genre;
            state = state == "" ? null : state;
            return context.Book.FromSqlInterpolated($"SELECT * FROM FilterBooks({titleFilter}, {publishingHouseFilter}, {genre}, {branchNumber}, {state})");
        }

        public static IQueryable<Models.Author> BookAuthors(this LibraryContext context, int bookId) {
            return context.Author.FromSqlInterpolated($"SELECT * FROM BookAuthors({bookId})");
        }
    }
}
