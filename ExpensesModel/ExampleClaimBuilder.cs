namespace ActionableEmail.ExpensesModel
{
    class ExampleClaimBuilder
    {
        public ExpenseClaim GetExampleClaim()
        {
            var claim = new ExpenseClaim()
            {
                NetValue = 100,
                ClaimDate = new System.DateTime(2017, 5, 31),
                ClaimNumber = "STANDARD766",
                Claimant = "David Betteridge",
                ClaimantEmailAddress = "david.betteridge@proactis.com",
                RaisedBy = "Rebecca Betteridge",
                Title = "Visit to P&O by train"

            };

            var line1 = new Line()
            {
                Description = "Some really nice eggs",
                Value = 24.5M
            };
            line1.Nominals.Add(new Nominal() { Code = "A.B.C", Value = 20M });
            line1.Nominals.Add(new Nominal() { Code = "D.E.F", Value = 4.5M });
            claim.Lines.Add(line1);

            var line2 = new Line()
            {
                Description = "Buttered toast",
                Value = 75.5M
            };
            line2.Nominals.Add(new Nominal() { Code = "A.B.C", Value = 75.5M });
            claim.Lines.Add(line2);

            return claim;
        }
    }
}
