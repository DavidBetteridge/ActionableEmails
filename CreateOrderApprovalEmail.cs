using MessageCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ActionableEmail
{
    class CreateOrderApprovalEmail
    {
        public void CreateEmail(OrderHeader order)
        {
            var card = new Card();
            card.ThemeColor = "0099CC";
            card.HideOriginalBody = true;
            card.Title = "Purchase Order is pending your approval";

            var headerSection = new Section();
            card.Sections = new List<Section>() { headerSection };

            headerSection.ActivityImage = "https://acceleratedpayments.proactisp2p.com:8000/outlook/david.jpg";
            headerSection.ActivityTitle = $"Orginator is {order.Orginator}";
            headerSection.ActivitySubtitle = order.OrginatorEmailAddress;

            if (order.ExceedsBudget)
            {
                headerSection.ActivityText = "**This order will exceed the budget by £" + order.OverspentAmount.ToString("0.00") + "**";
            }

            headerSection.Text = $"Purchase order {order.OrderNumber} requires your authorisation. ";
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
                    Title="Explain why the purchase order is rejected",
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
                Name = "View Purchase Order",
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


            var body = LoadDocumentMessageBody(card);

            var subject = "Authorise Order : " + order.OrderNumber;

            var e = "davidabetteridge";
            var mail = new MailMessage(e, e);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.Host = "mailcore.as13009.net";

            //client.Host = "smtp.gmail.com";
            //client.Port = 587;
            //client.EnableSsl = true;
            //client.Timeout = 10000;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("davidabetteridge@gmail.com", "");


            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            client.Send(mail);


        }

        private static string LoadDocumentMessageBody(Card document)
        {
            return
                "<html>" +
                "  <head>" +
                "    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">" +
                "    <script type=\"application/ld+json\">" +
                document.ToJson() +
                "    </script>" +
                "  <head>" +
                "  <body>" +
                "    Your email client cannot display this email - try using https://outlook.office.com/owa/" +
                "  </body>" +
                "</html>";
        }

    }
}
