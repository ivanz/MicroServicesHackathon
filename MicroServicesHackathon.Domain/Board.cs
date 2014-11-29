using System;
using System.Collections.Generic;

namespace MicroServicesHackathon.Domain
{
    using System.Linq;

    public class Board
    {
        private readonly List<Movement> _movements;

        public Board() : this(new List<Movement>())
        {
        }

        public Board(List<Movement> movements)
        {
            if (movements == null)
                throw new ArgumentNullException("movements", "movements is null.");

            Size = 3;
            _movements = movements;
        }

        public string GameId { get; set; }

        public ICollection<Movement> Movements {
            get { return _movements; }
        }

        public int Size { get; private set; }

        public bool IsValid(Movement movement)
        {
            if (movement.X > Size || movement.Y > Size)
                return false;

            return !Movements.Any(m => m.X == movement.X && m.Y == movement.Y);
        }
    }
}