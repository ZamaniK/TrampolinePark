using ForcedDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Models.ViewModels.Shop
{
    public class CategoryVM
    {
        public CategoryVM()
        {
        }

        public CategoryVM(CategoryDTO row)
        {
            Id = row.CategoryID;
            Name = row.Name;
            Slug = row.Slug;
            Sorting = row.Sorting;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}