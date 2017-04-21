using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Erp.Models.People
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName {get; set;}
        public string LastName { get; set; }
        public string SocialId { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }
    }
}
