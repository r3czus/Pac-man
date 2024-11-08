using System;

namespace Pac_man.Utilities
{
    public class InputHandler
    {
        public Direction GetInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        return Direction.Up;
                    case ConsoleKey.DownArrow:
                        return Direction.Down;
                    case ConsoleKey.LeftArrow:
                        return Direction.Left;
                    case ConsoleKey.RightArrow:
                        return Direction.Right;
                }
            }
            return Direction.None;
        }
    }
}
