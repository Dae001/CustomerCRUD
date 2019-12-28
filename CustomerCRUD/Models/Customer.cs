using System.Collections.Generic;

namespace CustomerCRUD.Models
{
    public class Customer
    {
        //public Customer()
        //{
        //   // this.CustomerCustomerDemoes = new List<CustomerCustomerDemo>();
        //    this.Orders = new List<Order>();
        //}
        public string CustomerID { get; set; } // nchar(5), not null
        public string CompanyName { get; set; } // nvarchar(40), not null
        public string ContactName { get; set; } // nvarchar(30), null
        public string ContactTitle { get; set; } // nvarchar(30), null
        public string Address { get; set; } // nvarchar(60), null
        public string City { get; set; } // nvarchar(15), null
        public string Region { get; set; } // nvarchar(15), null
        public string PostalCode { get; set; } // nvarchar(10), null
        public string Country { get; set; } // nvarchar(15), null
        public string Phone { get; set; } // nvarchar(24), null
        public string Fax { get; set; } // nvarchar(24), null
     
        public IEnumerable<Order> Orders { get; set; }
        // public IEnumerable<CustomerCustomerDemo> CustomerCustomerDemoes { get; set; }

    }
}
