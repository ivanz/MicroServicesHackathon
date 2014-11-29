namespace MicroServicesHackathon.Facts
{
    public class ProposedMovement : MovementFact
    {
        public const string Topic = "game.proposed_move";
        public override string TopicName { get { return Topic; } }
    }
}