using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroServicesHackathon.Facts;
using MicroServicesHackathon.Rest;


namespace MicroServicesHackathon
{
    class Program
    {
        static void Main(string[] args)
        {
            new RestClient().PostFact("chat", new ChatFact() {
                 says = "banana",
                 who = "bomb"
            });
        }
    }
}
