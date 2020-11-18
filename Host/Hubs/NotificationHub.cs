namespace Host.Hubs
{
    using Microsoft.AspNet.SignalR;
    using System;
    using System.Threading;

    public class NotificationHub : Hub
    {
        public void ServerTime()
        {
            do
            {
                Clients.All.displayTime($"{DateTime.UtcNow:F}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            } while (true);
        }
    }
}
