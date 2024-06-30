
namespace DungeonGeneration
{
    public interface ICompositePart
    {
        public int width    { get; set; }
        public int height   { get; set; }

        public int xPos { get; set; }
        public int yPos { get; set; }
    }
}