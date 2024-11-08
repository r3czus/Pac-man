using Pac_man.Entities;
using Pac_man.Map;
using Pac_man.UI;
using Pac_man.Utilities;
using System;
using System.Collections.Generic;

namespace Pac_man.GameLogic
{
    public class GameManager
    {
        private Player player;
        private List<Ghost> ghosts;
        private GameMap map;
        private Renderer renderer;
        private InputHandler inputHandler;

        private List<string> mapPaths;
        private int currentMapIndex;

        public void StartGame()
        {
            mapPaths = new List<string>
            {
                "MapFiles/map1.txt",
                "MapFiles/map2.txt",
                "MapFiles/map3.txt"
                // Dodaj więcej map według potrzeb
            };
            currentMapIndex = 0;

            renderer = new Renderer();
            inputHandler = new InputHandler();

            LoadMap(currentMapIndex);

            GameLoop();
        }

        private void LoadMap(int mapIndex)
        {
            MapLoader mapLoader = new MapLoader();
            map = mapLoader.LoadMap(mapPaths[mapIndex]);

            player = new Player(map.PlayerStart, map);

            ghosts = new List<Ghost>();
            foreach (var ghostPosition in map.GhostStarts)
            {
                ghosts.Add(new Ghost(ghostPosition, map));
            }
        }

        private void GameLoop()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Update();
                renderer.Render(map, GetEntities(), player, currentMapIndex + 1);
                System.Threading.Thread.Sleep(120); // Kontrola szybkości gry

                if (player.Lives <= 0)
                {
                    isRunning = false;
                    renderer.RenderGameOver();
                }
            }
        }

        private void Update()
        {
            Direction direction = inputHandler.GetInput();
            player.Move(direction);

            // Aktualizacja duchów
            foreach (var ghost in ghosts)
            {
                if (ghost.Behavior == GhostBehavior.Chase)
                {
                    ghost.Chase(player);
                }
                else if (ghost.Behavior == GhostBehavior.Scatter)
                {
                    ghost.Scatter();
                }
            }

            CheckCollisions();
        }

        private void CheckCollisions()
        {

            var foodsToRemove = new List<Food>();

            foreach (var food in map.Foods)
            {
                if (player.Position.Equals(food.Position))
                {
                    foodsToRemove.Add(food);
                }
            }

            // Gracz zjada jedzenie
            foreach (var food in foodsToRemove)
            {
                player.Eat(food);
                map.Foods.Remove(food);
            }

            // Sprawdzenie kolizji z duchami
            foreach (var ghost in ghosts)
            {
                if (player.Position.Equals(ghost.Position))
                {
                    player.Lives--;
                    player.Position = map.PlayerStart;
                    // dodanie zabicia duchów
                    break;
                }
            }

            // Sprawdzenie, czy wszystkie owoce zostały zebrane
            if (map.Foods.Count == 0)
            {
                currentMapIndex++;
                if (currentMapIndex < mapPaths.Count)
                {
                    LoadMap(currentMapIndex);
                }
                else
                {
                    renderer.RenderWin();
                    Environment.Exit(0); // Zakończ grę
                }
            }
        }

        private List<Entity> GetEntities()
        {
            List<Entity> entities = new List<Entity> { player };
            entities.AddRange(ghosts);
            entities.AddRange(map.Foods);
            return entities;
        }
    }
}