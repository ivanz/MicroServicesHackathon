using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicesHackathon.Facts;

namespace MicroServicesHackathon.Rest
{
    public interface IRestClient
    {
        string Subscribe(string topic);

        T NextFact<T>(string topic, string subscription) where T : Fact;

        void PostFact(string topic, Fact fact);
    }
}
