using Pac_man.Utilities;
using Pac_man.Map;

namespace Pac_man.Entities
{
    public class Player : Entity
    {
        private int lives;
        private int score;
        private GameMap map;

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Player(Position startPosition, GameMap map)
        {
            this.position = startPosition;
            this.symbol = 'P';
            this.lives = 3;
            this.score = 0;
            this.map = map;
        }

        public override void Move(Direction direction)
        {
            Position newPosition = position.Move(direction);

           
            if (IsWalkable(newPosition))
            {
                position = newPosition;
            }
            // Jeśli ruch jest niemożliwy, pozostajemy na miejscu
        }

        public void Eat(Food food)
        {
            
            if (food.IsPowerPellet)
            {
                score += 50;
                // dodanie  reakcji na duchy
            }
            else
            {
                score += 10; // Przykładowa wartość za zwykłe jedzenie
            }

        }

        public override void Draw()
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.ForegroundColor = ConsoleColor.Yellow; // Kolor gracza
            Console.Write(symbol);
            Console.ResetColor();
        }

        private bool IsWalkable(Position pos)
        {
            Tile tile = map.GetTileAt(pos);
            return tile != null && tile.IsWalkable;
        }
    }
}
