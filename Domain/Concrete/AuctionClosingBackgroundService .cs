using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Contracts; // Import the namespace where IAuctionDomain is located

public class AuctionClosingBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AuctionClosingBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CloseEndedAuctions();
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Adjust the frequency as needed
        }
    }

    private async Task CloseEndedAuctions()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var auctionDomain = scope.ServiceProvider.GetRequiredService<IAuctionDomain>();

            var auctionsToClose = auctionDomain.GetAuctionsToClose(); // You need to implement this method

            foreach (var auctionId in auctionsToClose)
            {
                try
                {
                    auctionDomain.CloseAuction(auctionId.AuctionId);
                }
                catch (Exception ex)
                {
                    // Log exception or handle it as needed
                }
            }
        }
    }
}
