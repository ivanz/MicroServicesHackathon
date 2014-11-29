using System.Collections.Generic;
using MicroServicesHackathon.Facts;
using MicroServicesHackathon.Repository;

namespace MicroServicesHackathon
{
    public class HRepository : IHRepository
    {
        private readonly IRepository _repository;

        public HRepository(IRepository repository)
        {
            _repository = repository;
        }

        public void Save(AcceptedMovement movement)
        {
            var moves = _repository.Get<System.Collections.Generic.List<AcceptedMovement>>(movement.GameId);
            if (moves == null)
                moves = new List<AcceptedMovement> {movement};
            else
                moves.Add(movement);

            _repository.Set(movement.GameId, moves);
        }

        public IEnumerable<AcceptedMovement> GetGame(string gameId)
        {
            return _repository.Get<List<AcceptedMovement>>(gameId);
        }
    }
}