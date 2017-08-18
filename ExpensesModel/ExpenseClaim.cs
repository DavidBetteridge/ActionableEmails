using System;
using System.Collections.Generic;

namespace ActionableEmail.ExpensesModel
{
    enum Status
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }


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

        public Status Status { get; set; }
        public string ApprovalComment { get; set; }

        public string UniqueID { get; set; }

    }
}
