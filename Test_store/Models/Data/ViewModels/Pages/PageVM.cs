using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_store.Models.Data.ViewModels.Pages
{
    public class PageVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Title { get; set; }

        public string Slug { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength =3)]
        public string Body { get; set; }

        public int Sorting { get; set; }

        public PageVM()
        {

        }
        public PageVM(PagesDTO row)
        {
            this.Id = row.Id;
            this.Title = row.Title;
            this.Slug = row.Slug;
            this.Body = row.Body;
            this.Sorting = row.Sorting;
        }
    }
}