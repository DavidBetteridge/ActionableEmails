using MessageCard;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace ActionableEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var oh = new OrderHeader()
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
            oh.Lines.Add(line1);

            var line2 = new Line()
            {
                DeliveryDate = new System.DateTime(2017, 6, 1),
                Description = "Buttered toast",
                Price = 75.5M,
                Quantity = 1,
                Value = 75.5M
            };
            line2.Nominals.Add(new Nominal() { Code = "A.B.C", Value = 75.5M });
            oh.Lines.Add(line2);

            var c = new CreateOrderApprovalEmail();
            c.CreateEmail(oh);
            return;



            //var card = new Card();
            //card.ThemeColor = "0075FF";
            //card.HideOriginalBody = true;
            //card.Title = "Expense claim is pending your approval";

            //var section = new Section();
            //card.Sections = new List<Section>() { section };

            //section.ActivityImage = "https://acceleratedpayments.proactisp2p.com:8000/outlook/david.jpg";
            //section.ActivityTitle = "Claimant is **David Betteridge**";
            //section.ActivitySubtitle = "david.betteridge@proactis.com";

            //section.Text = "Please review the expense claim below.";
            //section.Facts = new List<Fact>();

            //section.Facts.Add(new Fact() { Name = "ID", Value = "EXP1234" });
            //section.Facts.Add(new Fact() { Name = "Amount", Value = "64.40 GBP" });
            //section.Facts.Add(new Fact() { Name = "Submitter", Value = "David Betteridge" });
            //section.Facts.Add(new Fact() { Name = "Description", Value = "Travel to P&O" });

            //section.Actions = new List<MessageCard.Action>();

            //var approvalActionCard = new ActionCard();
            //section.Actions.Add(approvalActionCard);
            //approvalActionCard.Name = "Approve";
            //approvalActionCard.Actions = new List<ExternalAction>()
            //{
            //    new HttpPOST() {
            //        Name = "Approve",
            //        Target = "https://acceleratedpayments.proactisp2p.com:8000/outlook/expense/approve",
            //        Body = "{'database': 'LIVE',  'id': 'EXP1234' }",
            //        BodyContentType = "application/json"
            //    }
            //};

            //var rejectionActionCard = new ActionCard();
            //section.Actions.Add(rejectionActionCard);
            //rejectionActionCard.Name = "Reject";

            //rejectionActionCard.Inputs = new List<Input>()
            //{
            //    new TextInput()
            //    {
            //        Id="comment",
            //        IsMultiline=true,
            //        IsRequired=true,
            //        Title="Explain why the expense claim is rejected",
            //        Value=""
            //    }
            //};

            //rejectionActionCard.Actions = new List<ExternalAction>()
            //{
            //    new HttpPOST() {
            //        Name = "Reject",
            //        Target = "https://acceleratedpayments.proactisp2p.com:8000/outlook/expense/reject",
            //        Body = "{'database': 'LIVE',  'id': 'EXP1234', 'comment': '{{comment.value}}' }",
            //        BodyContentType = "application/json"
            //    }
            //};

            //var viewClaim = new OpenUri()
            //{
            //    Name = "View Expense",
            //    Targets = new List<OpenUriTarget>()
            //     {
            //         new OpenUriTarget()
            //         {
            //              OS="default",
            //              Uri="http://www.proactis.com"
            //         }
            //     }
            //};
            //section.Actions.Add(viewClaim);


            //var body = LoadDocumentMessageBody(card);

            //var subject = "Please Approve my claim";

            //var mail = new MailMessage("david.betteridge@proactis.com", "david.betteridge@proactis.com");
            //SmtpClient client = new SmtpClient();
            //client.Port = 25;
            //client.Host = "mailcore.as13009.net";

            //mail.Subject = subject;
            //mail.IsBodyHtml = true;
            //mail.Body = body;
            //client.Send(mail);

        }


//        private static string LoadDocumentMessageBody(Card document)
//        {
//            return
//                "<html>" +
//                "  <head>" +
//                "    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">" +
//                "    <script type=\"application/ld+json\">" +
//                document.ToJson() +
//                "    </script>" +
//                "  <head>" +
//                "  <body>" +
//                "    Your email client cannot display this email - try using https://outlook.office.com/owa/" +
//                "  </body>" +
//                "</html>";
//        }

    }
}


