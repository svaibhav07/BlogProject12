using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogProject12.Models
{
    public class TagModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TagName { get; set; }

        [Required]
        public string RelatedBlogs { get; set; }
    }
}
