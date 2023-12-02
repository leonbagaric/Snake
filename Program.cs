using System;
using System.Linq;
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

        /*
         * Znaci sad je napravljen dos dobar dio posla
         * imamo renderanje zasad i jos par stvari je sredeno al ima problem koji neznam jos do ceg je
         * 
         * znaci kad upalis prva stvar je da se body (x) spawna na 0,0 umjesto 9,10 ko sto bi trebo na prvom frameu al poslje radi ok
         * druga stvar je sto ljevo i desno mozes noramlano ic al gore i dolje ne radi kako treba iako runa istom logikom
         * treca stvar je da randomly nece radit ni ljevo desno neg sam stane u mjestu i moras drzat A il D da krene dalje????
         */
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
        GameLoop();

    }
    public void gameDraw()
    {

        //Ovih par linija su uzasne al mi se nije dalo razmisljat previse pa svaki loop popunjavam ovu listu
        //umjesto da updateam kad se promjeni pozicija neceg

        List < Tuple<int, int, string> > occupiedPositions= new List<Tuple<int, int, string>>();
        occupiedPositions.Add(new(snake.head.positionX, snake.head.positionY, "Head"));
        foreach (Segment seg in snake.wholeBody)
        {
            occupiedPositions.Add(new(seg.positionX, seg.positionY,"Body"));
        }


        for(int x = 0; x < height; x++)
        {
            for(int y = 0; y < width; y++)
            {
                if (occupiedPositions.Contains(new(y,x,"Head")))
                {
                    Console.Write('X');
                }
                else if (occupiedPositions.Contains(new(y, x, "Body")))
                {
                    Console.Write('x');
                }
                else
                {
                    Console.Write(' ');
                }
            }
        }
    }


    public void GameLoop()
    {
        while (this.isRunning)
        {
            if (Console.KeyAvailable)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.A || Console.ReadKey(true).Key == ConsoleKey.LeftArrow)
                {
                    snake.currentlyFacing = 'W';
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.D || Console.ReadKey(true).Key == ConsoleKey.RightArrow)
                {
                    snake.currentlyFacing = 'E';
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.S || Console.ReadKey(true).Key == ConsoleKey.DownArrow)
                {
                    snake.currentlyFacing = 'S';
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.W || Console.ReadKey(true).Key == ConsoleKey.UpArrow)
                {
                    snake.currentlyFacing = 'N';
                }
                else if(Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    this.isRunning = false;
                }
            }
            snake.MoveSnake();
            Console.Clear();
            gameDraw();


            //DELAY OD 500ms -> 0.5 sec -> treba isprobavat jel dosta i tweakat
            Thread.Sleep(500);
        }
    }
}