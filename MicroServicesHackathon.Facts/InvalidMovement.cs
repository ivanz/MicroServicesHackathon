namespace MicroServicesHackathon.Facts
{
    public class InvalidMovement : MovementFact
    {
        public override string TopicName { get { return "game.invalid_move"; } }
    }
}