using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using signalr.backend.Data;
using signalr.backend.Hubs;
using signalr.backend.Models;

namespace signalr.backend.Services
{
    public class ChannelService : BackgroundService
    {
        public const int DELAY = 30 * 1000;

        private IServiceScopeFactory _serviceScopeFactory;
        private IHubContext<ChatHub> _chatHub;
        public ChannelService(IServiceScopeFactory serviceScopeFactory, IHubContext<ChatHub> gameHub)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _chatHub = gameHub;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(DELAY, stoppingToken);
                Channel channel = await FindMostPopularChannel();
                string groupName = CreateChannelGroupName(channel.Id);
                await _chatHub.Clients.Group(groupName).SendAsync("MostPopular", "Vous êtes dans le canal le plus populaire avec " + channel.NbMessages + " messages");
            }
        }

        private static string CreateChannelGroupName(int channelId)
        {
            return "Channel" + channelId;
        }

        public async Task<Channel> FindMostPopularChannel()
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                ApplicationDbContext context =
                    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // TODO: Mettre à jour et sauvegarder le nbWinds des joueurs
                Channel channel = await context.Channel.OrderByDescending(c => c.NbMessages).FirstAsync();

                return channel;
            }
        }
    }
}
