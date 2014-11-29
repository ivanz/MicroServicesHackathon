using NUnit.Framework;
using StackExchange.Redis;

namespace MicroServicesHackathon.Repository.Tests
{
    [TestFixture]
    public class RedisIntegrationTest
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisIntegrationTest()
        {
            var redisHost = "127.0.0.1";
            var redisPort = 6379;

            _connectionMultiplexer = ConnectionMultiplexer.Connect(
                new ConfigurationOptions()
                {
                    EndPoints =
                    {
                        string.Format("{0}:{1}", redisHost, redisPort)
                    },
                    ConnectTimeout = 10000,
                    AbortOnConnectFail = false
                });
        }

        [Test]
        public void TestSet()
        {
            var redisRepository = new RedisRepository(_connectionMultiplexer);

            redisRepository.Set("a1", new A { X = 1, Y = 2});

            var a = redisRepository.Get<A>("a1");

            Assert.AreEqual(1, a.X);
            Assert.AreEqual(2, a.Y);
        }



    }
}