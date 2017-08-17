using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionableEmail
{
    class OrderHeader
    {
        public bool ExceedsBudget { get; set; }
        public decimal OverspentAmount { get; set; }

        public string OrderNumber { get; set; }

        public string Orginator { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal NetValue { get; set; }
        public decimal TaxValue { get; set; }
        public decimal GrossValue { get; set; }

        public string SupplierTitle { get; set; }
        public string SupplierAddress { get; set; }

        public string DeliveryAddress { get; set; }
        public string OrginatorEmailAddress { get; set; }

        public List<Line> Lines = new List<Line>();
    }
}
