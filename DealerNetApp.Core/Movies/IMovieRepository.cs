using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Movies
{
    public interface IMovieRepository
    {
        List<Movie> GetMovies(int? catId = null, string prodName= "");
    }
}
