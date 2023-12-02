using System;
using SnakeAnatomy;
using SnakeBehaviour;

public class Program
{
    public static void Main()
    {
        Game game = new Game();
        game.GameStart();
    }
} 

public class Game
{
    public int width, height;
    public bool isRunning = false;
 
    Snake snake;
    public Game()
    {   
        this.width = 60;
        this.height = 40;
        snake = new Snake();
        GameStart();
    }
     public void GameStart()
    {
        isRunning = true;
        GameLoop();

    }
    public void gameDraw()
    {

    }


    public void GameLoop()
    {
        while (this.isRunning)
        {

            ConsoleKeyInfo key = Console.ReadKey(true);

            //DELAY OD 500ms -> 0.5 sec -> treba isprobavat jel dosta i tweakat
            Thread.Sleep(500);


            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    snake.head.positionX -= 1;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    snake.head.positionX += 1;
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    snake.head.positionY += 1;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    snake.head.positionY -= 1;
                    break;
                case ConsoleKey.Escape:
                    break;
                default:
                    break;
            }

        }
    }
}