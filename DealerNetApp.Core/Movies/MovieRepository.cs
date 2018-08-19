using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Movies
{
    public class MovieRepository : IMovieRepository
    {
        private DealerNetDbEntities db = new DealerNetDbEntities();

        public List<Movie> GetMovies(int? catId = default(int?), string prodName = "")
        {
            return db.Movies.Where(m =>
            (catId.HasValue ? m.CategoryId == catId : true) &&
            (prodName == "" ? true : m.MovieName.Contains(prodName))
            ).ToList();
        }
    }
}
