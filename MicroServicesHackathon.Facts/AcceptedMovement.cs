namespace MicroServicesHackathon.Facts
{
    public class AcceptedMovement : MovementFact
    {
        public const string Topic = "game.accepted_move";

        public override string TopicName { get { return Topic; } }
    }
}