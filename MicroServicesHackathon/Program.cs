using MicroServicesHackathon.Facts;
using MicroServicesHackathon.Rest;


namespace MicroServicesHackathon
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisHost = "127.0.0.1";
            var redisPort = 6379;
            //var connectionMultiplexer = ConnectionMultiplexer.Connect(
            //    new ConfigurationOptions()
            //    {
            //        EndPoints =
            //                {
            //                    string.Format("{0}:{1}", redisHost, redisPort)
            //                },
            //        ConnectTimeout = 10000,
            //        AbortOnConnectFail = false
            //    });


            new RestClient().PostFact("chat", new ChatFact() {
                 says = "banana",
                 who = "bomb"
            });
        }
    }
}
