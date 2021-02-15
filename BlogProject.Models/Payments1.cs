using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogProject12.Models
{
    public class Payments1
    {
        [Key]
        public int PaymentId { get; set; }

        public string OrderId { get; set; }

        public string TransactionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        virtual public UserModel User { get; set; }

        [Required]
        public int Ammount { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
