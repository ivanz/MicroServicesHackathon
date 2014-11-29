using Newtonsoft.Json;
using StackExchange.Redis;

namespace MicroServicesHackathon
{
    public class RedisRepository : IRepository
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        public RedisRepository(ConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public void Set<T>(string key, T message) 
        {
            var db = _connectionMultiplexer.GetDatabase();
            // we use JSON 
            var serialized = JsonConvert.SerializeObject(message);
            db.StringSet(key, serialized);
        }

        public T Get<T>(string key)
            where T: class
        {
            var db = _connectionMultiplexer.GetDatabase();
            string serialized = db.StringGet(key);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

    }
}