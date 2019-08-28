using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Test_store.Models.Data
{
    public class Db :DbContext
    {
        public Db() : base("StoreDB")
        { }
        public DbSet<PagesDTO> Pages { get; set; }

        public DbSet<CategoryDTO> Categories { get; set; }

        public DbSet<ProductDTO> Products { get; set; }
    }
}