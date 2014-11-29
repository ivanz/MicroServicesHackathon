using System.Configuration;
using MicroServicesHackathon.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicesHackathon.Repository;
using MicroServicesHackathon.Rest;
using StackExchange.Redis;

namespace MicroServicesHackathon
{
    class Program
    {
        static void Main(string[] args)
        {


            new RestClient().PostFact("chat", new ChatFact
            {
                 says = "banana",
                 who = "bomb"
            });

            var redisHost = ConfigurationManager.AppSettings["redishost"]; // "127.0.0.1";
            var redisPort = Convert.ToInt32(ConfigurationManager.AppSettings["redisport"]);

            var connectionMultiplexer = ConnectionMultiplexer.Connect(
                new ConfigurationOptions
                {
                    EndPoints =
                    {
                        string.Format("{0}:{1}", redisHost, redisPort)
                    },
                    ConnectTimeout = 10000,
                    AbortOnConnectFail = false
                });


            var hRepository = new HRepository(new RedisRepository(connectionMultiplexer));

            //IHRepository repository = new InMemoryRepository();

            IRestClient restClient = new RestClient();
            //Referee referee = new Referee(restClient, repository);
            var referee = new Referee(restClient, hRepository);
            var task = referee.Start();
            task.Wait();
        }
    }

    public class InMemoryRepository : IHRepository
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
