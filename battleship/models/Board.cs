using battleship.common;
using System;

namespace battleship.models
{
    public class Board
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int[,] PlayingBoard;

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public Board():this(10,10)
        { }

        public void Setup()
        {
            if (IsValid())
            {
                PlayingBoard = new int[Height, Width];
            }
            else
            {
                throw new Exception(Constants.Board_Size_Invalid);
            }
        }
        public bool AddShipIfFitInTheBoard(Ship ship, ref int totalHitPositions)
        {
            if (!CanShipFitInTheBoard(ship))
            {
                return false;
            }

            for (int x = ship.StartPosition.X; x <= ship.EndPosition.X; x++)
            {
                for (int y = ship.StartPosition.Y; y <= ship.EndPosition.Y; y++)
                {
                    PlayingBoard[x, y] = PositionStatus.OccupiedByShip;
                    totalHitPositions++;
                }
            }

            return true;
        }


        public bool Attack(Position position, ref int totalHitCount)
        {
            try
            {
                if (PlayingBoard[position.X, position.Y] != PositionStatus.Empty)
                {
                    if (PlayingBoard[position.X, position.Y] == PositionStatus.OccupiedByShip)
                    {
                        PlayingBoard[position.X, position.Y] = PositionStatus.GotHit;
                        totalHitCount++;
                    }
                    return true;
                }
            }
            catch (IndexOutOfRangeException)
            {
            }

            return false;
        }


        #region private methods

        private bool IsValid()
        {
            return Height > 0 && Width > 0;
        }

        private bool CanShipFitInTheBoard(Ship ship)
        {
            try
            {
                for (int x = ship.StartPosition.X; x <= ship.EndPosition.X; x++)
                {
                    for (int y = ship.StartPosition.Y; y <= ship.EndPosition.Y; y++)
                    {
                        if (PlayingBoard[x, y] == PositionStatus.OccupiedByShip)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (IndexOutOfRangeException)
            {                
            }

            return false;
        }

        #endregion
    }
}
