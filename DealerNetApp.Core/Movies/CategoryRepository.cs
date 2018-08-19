using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Movies
{
    public class CategoryRepository : ICategoryRepository
    {
        private DealerNetDbEntities db = new DealerNetDbEntities();

        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }
    }
}
