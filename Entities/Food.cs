using Pac_man.Utilities;

namespace Pac_man.Entities
{
    public class Food : Entity
    {
        private bool isPowerPellet;

        public bool IsPowerPellet
        {
            get { return isPowerPellet; }
            set { isPowerPellet = value; }
        }

        public Food(Position position, bool isPowerPellet)
        {
            this.position = position;
            this.isPowerPellet = isPowerPellet;
            symbol = isPowerPellet ? 'O' : '.';
        }

        public override void Move(Direction direction)
        {
        }

        public override void Draw()
        {
            Console.SetCursorPosition(position.X, position.Y);
            if (isPowerPellet)
            {
                Console.ForegroundColor = ConsoleColor.White; // Kolor dla power pelletu
                Console.Write(symbol);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Kolor dla zwykłego jedzenia
                Console.Write(symbol);
                Console.ResetColor();
            }
        }
    }
}
