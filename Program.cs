using System;
using System.Linq;
using System.Text;
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
    List<Tuple<int, int, string>> occupiedPositions = new();
    List<Tuple<int, int, string>> lastOccupiedPositions = new();
    Apple apple;
    ConsoleKey lastKey;
    public int currentFrame = 0;
    public int width, height;
    public bool isRunning = false;
    public int score = 0;
 
    Snake snake;
    public Game()
    {   
        this.width = 60;
        this.height = 40;
        snake = new Snake();
        GameStart();
    }
    public Game(int width, int height)
    {
        this.width = width;
        this.height = height;
        snake = new Snake();
    }
    public void GameStart()
    {
        isRunning = true;
        this.apple = new Apple(this.width, this.height, this.occupiedPositions);
        this.occupiedPositions.Add(new(this.apple.positionX, this.apple.positionY, "Apple"));
        GameLoop();

    }
    public void GameDraw()
    {
        this.currentFrame += 1;
        lastOccupiedPositions = occupiedPositions;
        occupiedPositions.Clear();
        if(this.currentFrame != 1)
            occupiedPositions.Add(new(this.apple.positionX, this.apple.positionY, "Apple"));
        occupiedPositions.Add(new(snake.head.positionX, snake.head.positionY, "Head"));
        foreach (Body body in snake.wholeBody)
        {
            occupiedPositions.Add(new(body.positionX, body.positionY, "Body"));
        }

        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                
                if (occupiedPositions.Contains(new(y, x, "Head")))
                {
                    Console.SetCursorPosition(y, x);
                    Console.Write('O');
                }
                else if (occupiedPositions.Contains(new(y, x, "Body")))
                {
                    Console.SetCursorPosition(y, x);
                    Console.Write('x');
                }
                else if (occupiedPositions.Contains(new(y, x, "Apple")))
                {
                    Console.SetCursorPosition(y, x);
                    Console.Write('@');
                }

                if (x == 0 || y == 0 || x == height - 1 || y == width - 1)
                {
                    Console.SetCursorPosition(y, x);
                    Console.Write('#');
                }
                if (y == lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item1 && x == lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item2)
                {
                    Console.SetCursorPosition(y, x);
                    Console.Write(' ');
                }
            }
            Console.Write('\n');

        }
    }

    void KeyPress()
    {

        while (this.isRunning)
            if (Console.KeyAvailable)
            {
                lastKey = Console.ReadKey(intercept: true).Key;
                if (Console.ReadKey(intercept: true).Key == ConsoleKey.A || Console.ReadKey(true).Key == ConsoleKey.LeftArrow)
                {
                    snake.currentlyFacing = 'W';
                }
                else if (Console.ReadKey(intercept: true).Key == ConsoleKey.D || Console.ReadKey(true).Key == ConsoleKey.RightArrow)
                {
                    snake.currentlyFacing = 'E';
                }
                else if (Console.ReadKey(intercept: true).Key == ConsoleKey.S || Console.ReadKey(true).Key == ConsoleKey.DownArrow)
                {
                    snake.currentlyFacing = 'S';
                }
                else if (Console.ReadKey(intercept: true).Key == ConsoleKey.W || Console.ReadKey(true).Key == ConsoleKey.UpArrow)
                {
                    snake.currentlyFacing = 'N';
                }
                else if (Console.ReadKey(intercept: true).Key == ConsoleKey.Escape)
                {
                    this.isRunning = false;
                }
            }
    }

    public void GameLoop()
    {
        new Thread(delegate ()
        {
            KeyPress();
        }).Start();
        while (this.isRunning)
        {
            
            snake.MoveSnake(this.width, this.height);
            if (snake.head.positionX == this.occupiedPositions[0].Item1 && snake.head.positionY == this.occupiedPositions[0].Item2)
            {
                score++;
                snake.AddBodySegment(new Body(lastOccupiedPositions[lastOccupiedPositions.Count-1].Item1, lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item2));
                this.apple = new Apple(this.width, this.height, this.occupiedPositions);
                this.occupiedPositions[0] = new(apple.positionX,apple.positionY,"Apple");
            }
            GameDraw();

            Thread.Sleep(100);
        }
    }    
    
    public class Apple
    {
        public int positionX, positionY;

        public Apple(int width,int height, List<Tuple<int, int, string>> occupiedPositions)
        {
            Random rnd = new Random();
            do
            {
                positionX = rnd.Next(1, width - 1);
                positionY = rnd.Next(1, height - 1);
            }
            while (occupiedPositions.Contains(new(positionX, positionY, "Head")) || occupiedPositions.Contains(new(positionX, positionY, "Body")) || occupiedPositions.Contains(new(positionX, positionY, "Apple")));
        }
    }
}