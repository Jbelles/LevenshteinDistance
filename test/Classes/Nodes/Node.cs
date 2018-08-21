using SnakeTest.Interfaces;

namespace SnakeTest.Classes.Nodes
{
    public class Node : INode
    {
        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
