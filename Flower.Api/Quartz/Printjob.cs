using System;
using System.Linq;
using System.Threading.Tasks;
using Flower.Data;
using Flower.Service.Dtos.RoseDtos;
using Flower.Service.Implementations;
using Quartz;

namespace Flower.Api.Quartz
{
    public class PrintJob : IJob
    {
        private readonly EmailService _emailService;
        private readonly AppDbContext _context;

        public PrintJob(EmailService emailService, AppDbContext appDbContext)
        {
            _emailService = emailService;
            _context = appDbContext;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var now = DateTime.Now;
                var expirationDate = now.AddDays(3);

                var flowers = _context.Roses
                    .Where(f => f.DiscountExpireDate <= expirationDate && f.DiscountExpireDate > now)
                    .ToList();

                if (!flowers.Any())
                    return;

                var random = new Random();
                var randomFlower = flowers[random.Next(flowers.Count)];

                var subscribers = _context.Subscribers.ToList();

                var flowerLink = "https://fiorello.qodeinteractive.com/product/scarlet-sage/";

                var emailTemplate = @"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }
                            .container { max-width: 600px; margin: 20px auto; background-color: #ffffff; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
                            .header { background-color: #70c1b3; padding: 10px; border-radius: 5px 5px 0 0; }
                            .header h1 { margin: 0; color: #ffffff; }
                            .content { margin: 20px 0; }
                            .footer { text-align: center; color: #888888; font-size: 12px; margin-top: 20px; }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h1>Günün Floweri</h1>
                            </div>
                            <div class='content'>
                                <p>Salam,</p>
                                <p>Günün Floweri: <strong>{randomFlower.Name}</strong></p>
                                <p>Bu floweri görmek üçün <a href='{flowerLink}'>buraya clickleyin</a>.</p>
                            </div>
                            <div class='footer'>
                                <p>&copy; {DateTime.Now.Year} Flower.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

                foreach (var subscriber in subscribers)
                {
                    var emailBody = emailTemplate.Replace("{randomFlower.Name}", randomFlower.Name)
                                                 .Replace("{flowerLink}", flowerLink);
                    _emailService.Send(subscriber.Email, "Günün Floweri", emailBody);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in PrintJob: {ex.Message}");
                
            }
        }
    }
}
