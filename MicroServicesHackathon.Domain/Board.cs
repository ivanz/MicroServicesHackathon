using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroServicesHackathon.Domain
{
    public class Board
    {
        private List<Movement> _movements;

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

        public bool MakeMove(Movement movement)
        {
            throw new NotImplementedException();
        }
    }
}