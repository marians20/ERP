using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Erp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sakura.AspNetCore;

namespace Erp.Controllers
{
    public abstract class MyBaseController : Controller
    {
        protected readonly ApplicationDbContext _context;

        public MyBaseController(ApplicationDbContext context)
        {
            _context = context;
        }

        protected void AddGendersSelectList(object selectedValue = null)
        {
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Name", selectedValue);
        }

        protected IPagedList Paginated<T>(IQueryable<T> model, int? page, int? pageSize) where T : class
        {
            if (page == null) { page = 1; }
            if (pageSize == null) { pageSize = 3; }
            return model.ToPagedList(pageSize.Value, page.Value);
        }

        protected void AddFieldNames<T>()
        {
            ViewData["FieldNames"] = Models.Helper.GetProperties<T>()
                .Where(x => !x.PropertyType.IsArray)
                .Select(x => x.Name)
                .ToList();
        }

        protected void AddSortParameters<T>(string sortOrder)
        {
            var fields = Models.Helper.GetProperties<T>()
                .Where(x => !x.PropertyType.IsArray)
                .Select(x => x.Name.Replace("_", ""));
            var sortParameter = new SortParameter(sortOrder);
            foreach (var field in fields)
            {
                var key = $"{field}SortParam";
                var value = $"{field.ToLower()}";

                if (sortParameter.Field == value)
                {
                    value = sortParameter.ToString();
                }

                ViewData[key] = value;
            }
        }
    }

    public class SortParameter
    {
        public string Field { get; set; } = string.Empty;
        public SortDirections Direction { get; set; } = SortDirections.None;

        public SortParameter(string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
            {
                var sortParams = sortOrder.Split("_".ToCharArray());
                Field = sortParams[0];
                Direction = sortParams.Length == 1
                    ? SortDirections.None
                    : sortParams[1] == "desc"
                        ? SortDirections.Desc
                        : SortDirections.Asc;
            }
        }

        public override string ToString()
        {
            switch (Direction)
            {
                case SortDirections.Asc:
                    return $"{Field}_asc";
                case SortDirections.Desc:
                    return $"{Field}_desc";
                default:
                    return Field;
            }
        }
    }

    public enum SortDirections { None, Asc, Desc};
}