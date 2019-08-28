using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_store.Models.Data
{
    [Table("tbCategories")]
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Slug { get; set; }

        public int Sorting { get; set; }

    }
}