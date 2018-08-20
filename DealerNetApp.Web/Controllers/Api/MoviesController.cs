using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealerNetApp.Core.Movies;

namespace DealerNetApp.Web.Controllers.Api
{
    public class MoviesController : ApiController
    {
        IMovieRepository _movieRepo;

        public MoviesController(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        // GET: api/Movies
        public IHttpActionResult Get(int? catId = null, string prodName = "")
        {
            return Ok(_movieRepo.GetMovies(catId, prodName));
        }
    }
}
