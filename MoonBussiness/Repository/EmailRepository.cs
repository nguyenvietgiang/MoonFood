using MimeKit;
using MoonBussiness.Interface;
using MailKit.Net.Smtp;

namespace MoonBussiness.Repository
{
    public class EmailRepository : IEmailRepository
    {
        public async Task SendEmailAsync(string email, string content)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("nguyenvietgiang1110@gmail.com"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Password Reset";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
                <html>
                    <head>
                        <title>Bức Thư</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                font-size: 14px;
                                line-height: 1.5;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: 0 auto;
                                padding: 20px;
                                border: 1px solid #ccc;
                                border-radius: 5px;
                            }}
                            .header {{
                                text-align: center;
                                margin-bottom: 20px;
                            }}
                            .logo {{
                                max-width: 200px;
                                max-height: 200px;
                                display: block;
                                margin: 0 auto;
                            }}
                            .content {{
                                margin-bottom: 20px;
                            }}
                            .footer {{
                                text-align: center;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class=""container"">
                            <div class=""header"">
                                <img class=""logo"" src=""https://image.pngaaa.com/503/6090503-middle.png"" alt=""Logo"">
                                <h2>Moon Food Store</h2>
                            </div>
                            <div class=""content"">
                                <p>Hello!</p>
                                <p>This is feedback Email from our store.</p>
                                <p>{content}</p>
                                <p>Thank you for using our service!</p>
                            </div>
                            <div class=""footer"">
                                <p>Best Regast,</p>
                                <p>Nguyễn Việt Giang</p>
                            </div>
                        </div>
                    </body>
                </html>";

            message.Body = bodyBuilder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("nguyenvietgiang1110@gmail.com", "**********");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
