using ShoeperStar.Models.ViewModels;
using ShoeperStar.Models;
using Microsoft.EntityFrameworkCore;
using ShoeperStar.Models.DTO;

namespace ShoeperStar.Data.Repository.Query
{
    public static class ShoesQueryExtensions
    {
        public static IQueryable<Shoe> SearchShoes(this IQueryable<Shoe> shoes, string searchValue)
        {

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                return shoes;
            }

            searchValue = searchValue.Trim().ToLower();

            return shoes.Where(s => s.Name.ToLower().Contains(searchValue))
                            .AsNoTracking();
        }

        public static IQueryable<Shoe> FilterShoes(this IQueryable<Shoe> shoes, ShoeFilterDTO shoeFilter)
        {
            if (shoeFilter.BrandId != 0)
            {
                shoes = shoes.Where(s => s.BrandId.Equals(shoeFilter.BrandId))
                            .AsNoTracking();
            }

            if (shoeFilter.GenderId != 0)
            {
                shoes = shoes.Where(s => s.GenderId.Equals(shoeFilter.GenderId))
                            .AsNoTracking();
            }

            if (shoeFilter.CategoryId != 0)
            {
                shoes = shoes.Where(s => s.CategoryId.Equals(shoeFilter.CategoryId))
                            .AsNoTracking();
            }

            return shoes;
        }

        public static IOrderedQueryable<Shoe> SortShoes(this IQueryable<Shoe> shoes, string orderByQueryString)
        {
            var orderQuery = orderByQueryString.Trim().Split(' ');
            var sortField = orderQuery[0];
            var sortOrder = orderQuery[1];

            if (sortOrder == "asc")
            {
                // a compressed switch case statement
                return sortField switch
                {
                    nameof(ShoeVM.BrandName) => shoes.OrderBy(s => s.Brand.Name),
                    nameof(ShoeVM.CategoryName) => shoes.OrderBy(s => s.Catergory.Name),
                    nameof(ShoeVM.GenderName) => shoes.OrderBy(s => s.Gender.Name),
                    _ => shoes.OrderBy(s => s.Name),
                };
            }

            return sortField switch
            {
                nameof(ShoeVM.BrandName) => shoes.OrderByDescending(s => s.Brand.Name),
                nameof(ShoeVM.CategoryName) => shoes.OrderByDescending(s => s.Catergory.Name),
                nameof(ShoeVM.GenderName) => shoes.OrderByDescending(s => s.Gender.Name),
                _ => shoes.OrderByDescending(s => s.Name),
            };


            // NOT APPLICABLE SINCE WE ARE ALSO SORTING USING NAVIGATION PROPERTY FIELDS
            //if (string.IsNullOrWhiteSpace(orderByQueryString))
            //    return shoes.OrderBy(s => s.Name);

            //var orderQuery = OrderQueryBuilder.OrderQuery<Shoe>(orderByQueryString);

            //if (string.IsNullOrWhiteSpace(orderQuery))
            //    return shoes.OrderBy(e => e.Name);

            //return shoes.OrderBy(orderQuery);
        }
    }
}
