using ForcedDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForcedDemo.Models.ViewModels.Shop
{
    public class OrderVM
    {
        public OrderVM()
        {
        }

        public OrderVM(OrderDTO row)
        {
            OrderId = row.OrderId;
            UserId = row.ApplicationUserId;
            CreatedAt = row.CreatedAt;
        }

        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}