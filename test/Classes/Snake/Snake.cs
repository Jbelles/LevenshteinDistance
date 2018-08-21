
using SnakeTest.Classes.Nodes;
using SnakeTest.Interfaces;
using System.Collections.Generic;
using System.Linq;
namespace SnakeTest.Classes
{
    public class Snake
    {
        public Snake(INode node)
        {
            Head = node;
        }
        public INode Head { get; set; }
        public bool IsEating { get; set; }
        public void Move(INode node, INode[,] matrix)
        {
            INode foodloc = null;
            Body.Insert(0, Head as Node);
            if (node is Food)
            {
                Head = new Node(node.X, node.Y);
                matrix[node.X, node.Y] = Head;
                IsEating = true;
                foodloc = Pipeline.GenerateFood(matrix, this);
            }
            else
            {
                Head = node;
            }

            List<INode> changed = new List<INode>();
            if (IsEating)
            {

            }
            else
            {
                changed.Add(Body.ElementAt(Body.Count - 1));
                Body.RemoveAt(Body.Count - 1);
            }
            IsEating = false;

            foreach (INode bodyNode in Body)
            {
                changed.Add(bodyNode);
            }
            changed.Add(Head);
            if (!(foodloc is null))
                changed.Add(foodloc);
            Pipeline.Draw(changed, this);
        }
        public List<INode> Body { get; set; } = new List<INode>();
        public byte Direction { get; set; } = Constants.UP; //default direction
        public void ChangeDirection(byte direction)
        {
            var XORDir = (Direction ^ direction);
            if (XORDir == 0b0000 || XORDir == 0b0011 || XORDir == 0b1100)
            {
                return;
            }
            Direction = direction;
        }
        public bool IsValid()
        {
            if (Head is Wall || Body.Contains(Head))
                return false;
            return true;
        }
    }
}
