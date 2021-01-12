using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogProject12.Models
{
    public class BlogModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BlogTitle { get; set; }

        [Required]
        public string BlogRaw { get; set; }





    }
}
