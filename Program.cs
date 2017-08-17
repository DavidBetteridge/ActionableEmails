using ActionableEmail.ExpensesModel;
using ActionableEmail.OrdersModel;
using MessageCard;
using System.Net.Mail;

namespace ActionableEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            //var exampleOrderBuilder = new ExampleOrderBuilder();
            //var order = exampleOrderBuilder.GetExampleOrder();

            //var cardBuilder = new BuildOrderApprovalCard();
            //var card = cardBuilder.CreateCard(order);
            //var subject = "Authorise Order : " + order.OrderNumber;


            var exampleClaimBuilder = new ExampleClaimBuilder();
            var claim = exampleClaimBuilder.GetExampleClaim();

            var cardBuilder = new BuildExpensesApprovalCard();
            var card = cardBuilder.CreateCard(claim);
            var subject = "Authorise Expense Claim : " + claim.ClaimNumber;

            var body = LoadDocumentMessageBody(card);

            var emailAddress = "david.betteridge@proactis.com";
            var mail = new MailMessage(emailAddress, emailAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };
            var client = new SmtpClient()
            {
                Port = 25,
                Host = "mailcore.as13009.net"
            };
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


