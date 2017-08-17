namespace ActionableEmail.OrdersModel
{
    class ExampleOrderBuilder
    {
        public OrderHeader GetExampleOrder()
        {
            var order = new OrderHeader()
            {
                DeliveryAddress = "Lower Darnborough Street, York ",
                ExceedsBudget = true,
                GrossValue = 120,
                NetValue = 100,
                TaxValue = 20,
                OrderDate = new System.DateTime(2017, 5, 31),
                OrderNumber = "STANDARD766",
                Orginator = "David Betteridge",
                OrginatorEmailAddress = "david.betteridge@proactis.com",
                OverspentAmount = 20000,
                SupplierAddress = "Milestone Gardens, York, TR25 6PL",
                SupplierTitle = "Angel Face "
            };

            var line1 = new Line()
            {
                DeliveryDate = new System.DateTime(2017, 6, 1),
                Description = "Some really nice eggs",
                Price = 2.45M,
                Quantity = 10,
                Value = 24.5M
            };
            line1.Nominals.Add(new Nominal() { Code = "A.B.C", Value = 20M });
            line1.Nominals.Add(new Nominal() { Code = "D.E.F", Value = 4.5M });
            order.Lines.Add(line1);

            var line2 = new Line()
            {
                DeliveryDate = new System.DateTime(2017, 6, 1),
                Description = "Buttered toast",
                Price = 75.5M,
                Quantity = 1,
                Value = 75.5M
            };
            line2.Nominals.Add(new Nominal() { Code = "A.B.C", Value = 75.5M });
            order.Lines.Add(line2);

            return order;
        }
    }
}
