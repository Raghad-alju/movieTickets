using movieTickets.data_context;
using movieTickets.Models;
using System.Diagnostics.Metrics;

namespace movieTickets
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }


        public void SeedDataContext()
        {
            if (!dataContext.MovieExperiences.Any())
            {
                var movieExps = new List<MovieExperience>()
                {
                    new MovieExperience()
                    {
                        Movie = new Movie()
                        {
                            Title = "Pikachu",
                            ReleaseDate = new DateTime(1903,1,1),
                            Geners = new List<Genre>
                            {
                              new Genre{ GenreName="a" },
                              new Genre{ GenreName="b" },
                              new Genre{ GenreName="c" }
                                },

                      }, }

                };
                dataContext.MovieExperiences.AddRange(movieExps);
                dataContext.SaveChanges();
            }
        }
    }
}
