using System;
using System.Linq;
using System.Text;
using SnakeAnatomy;
using SnakeBehaviour;


public class Program
{
    public static void Main()
    {
        Game game = new Game(40,20,false);
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
    public bool debugBot = true;
    public int score = 0;
    public int speed;
    Tuple<int,int,string> a;
    Tuple<int,int,string> b;
    Snake snake;
    Tuple<int, int, string> tempPos;
    public Game()
    {   
        this.width = 60;
        this.height = 40;
        debugBot = false;
        snake = new Snake();
        GameStart();
    }
    public Game(bool bot)
    {
        this.width = 60;
        this.height = 40;
        debugBot = bot;
        snake = new Snake();
        GameStart();
    }
    public Game(int width, int height)
    {
        this.width = width;
        this.height = height;
        snake = new Snake();
    }
    public Game(int width, int height, bool bot)
    {
        this.width = width;
        this.height = height;
        this.debugBot = bot;
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


        lastOccupiedPositions = new List<Tuple<int,int,string>>( occupiedPositions);
        this.currentFrame++;
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

                else
                {
                    Console.Write(' ');
                }

                if (x == 0 || y == 0 || x == height - 1 || y == width - 1)
                {
                    Console.SetCursorPosition(y, x);
                    Console.Write('#');
                }


                
            }

        }
        Console.Write('\n');

        Console.WriteLine($"Frame {currentFrame}");
        Console.WriteLine($"Score {score}");
    }

    void KeyPress()
    {

        while (this.isRunning && debugBot == false)
            if (Console.KeyAvailable)
            {
                
                ConsoleKeyInfo key = Console.ReadKey(true);
                lastKey = key.Key;
                if ((key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)&& snake.currentlyFacing != 'E')
                {
                    snake.currentlyFacing = 'W';
                }
                if ((key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)&& snake.currentlyFacing != 'W')
                {
                    snake.currentlyFacing = 'E';
                }
                if ((key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)&& snake.currentlyFacing != 'N')
                {
                    snake.currentlyFacing = 'S';
                }
                if ((key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)&& snake.currentlyFacing != 'S')
                {
                    snake.currentlyFacing = 'N';
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    this.isRunning = false;
                }
            }
        while (this.isRunning && debugBot == true)
        {
            if(snake.head.positionX > apple.positionX)
            {
                snake.currentlyFacing = 'W';
            }
            else if (snake.head.positionX < apple.positionX)
            {
                snake.currentlyFacing = 'E';
            }
            else if (snake.head.positionY > apple.positionY)
            {
                snake.currentlyFacing = 'N';
            }
            else if (snake.head.positionY < apple.positionY)
            {
                snake.currentlyFacing = 'S';
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
            speed = (int)(300 / Math.Exp((double)(score+1)/60));
            snake.MoveSnake(this.width, this.height);
            if (snake.head.positionX == this.occupiedPositions[0].Item1 && snake.head.positionY == this.occupiedPositions[0].Item2)
            {
                score++;
                tempPos = new (lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item1, lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item2, lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item3);
            
                GameDraw();

                snake.AddBodySegment(new Body(lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item1, lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item2));
                occupiedPositions.Add(new(lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item1, lastOccupiedPositions[lastOccupiedPositions.Count - 1].Item2, "Body"));
                this.apple = new Apple(this.width, this.height, this.occupiedPositions);
                this.occupiedPositions[0] = new(apple.positionX, apple.positionY, "Apple");
                lastOccupiedPositions.Add(new (tempPos.Item1, tempPos.Item2, tempPos.Item3 ));
            }
            else
            {
                GameDraw();
            }
            Thread.Sleep(speed);
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