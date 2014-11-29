using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroServicesHackathon
{
    using MicroServicesHackathon.Domain;
    using MicroServicesHackathon.Facts;
    using MicroServicesHackathon.Rest;

    public class Referee
    {
        private readonly IRestClient _restClient;
        private readonly string _acceptedMovementSubscribeId;
        private readonly IRepository _acceptedMovementRepository;

        public Referee(IRestClient restClient, IRepository acceptedMovementRepository)
        {
            _restClient = restClient;
            _acceptedMovementRepository = acceptedMovementRepository;
            _acceptedMovementSubscribeId = _restClient.Subscribe(ProposedMovement.Topic);
        }

        public void ProcessMovements()
        {
            while (true) {
                ProposedMovement fact = _restClient.NextFact<ProposedMovement>(
                    ProposedMovement.Topic,
                    _acceptedMovementSubscribeId);

                Movement movement = Convert(fact);
                IList<Movement> previousMovements = 
                    _acceptedMovementRepository.GetGame(fact.GameId)
                    .Select(Convert)
                    .ToList();
                Board board = new Board(previousMovements);
                if (board.IsValid(movement)) {
                    AcceptedMovement acceptedMovement = Accept(fact);
                    _restClient.PostFact(acceptedMovement.TopicName, acceptedMovement);
                    _acceptedMovementRepository.Save(fact);

                } else {
                    InvalidMovement invalidMovement = Reject(fact);
                    _restClient.PostFact(invalidMovement.TopicName, invalidMovement);
                }
            }
        }

        private static Movement Convert(MovementFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("fact");
            if (fact.Position == null)
                throw new ArgumentException("The movement fact doesn't have a position");

            return new Movement(fact.Position.X, fact.Position.Y, fact.PlayerId);
        }

        private static AcceptedMovement Accept(ProposedMovement fact)
        {
            return new AcceptedMovement {
                GameId = fact.GameId,
                PlayerId = fact.PlayerId,
                Position = fact.Position
            };
        }

        private static InvalidMovement Reject(ProposedMovement fact)
        {
            return new InvalidMovement {
                GameId = fact.GameId,
                PlayerId = fact.PlayerId,
                Position = fact.Position
            };
        }
    }

    public interface IRepository
    {
        void Save(MovementFact movement);
        IEnumerable<AcceptedMovement> GetGame(string gameId);
    }
}
