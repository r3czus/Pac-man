using Pac_man.Utilities;
using Pac_man.Entities;


namespace Pac_man.Map
{
    public class MapLoader
    {
        public GameMap LoadMap(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Nie znaleziono mapy {filePath}");
                throw new FileNotFoundException($"Mapa nie znaleziona {filePath}");
            }

            string[] lines = File.ReadAllLines(filePath);
            int height = lines.Length;
            int width = lines.Max(line => line.Length); 

            Tile[,] tiles = new Tile[height, width]; // tworzenie tablicy 2 wymiarowej 
            List<Food> foods = new List<Food>();
            Position playerStart = null;
            List<Position> ghostStarts = new List<Position>();

            for (int y = 0; y < height; y++)
            {
                string line = lines[y];

                for (int x = 0; x < width; x++)
                {
                    char c = x < line.Length ? line[x] : ' '; // jeśli wyjdziemy poza index dajemy spacje
                    Position position = new Position(x, y);

                    // stawianie ściany
                    if (tiles[y, x] == null)
                    {
                        tiles[y, x] = new Tile(true); 
                    }

                    switch (c)
                    {
                        case '#':
                            tiles[y, x] = new Tile(false);
                            break;
                        case ' ':
                            tiles[y, x] = new Tile(true);
                            break;
                        case '.':
                            tiles[y, x] = new Tile(true);
                            foods.Add(new Food(position, false));
                            break;
                        case 'O':
                            tiles[y, x] = new Tile(true);
                            foods.Add(new Food(position, true));
                            break;
                        case 'P':
                            tiles[y, x] = new Tile(true);
                            playerStart = position;
                            break;
                        case 'G':
                            tiles[y, x] = new Tile(true);
                            ghostStarts.Add(position);
                            break;
                        default:
                            tiles[y, x] = new Tile(true);
                            break;
                    }
                }
            }

            return new GameMap(width, height, tiles, foods, playerStart, ghostStarts);
        }
    }
}
