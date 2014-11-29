using System;
using System.Collections.Generic;
using System.Linq;
using MicroServicesHackathon.Facts;

namespace MicroServicesHackathon.Rest
{
    public interface IRestClient
    {
        string Subscribe(string topic);

        Fact NextFact(string subscription);

        void PostFact(string topic, Fact fact);
    }
}
