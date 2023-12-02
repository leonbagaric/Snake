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
         * znaci kad upalis prva stvar je da se body (x) spawna na 0,0 umjesto 9,10 ko sto bi trebo na prvom frameu al poslje radi ok
         * druga stvar je sto ljevo i desno mozes noramlano ic al gore i dolje ne radi kako treba iako runa istom logikom
         * treca stvar je da randomly nece radit ni ljevo desno neg sam stane u mjestu i moras drzat A il D da krene dalje????
         * 
         * update: nez jesam sjebo al kad idem gore/dole krene se x povecat za otp 50 i onda treperi na dobrom mjestu i mjestu +50 na x osi
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
    public void GameDraw()
    {

        //Ovih par linija su uzasne al mi se nije dalo razmisljat previse pa svaki loop popunjavam ovu listu
        //umjesto da updateam kad se promjeni pozicija neceg

        List < Tuple<int, int, string> > occupiedPositions= new List<Tuple<int, int, string>>();
        occupiedPositions.Add(new(snake.head.positionX, snake.head.positionY, "Head"));
        foreach (Body body in snake.wholeBody)
        {
            occupiedPositions.Add(new(body.positionX, body.positionY,"Body"));
        }


        for(int x = 0; x < height; x++)
        {
            for(int y = 0; y < width; y++)
            {
                if (occupiedPositions.Contains(new(y,x,"Head")))
                {
                    Console.Write('o');
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
                else if(Console.ReadKey(intercept: true).Key == ConsoleKey.Escape)
                {
                    this.isRunning = false;
                }
            }
            snake.MoveSnake();
            Console.Clear();
            GameDraw();

            //probo sam s async wait al onda cijela funkcija morabit async pa mi se nije dalo zajebavat
            Thread.Sleep(250);
        }
    }
}