using Pac_man.Utilities;

namespace Pac_man.Entities
{
    public abstract class Entity
    {
        protected Position position;
        protected char symbol;

        public Position Position
        {
            get { return position; }
            set { position = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public abstract void Move(Direction direction);

        public virtual void Draw()
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(symbol);
        }
    }
}
