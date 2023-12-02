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


    //Mozda sam sjebo, al kontam da radi jer dobivamo delay
    //prebaceno kretanje u head
    public void GameLoop()
    {
        while (this.isRunning)
        {
            snake.head.SetPosition();
        }
    }
}