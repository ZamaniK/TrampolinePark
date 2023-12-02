using ForcedDemo.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForcedDemo.Entities
{
    [Table("tblOrderDetails")]
    public class OrderDetailsDTO
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderDTO Orders { get; set; }
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("ProductId")]
        public virtual ProductDTO Products { get; set; }
    }
}