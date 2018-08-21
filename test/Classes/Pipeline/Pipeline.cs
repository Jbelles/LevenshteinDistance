using SnakeTest.Classes.Nodes;
using SnakeTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeTest.Classes
{
    public static class Pipeline
    {
        public static int ArenaSize;
        public static INode GenerateFood(INode[,] matrix, Snake player)
        {
            Random rand = new Random();
            var x = rand.Next(1, ArenaSize);
            var y = rand.Next(1, ArenaSize);
            while (matrix[x, y] is Wall || matrix[x, y] == player.Head || player.Body.Contains(matrix[x, y]))
            {
                x = rand.Next(1, ArenaSize);
                y = rand.Next(1, ArenaSize);
            }
            matrix[x, y] = new Food(x, y);
            return matrix[x, y];

        }

        public static Snake InitArena(INode[,] matrix, (int x, int y) pos)
        {
            try
            {
                Snake player = null;
                for (int i = 0; i < Pipeline.ArenaSize; i++)
                {
                    for (int j = 0; j < Pipeline.ArenaSize; j++)
                    {
                        //if it's a wall
                        if (i == 0 || i == Pipeline.ArenaSize - 1 || j == 0 || j == Pipeline.ArenaSize - 1)
                        {
                            matrix[i, j] = new Wall(i, j);
                        }
                        else
                            matrix[i, j] = new Node(i, j);
                        //if its the player
                        if (pos.x == i && pos.y == j)
                        {
                            player = new Snake(matrix[i, j]);
                        }


                    }
                }
                GenerateFood(matrix, player);
                Draw(matrix, player);

                return player;
            }
            finally
            {
                Thread.Sleep(1000);
            }
            
        }


        public async static Task ProcessInput(Snake player)
        {

            ConsoleKeyInfo input = new ConsoleKeyInfo();
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            player.ChangeDirection(Constants.UP);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            player.ChangeDirection(Constants.LEFT);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            player.ChangeDirection(Constants.DOWN);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            player.ChangeDirection(Constants.RIGHT);
                            break;
                        }
                }
            }
            Thread.Sleep(100);
        }

        public static void Update(INode[,] matrix, Snake player)
        {
            switch (player.Direction)
            {
                case Constants.UP:
                    {
                        player.Move(matrix[player.Head.X, player.Head.Y - 1], matrix);
                        break;
                    }
                case Constants.LEFT:
                    {
                        player.Move(matrix[player.Head.X - 1, player.Head.Y], matrix);
                        break;
                    }
                case Constants.DOWN:
                    {
                        player.Move(matrix[player.Head.X, player.Head.Y + 1], matrix);
                        break;
                    }
                case Constants.RIGHT:
                    {
                        player.Move(matrix[player.Head.X + 1, player.Head.Y], matrix);
                        break;
                    }
            }
            if (!player.IsValid())
            {
                Environment.Exit(1);
            }
        }

        public static void Draw(INode[,] matrix, Snake player)
        {
            Console.Clear();
            foreach (INode node in matrix)
            {
                Console.SetCursorPosition(node.X, node.Y);
                if (node is Wall)
                {
                    Console.Write("X");
                }
                else if (player.Head == node)
                {
                    Console.Write("0");
                }
                else if (player.Body.Contains(node))
                {
                    Console.Write("O");
                }
                else if (node is Food)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
                if (node.X == ArenaSize)
                {
                    Console.WriteLine();
                }
            }
        }

        //called by passing in a list of elements that need to be redrawn
        public static void Draw(List<INode> changed, Snake player)
        {
            foreach (INode node in changed)
            {
                Console.SetCursorPosition(node.X, node.Y);
                if (node is Wall)
                {
                    Console.Write("X");
                }
                else if (player.Head == node)
                {
                    Console.Write("0");
                }
                else if (player.Body.Contains(node))
                {
                    Console.Write("O");
                }
                else if (node is Food)
                {
                    Console.Write("*");
                }
                else
                {
                    Console.Write(" ");
                }
                if (node.X == ArenaSize)
                {
                    Console.WriteLine();
                }
            }
        }
    }



}
