namespace Pac_man.Map
{
    public class Tile
    {
        private bool isWalkable;

        public bool IsWalkable
        {
            get { return isWalkable; }
            set { isWalkable = value; }
        }

        public Tile(bool isWalkable)
        {
            this.isWalkable = isWalkable;
        }
    }
}
