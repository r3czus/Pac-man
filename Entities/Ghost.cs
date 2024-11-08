using Pac_man.Utilities;
using Pac_man.Map;

namespace Pac_man.Entities
{
    public class Ghost : Entity
    {
        private GhostBehavior behavior;
        private GameMap map;
        private Random random;
        private Direction lastDirection;

        public GhostBehavior Behavior
        {
            get { return behavior; }
            set { behavior = value; }
        }

        public Ghost(Position startPosition, GameMap map)
        {
            this.position = startPosition;
            this.symbol = 'G';
            this.behavior = GhostBehavior.Scatter;
            this.map = map;
            this.random = new Random();
            this.lastDirection = Direction.None;
        }

        public override void Move(Direction direction)
        {
            // Oblicz nową pozycję na podstawie kierunku ruchu
            Position newPosition = position.Move(direction);

            // Sprawdź, czy nowa pozycja jest dostępna (nie ma ściany)
            if (IsWalkable(newPosition))
            {
                position = newPosition;
                lastDirection = direction;
            }
            // Jeśli ruch jest niemożliwy, nie zmieniaj pozycji
        }

        public void Chase(Player player)
        {
            // Pobierz listę możliwych kierunków ruchu
            List<Direction> possibleDirections = GetPossibleDirections();

            if (possibleDirections.Count == 0)
                return;

            // Wybierz kierunek, który minimalizuje odległość do gracza
            Direction bestDirection = possibleDirections[0];
            double minDistance = Distance(position.Move(bestDirection), player.Position);

            foreach (var dir in possibleDirections)
            {
                Position newPos = position.Move(dir);
                double dist = Distance(newPos, player.Position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    bestDirection = dir;
                }
            }

            Move(bestDirection);
        }

        public void Scatter()
        {
            // Pobierz listę możliwych kierunków ruchu
            List<Direction> possibleDirections = GetPossibleDirections();

            if (possibleDirections.Count == 0)
                return;

            // Wybierz losowy kierunek ruchu
            int index = random.Next(possibleDirections.Count);
            Direction randomDirection = possibleDirections[index];

            Move(randomDirection);
        }

        private List<Direction> GetPossibleDirections()
        {
            List<Direction> possibleDirections = new List<Direction>();

            // Sprawdź każdy kierunek i dodaj do listy, jeśli ruch jest możliwy i nie jest przeciwny do ostatniego ruchu
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                if (dir == Direction.None || dir == OppositeDirection(lastDirection))
                    continue;

                Position newPos = position.Move(dir);
                if (IsWalkable(newPos))
                {
                    possibleDirections.Add(dir);
                }
            }

            return possibleDirections;
        }

        private bool IsWalkable(Position pos)
        {
            Tile tile = map.GetTileAt(pos);
            return tile != null && tile.IsWalkable;
        }

        private double Distance(Position a, Position b)
        {
            int dx = a.X - b.X;
            int dy = a.Y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private Direction OppositeDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                case Direction.Left: return Direction.Right;
                case Direction.Right: return Direction.Left;
                default: return Direction.None;
            }
        }

        public override void Draw()
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.ForegroundColor = ConsoleColor.Red; // Możesz zmienić kolor dla różnych duchów
            Console.Write(symbol);
            Console.ResetColor();
        }
    }
}
