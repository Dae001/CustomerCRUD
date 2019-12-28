using System;

namespace CustomerCRUD.Models
{
    public class OrderDetail
    {
        public int OrderID { get; set; } // int, not null
        public int ProductID { get; set; } // int, not null
        public decimal UnitPrice { get; set; } // money, not null
        public short Quantity { get; set; } // smallint, not null
        public Single Discount { get; set; } // real, not null

        // public Order Order { get; set; }
        // public Products Product { get; set; }
    }
}
