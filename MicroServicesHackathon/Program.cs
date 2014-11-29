using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServicesHackathon.Facts;
using MicroServicesHackathon.Rest;
using StackExchange.Redis;

namespace MicroServicesHackathon
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisHost = "127.0.0.1";
            var redisPort = 6379;
            var connectionMultiplexer = ConnectionMultiplexer.Connect(
                new ConfigurationOptions()
                {
                    EndPoints =
                            {
                                string.Format("{0}:{1}", redisHost, redisPort)
                            },
                    ConnectTimeout = 10000,
                    AbortOnConnectFail = false
                });

            new RestClient().PostFact("chat", new ChatFact() {
                 says = "banana",
                 who = "bomb"
            });

            IRepository repository = new InMemoryRepository();
            IRestClient restClient = new RestClient();
            Referee referee = new Referee(restClient, repository);
            Task task = referee.Start();
            task.Wait();
        }
    }

    public class InMemoryRepository : IRepository
    {
        private readonly IList<AcceptedMovement> _movements;

        public InMemoryRepository()
        {
            _movements = new List<AcceptedMovement>();
        }

        public void Save(AcceptedMovement movement)
        {
            _movements.Add(movement);
        }

        public IEnumerable<AcceptedMovement> GetGame(string gameId)
        {
            return _movements
                .Where(m => string.Equals(m.GameId, gameId, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
