using System;
using System.Collections.Generic;

namespace ActionableEmail
{
    class Line
    {
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Value { get; set; }

        public DateTime DeliveryDate { get; set; }

        public List<Nominal> Nominals = new List<Nominal>();
    }
}

