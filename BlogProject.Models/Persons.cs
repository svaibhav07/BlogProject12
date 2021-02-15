using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject12.Models
{
    [Table("tblPersons")]
    public class Persons
    {
        [Key]
         public int  PersonID { get; set; }

        [Column("stLastName")]
        public   string LastName { get; set; }
         public string FirstName { get; set; }
          public  string Address { get; set; }
           public  string City { get; set; }

        public string XYZ { get; set; }
}
}
