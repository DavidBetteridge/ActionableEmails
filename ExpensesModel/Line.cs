using System;
using System.Collections.Generic;

namespace ActionableEmail.ExpensesModel
{
    class Line
    {
        public string Description { get; set; }
        public decimal Value { get; set; }

        public List<Nominal> Nominals = new List<Nominal>();
    }
}

