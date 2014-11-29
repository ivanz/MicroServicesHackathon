using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServicesHackathon
{
    using System.Collections;
    using MicroServicesHackathon.Domain;
    using MicroServicesHackathon.Facts;
    using MicroServicesHackathon.Rest;

    public class Referee
    {
        private readonly IRestClient restClient;
        private readonly string _acceptedMovementSubscribeId;
        private readonly IRepository _acceptedMovementRepository;

        public Referee(IRestClient restClient, IRepository acceptedMovementRepository)
        {
            this.restClient = restClient;
            _acceptedMovementRepository = acceptedMovementRepository;
            _acceptedMovementSubscribeId = this.restClient.Subscribe(ProposedMovement.Topic);
        }

        public void ProcessMovements()
        {
            while (true) {
                ProposedMovement fact = restClient.NextFact<ProposedMovement>(
                    ProposedMovement.Topic,
                    _acceptedMovementSubscribeId);

                Movement movement = Convert(fact);
                IList<Movement> previousMovements = 
                    _acceptedMovementRepository.GetGame(fact.GameId)
                    .Select(Convert)
                    .ToList();
                Board board = new Board(previousMovements);
                if (board.IsValid(movement)) {
                    _acceptedMovementRepository.Save(fact);
                    // todo; accept movement fact

                } else {
                    // todo: reject movement fact
                }
            }
        }

        public static Movement Convert(MovementFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");
            if (fact.Position == null)
                throw new ArgumentException("The movement fact doesn't have a position");

            return new Movement(fact.Position.X, fact.Position.Y, fact.PlayerId);
        }
    }

    public interface IRepository
    {
        void Save(MovementFact movement);
        IEnumerable<AcceptedMovement> GetGame(string gameId);
    }
}
