using Microsoft.Extensions.DependencyInjection;
using SeaWar.BLL.Infrastructure.Interfaces;
using SeaWar.UI.Infrastructure.Config.ConfigDI;
using SeaWar.UI.Infrastructure.Config.ConfigMapper;
using SeaWar.UI.Infrastructure.Interfaces;
using System;

namespace SeaWar.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                               .SetDI()
                               .SetMapper()
                               .BuildServiceProvider();
            
            var serviceAddShip = services.GetService<IServiceMessageAddShip>();
            var listShips =  serviceAddShip.MessageAddShips();
            var serviceGame = services.GetService<IServiceGame>();
            serviceGame.DisplayGame(listShips);
            Console.ReadLine();
        }
    }
}
