using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace What2MakeAPI.Models
{
    public class RecipeSearchListModel
    {
        public List<RecipeSearchResultModel> recipes { get; set; }
    }
}
