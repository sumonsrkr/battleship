namespace battleship.models
{
    public class Ship
    {
        public Position StartPosition { get; set; }

        public Position EndPosition { get; set; }

        public Ship(Position startPosition, Position endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }
}
