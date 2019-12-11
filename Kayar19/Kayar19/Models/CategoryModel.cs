using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kayar19.Models
{
   public class CategoryModel
    {
        [JsonProperty("Category_Id")]
        public string Category_Id { get; set; }

        [JsonProperty("Category_Name")]
        public string Category_Name { get; set; }
        [JsonProperty("Date_Created")]
        public string Date_Created { get; set; }
    }
    public class GetCatModel
    {
        public List<CategoryModel> categories { get; set; }
        public bool success { get; set; }

    }
}
