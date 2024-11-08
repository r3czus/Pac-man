using Pac_man.Utilities;
using Pac_man.Entities;

namespace Pac_man.Map
{
    public class GameMap
    {
        private int width;
        private int height;
        private Tile[,] tiles;
        private List<Food> foods;
        private Position playerStart;
        private List<Position> ghostStarts;

        public int Width => width;
        public int Height => height;
        public Tile[,] Tiles => tiles;
        public List<Food> Foods => foods;
        public Position PlayerStart => playerStart;
        public List<Position> GhostStarts => ghostStarts;

        public GameMap(int width, int height, Tile[,] tiles, List<Food> foods, Position playerStart, List<Position> ghostStarts)
        {
            this.width = width;
            this.height = height;
            this.tiles = tiles; // tiles[y, x]
            this.foods = foods;
            this.playerStart = playerStart;
            this.ghostStarts = ghostStarts;
        }

        public Tile GetTileAt(Position position)
        {
            if (position.X >= 0 && position.X < width && position.Y >= 0 && position.Y < height)
            {
                return tiles[position.Y, position.X]; // Poprawione indeksowanie
            }
            else
            {
                return null;
            }
        }
    }
}