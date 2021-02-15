using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogProject12.Models
{
   public class PaymentInitiateModel
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public int amount { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        virtual public UserModel User { get; set; }
    }
}
