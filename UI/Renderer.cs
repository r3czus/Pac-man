using System;
using System.Collections.Generic;
using Pac_man.Map;
using Pac_man.Entities;

namespace Pac_man.UI
{
    public class Renderer
    {
        public void Render(GameMap map, List<Entity> entities, Player player, int level)
        {
            Console.Clear();

            // Rysowanie mapy
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    char tileChar = map.Tiles[y, x].IsWalkable ? ' ' : '#';
                    Console.Write(tileChar);
                }
            }

            // Rysowanie jedzenia
            foreach (var food in map.Foods)
            {
                food.Draw();
            }

            // Rysowanie bytów (gracza i duchów)
            foreach (var entity in entities)
            {
                entity.Draw();
            }

            // Wyświetlanie informacji o punkcie, życiu i poziomie
            Console.SetCursorPosition(0, map.Height + 1);
            Console.WriteLine($"Score: {player.Score}  Lives: {player.Lives}  Level: {level}");
        }

        public void RenderGameOver()
        {
            Console.Clear();
            Console.WriteLine("GAME OVER");
        }

        public void RenderWin()
        {
            Console.Clear();
            Console.WriteLine("GRATULACJE! WYGRAŁEŚ GRĘ!");
        }
    }
}