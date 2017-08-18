using MessageCard;
using System.Collections.Generic;

namespace ActionableEmail.OrdersModel
{
    class BuildOrderApprovalCard
    {
        public Card CreateCard(OrderHeader order, Connection connection)
        {
            var card = new Card();
            card.ThemeColor = "0099CC";
            card.HideOriginalBody = true;

            switch (order.Status)
            {
                case Status.Pending:
                    card.Title = "Purchase Order is pending your approval";
                    break;
                case Status.Approved:
                    card.Title = "Purchase Order has been approved";
                    break;
                case Status.Rejected:
                    card.Title = "Purchase Order has been rejected - " + order.ApprovalComment;
                    break;
                default:
                    break;
            }

            var headerSection = new Section();
            card.Sections = new List<Section>() { headerSection };

            headerSection.ActivityImage = connection.SiteURL + "/david.jpg";
            headerSection.ActivityTitle = $"Orginator is {order.Orginator}";
            headerSection.ActivitySubtitle = order.OrginatorEmailAddress;

            if (order.ExceedsBudget)
            {
                headerSection.ActivityText = "**This order will exceed the budget by £" + order.OverspentAmount.ToString("0.00") + "**";
            }

            switch (order.Status)
            {
                case Status.Pending:
                    headerSection.Text = $"Purchase order {order.OrderNumber} requires your authorisation. ";
                    break;
                case Status.Approved:
                    headerSection.Text = $"Purchase order {order.OrderNumber} has been approved.";
                    break;
                case Status.Rejected:
                    headerSection.Text = $"Purchase order {order.OrderNumber} has been rejected.";
                    break;
                default:
                    break;
            }

            headerSection.Facts = new List<Fact>();

            headerSection.Facts.Add(new Fact() { Name = "Order No", Value = order.OrderNumber });
            headerSection.Facts.Add(new Fact() { Name = "Order Date", Value = order.OrderDate.ToString("dd-MM-yyyy") });

            headerSection.Facts.Add(new Fact() { Name = "Net Value", Value = "£" + order.NetValue.ToString("0.00") });
            headerSection.Facts.Add(new Fact() { Name = "Tax Value", Value = "£" + order.TaxValue.ToString("0.00") });
            headerSection.Facts.Add(new Fact() { Name = "Total Value", Value = "£" + order.GrossValue.ToString("0.00") });

            headerSection.Facts.Add(new Fact() { Name = "Supplier", Value = order.SupplierTitle });
            headerSection.Facts.Add(new Fact() { Name = "Supplier's Address", Value = order.SupplierAddress });
            headerSection.Facts.Add(new Fact() { Name = "Delivery Address", Value = order.DeliveryAddress });


            var lineNumber = 1;
            foreach (var line in order.Lines)
            {
                var linesSection = new Section();
                card.Sections.Add(linesSection);
                linesSection.StartGroup = true;
                linesSection.Facts = new List<Fact>();
                linesSection.Title = $"Line {lineNumber} - {line.Description}";
                linesSection.Facts.Add(new Fact() { Name = "Quantity", Value = line.Quantity.ToString() });
                linesSection.Facts.Add(new Fact() { Name = "Unit Price", Value = "£" + line.Price.ToString("0.00") });
                linesSection.Facts.Add(new Fact() { Name = "Value", Value = "£" + line.Value.ToString("0.00") });
                linesSection.Facts.Add(new Fact() { Name = "Delivery Date", Value = line.DeliveryDate.ToString("dd-MM-yyyy") });

                var nominalNumber = 1;
                foreach (var nominal in line.Nominals)
                {
                    linesSection.Facts.Add(new Fact() { Name = "Nominal " + nominalNumber, Value = $"{nominal.Code} (£{nominal.Value.ToString("0.00")})" });
                    nominalNumber++;
                }
                lineNumber++;
            }


            headerSection.Actions = new List<MessageCard.Action>();

            if (order.Status == Status.Pending)
            {
                var approvalActionCard = new ActionCard();
                headerSection.Actions.Add(approvalActionCard);
                approvalActionCard.Name = "Approve";
                approvalActionCard.Actions = new List<ExternalAction>()
            {
                new HttpPOST() {
                    Name = "Approve",
                    Target = connection.SiteURL + "/order/approve",
                    Body = "{'database': '" + connection.DatabaseTitle + "',  'id': '" + order.UniqueID + "' }",
                    BodyContentType = "application/json"
                }
            };

                var rejectionActionCard = new ActionCard();
                headerSection.Actions.Add(rejectionActionCard);
                rejectionActionCard.Name = "Reject";

                rejectionActionCard.Inputs = new List<Input>()
            {
                new TextInput()
                {
                    Id="comment",
                    IsMultiline=true,
                    IsRequired=true,
                    Title="Explain why the purchase order is rejected",
                    Value=""
                }
            };

                rejectionActionCard.Actions = new List<ExternalAction>()
                {
                    new HttpPOST() {
                        Name = "Reject",
                        Target = connection.SiteURL + "/order/reject",
                        Body = "{'database': '" + connection.DatabaseTitle + "',  'id': '" + order.UniqueID + "', 'comment': '{{comment.value}}' }",
                        BodyContentType = "application/json"
                    }
                };
            }

            var viewClaim = new OpenUri()
            {
                Name = "View Purchase Order",
                Targets = new List<OpenUriTarget>()
                 {
                     new OpenUriTarget()
                     {
                          OS="default",
                          Uri=connection.SiteURL
                     }
                 }
            };
            headerSection.Actions.Add(viewClaim);
            return card;
        }
    }
}
