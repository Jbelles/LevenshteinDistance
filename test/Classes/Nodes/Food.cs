using SnakeTest.Interfaces;

namespace SnakeTest.Classes.Nodes
{
    public class Food : INode
    {
        public Food(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
