using System;
using System.Collections.Generic;

namespace ActionableEmail.ExpensesModel
{
    class ExpenseClaim
    {
        public List<Line> Lines = new List<Line>();

        public string ClaimNumber { get; internal set; }
        public string Claimant { get; internal set; }
        public string ClaimantEmailAddress { get; internal set; }
        public string Title { get; internal set; }
        public string RaisedBy { get; internal set; }
        public DateTime ClaimDate { get; internal set; }
        public decimal NetValue { get; internal set; }
    }
}
