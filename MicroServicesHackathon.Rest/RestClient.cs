using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicesHackathon.Facts;
using RestSharp;

namespace MicroServicesHackathon.Rest
{
    public class RestClient : IRestClient
    {
        private readonly RestSharp.RestClient _client;

        public RestClient()
        {
            _client = new RestSharp.RestClient("http://combo-squirrel.herokuapp.com");
        }

        public string Subscribe(string topic)
        {
            //RestRequest request = new RestRequest("/topics/{topic_name}/subscriptions", Method.POST);
            //request.AddUrlSegment("topic_name", topic);

            throw new NotImplementedException();
        }

        public T NextFact<T>(string topic, string subscription) where T:Fact
        {
            throw new NotImplementedException();
        }

        public void PostFact(string topic, Fact fact)
        {
            RestRequest request = new RestRequest("/topics/{topic_name}/facts", Method.POST);
            request.AddUrlSegment("topic_name", topic);

            request.AddJsonBody(fact);

            _client.Post(request);
        }
    }
}
