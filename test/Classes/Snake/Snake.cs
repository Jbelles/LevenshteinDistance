
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
        public bool Eating { get; set; }
        public void Move(INode node, INode[,] matrix)
        {
            INode foodloc = null;
            Body.Insert(0, Head as Node);
            if (node is Food)
            {
                Head = new Node(node.X, node.Y);
                matrix[node.X, node.Y] = Head;
                Eating = true;
                foodloc = Pipeline.GenerateFood(matrix, this);
            }
            else
            {
                Head = node;
            }

            List<INode> changed = new List<INode>();
            if (Eating)
            {

            }
            else
            {
                changed.Add(Body.ElementAt(Body.Count - 1));
                Body.RemoveAt(Body.Count - 1);
            }
            Eating = false;

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
        public byte Direction { get; set; } = 0b0001;
        public void ChangeDirection(byte direction)
        {
            var XORDir = (Direction ^ direction);
            if (XORDir == 0b0000 || XORDir == 0b0011 || XORDir == 0b1100)
            {
                return;
            }
            Direction = direction;
        }
        public void Push(Node node)
        {
            Body.Add(node);
        }

        public void Pop()
        {
            Body.RemoveAt(Body.Count() - 1);
        }
        public bool IsValid()
        {
            if (Head is Wall || Body.Contains(Head))
                return false;
            return true;
        }
    }
}
