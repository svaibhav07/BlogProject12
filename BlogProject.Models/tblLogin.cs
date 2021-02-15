using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject12.Models
{
   
    public class Login
    {
        [Display(Name = "inUserId")]
        [Key]

        public int UserId { get; set; }

        [Display(Name = "stName")]
        public string UserName { get; set; }

    }
}
