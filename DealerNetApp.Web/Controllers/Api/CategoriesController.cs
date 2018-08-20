using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DealerNetApp.Data;
using DealerNetApp.Core.Movies;

namespace DealerNetApp.Web.Controllers.Api
{
    [Authorize]
    public class CategoriesController : ApiController
    {
        private ICategoryRepository _cats;

        public CategoriesController(ICategoryRepository cats)
        {
            _cats = cats;
        }
        // GET: api/Categories
        public IHttpActionResult Get()
        {
            return Ok(_cats.GetCategories());
        }
    }
}
