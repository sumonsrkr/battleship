using battleship.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace battleship.services
{
    public class BoardService
    {
        private Board _board { get; set; }

        private int _totalHitPositions;

        private int _totalHitCount;

        public bool AllShipsSunk
        {
            get
            {
                return _totalHitPositions > 0 && _totalHitPositions == _totalHitCount;
            }
        }

        public BoardService(Board board)
        {
            _board = board;
        }

        public void SetupBoard()
        {
            _board.Setup();
            _totalHitPositions = 0;
            _totalHitCount = 0;
        }

        public bool AddShip(Ship ship)
        {
            RecalculateShipPosition(ship);

            return _board.AddShipIfFitInTheBoard(ship, ref _totalHitPositions);
        }

        public bool Attack(Position position)
        {
            return _board.Attack(position, ref _totalHitCount);
        }

        private void RecalculateShipPosition(Ship ship)
        {
            int startX = -1;
            int endX = -1;
            int startY = -1;
            int endY = -1;

            if (ship.StartPosition.X <= ship.EndPosition.X)
            {
                startX = ship.StartPosition.X;
                endX = ship.EndPosition.X;
            }
            else
            {
                startX = ship.EndPosition.X;
                endX = ship.StartPosition.X;
            }

            if (ship.StartPosition.Y <= ship.EndPosition.Y)
            {
                startY = ship.StartPosition.Y;
                endY = ship.EndPosition.Y;
            }
            else
            {
                startY = ship.EndPosition.Y;
                endY = ship.StartPosition.Y;
            }

            ship.StartPosition = new Position(startX, startY);
            ship.EndPosition = new Position(endX, endY);
        }
    }
}
