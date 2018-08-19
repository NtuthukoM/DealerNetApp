using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DealerNetApp.Data;

namespace DealerNetApp.Core.Movies
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
    }
}
