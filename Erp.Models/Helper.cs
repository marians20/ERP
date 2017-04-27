using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace Erp.Models
{
    public class Helper
    {
        public static List<PropertyInfo> GetProperties<T>()
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            return properties.ToList();
        }
    }
}
