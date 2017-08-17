using ActionableEmail.ExpensesModel;
using MessageCard;
using System.Collections.Generic;

namespace ActionableEmail
{
    class BuildExpensesApprovalCard
    {
        public Card CreateCard(ExpenseClaim expenseClaim)
        {

            var card = new Card();
            card.ThemeColor = "0099CC";
            card.HideOriginalBody = true;
            card.Title = "Expense Claim is pending your approval";

            var headerSection = new Section();
            card.Sections = new List<Section>() { headerSection };

            headerSection.ActivityImage = "https://acceleratedpayments.proactisp2p.com:8000/outlook/david.jpg";
            headerSection.ActivityTitle = $"Claimant is {expenseClaim.Claimant}";
            headerSection.ActivitySubtitle = expenseClaim.ClaimantEmailAddress;
            headerSection.ActivityText = expenseClaim.Title;


            headerSection.Text = $"Expense Claim {expenseClaim.ClaimNumber} requires your authorisation. ";
            headerSection.Facts = new List<Fact>();

            headerSection.Facts.Add(new Fact() { Name = "Claim No", Value = expenseClaim.ClaimNumber });
            headerSection.Facts.Add(new Fact() { Name = "Raised By", Value = expenseClaim.RaisedBy });
            headerSection.Facts.Add(new Fact() { Name = "Claim Date", Value = expenseClaim.ClaimDate.ToString("dd-MM-yyyy") });

            headerSection.Facts.Add(new Fact() { Name = "Net Value", Value = "£" + expenseClaim.NetValue.ToString("0.00") });


            headerSection.Actions = new List<MessageCard.Action>();

            var approvalActionCard = new ActionCard();
            headerSection.Actions.Add(approvalActionCard);
            approvalActionCard.Name = "Approve";
            approvalActionCard.Actions = new List<ExternalAction>()
            {
                new HttpPOST() {
                    Name = "Approve",
                    Target = "https://acceleratedpayments.proactisp2p.com:8000/outlook/expense/approve",
                    Body = "{'database': 'LIVE',  'id': 'EXP1234' }",
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
                    Title="Explain why the expense claim is rejected",
                    Value=""
                }
            };

            rejectionActionCard.Actions = new List<ExternalAction>()
            {
                new HttpPOST() {
                    Name = "Reject",
                    Target = "https://acceleratedpayments.proactisp2p.com:8000/outlook/expense/reject",
                    Body = "{'database': 'LIVE',  'id': 'EXP1234', 'comment': '{{comment.value}}' }",
                    BodyContentType = "application/json"
                }
            };

            var viewClaim = new OpenUri()
            {
                Name = "View Expense",
                Targets = new List<OpenUriTarget>()
                 {
                     new OpenUriTarget()
                     {
                          OS="default",
                          Uri="http://www.proactis.com"
                     }
                 }
            };
            headerSection.Actions.Add(viewClaim);


            var lineNumber = 1;
            foreach (var line in expenseClaim.Lines)
            {
                var linesSection = new Section();
                card.Sections.Add(linesSection);
                linesSection.StartGroup = true;
                linesSection.Facts = new List<Fact>();
                linesSection.Title = $"Line {lineNumber} - {line.Description}";
                linesSection.Facts.Add(new Fact() { Name = "Value", Value = "£" + line.Value.ToString("0.00") });

                var nominalNumber = 1;
                foreach (var nominal in line.Nominals)
                {
                    linesSection.Facts.Add(new Fact() { Name = "Nominal " + nominalNumber, Value = $"{nominal.Code} (£{nominal.Value.ToString("0.00")})" });
                    nominalNumber++;
                }
                lineNumber++;
            }


            return card;

        }



    }
}
