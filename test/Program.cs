using System;
using SnakeTest.Interfaces;
using SnakeTest.Classes.Nodes;
using SnakeTest.Classes;

public class Program
{
    static (int x, int y) pos = (20, 20);
    static bool exit = false;
    static Snake Player;

    public static void Main()
    {
        Pipeline.ArenaSize = 30;
        INode[,] matrix = new INode[Pipeline.ArenaSize, Pipeline.ArenaSize];
        Console.CursorVisible = false;
        Snake player = Pipeline.InitArena(matrix, (20, 20));

        while (true)
        {
            Pipeline.ProcessInput(player);
            Pipeline.Update(matrix, player);
        }
    }



    public static void ValidateLocation()
    {
        if (Player.Head is Wall || Player.Body.Contains(Player.Head))
            exit = true;
    }
    

    
}