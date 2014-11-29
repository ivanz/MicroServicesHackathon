using System.Collections.Generic;
using MicroServicesHackathon.Facts;

namespace MicroServicesHackathon
{
    public interface IHRepository
    {
        void Save(AcceptedMovement movement);
        IEnumerable<AcceptedMovement> GetGame(string gameId);
    }
}