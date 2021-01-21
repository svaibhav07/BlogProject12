using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BlogProject.Models;

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

        [Required]
        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public TagModel Tag { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BlogPostDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }






    }
}
