using Erp.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Erp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Genders.Any())
            {
                return;
            }

            var genders = new Gender[]
            {
                new Gender {Name = "Man"},
                new Gender {Name = "Woman"}
            };

            foreach(var gender in genders)
            {
                context.Genders.Add(gender);
            }

            context.SaveChanges();
        }
    }
}
