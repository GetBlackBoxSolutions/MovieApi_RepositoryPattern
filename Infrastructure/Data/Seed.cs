using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!await context.Ratings.AnyAsync())
            {
                var ratings = new List<Rating>{
                    new Rating
                    {
                        Type = "G"
                    },
                    new Rating
                    {
                        Type = "PG"
                    },
                    new Rating
                    {
                        Type = "PG-13"
                    },
                    new Rating
                    {
                        Type = "R"
                    },
                    new Rating
                    {
                        Type = "NC-17"
                    },
                };

                await context.Ratings.AddRangeAsync(ratings);
                await context.SaveChangesAsync();
            }

            if (!await context.Genres.AnyAsync()) { 
                var genres = new List<Genre>
                {
                    new Genre
                    {
                        Type = "Action"
                    },
                    new Genre
                    {
                        Type = "Adventure"
                    },
                    new Genre
                    {
                        Type = "Comedy"
                    },
                    new Genre
                    {
                        Type = "Drama"
                    },
                    new Genre
                    {
                        Type = "Horror"
                    },
                    new Genre
                    {
                        Type = "Mystery"
                    },
                    new Genre
                    {
                        Type = "Romance"
                    },
                    new Genre
                    {
                        Type = "Sci-Fi"
                    },
                    new Genre
                    {
                        Type = "Thriller"
                    },
                };  

                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();
            }

            if (!await context.Movies.AnyAsync())
            {
                var genres = await context.Genres.ToListAsync();
                var ratings = await context.Ratings.ToListAsync();

                //populate with movies
                var movies = new List<Movie>
                {
                    new Movie
                    {
                        Title = "The Shawshank Redemption",
                        Description = "Two imprisoned",
                        YearReleased = 1994,
                        RatingId = ratings.First(r => r.Type == "R").Id,
                        MovieGenres = new List<Genre>
                        {
                            genres[3],
                            genres[6]
                        }
                    },
                    new Movie
                    {
                        Title = "The Godfather",
                        Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                        YearReleased = 1972,
                        RatingId = ratings.First(r => r.Type == "R").Id,
                        MovieGenres = new List<Genre>
                        {
                            genres[3],
                            genres[6]
                        }
                    },
                    new Movie
                    {
                        Title = "The Dark Knight",
                        Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                        YearReleased = 2008,
                        RatingId = ratings.First(r => r.Type == "PG-13").Id,
                        MovieGenres = new List<Genre>
                        {
                            genres[0],
                            genres[8]
                        }
                    },
                };

                await context.Movies.AddRangeAsync(movies);
                await context.SaveChangesAsync();
            }

        }
    }
}
