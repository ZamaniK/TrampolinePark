using ForcedDemo.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForcedDemo.Entities
{
    [Table("tblOrders")]
    public class OrderDTO
    {
        [Key]
        public int OrderId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }
    }
}