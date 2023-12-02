﻿using SnakeBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAnatomy
{

    public class Segment
    {
        public int positionX, positionY;
        public int previousX, previousY;

        public Segment()
        {
            this.positionX = 0;
            this.positionY = 0;
        }

        public Segment(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }

        public Segment(int positionXY)
        {
            this.positionX = positionXY;
            this.positionY = positionXY;
        }
    }

    public sealed class Head : Segment
    {

        private static Head instance = null;
        private static readonly object padlock = new object();

        public Head() : base(0, 0)
        {

        }

        /*Singleton do iduceg komentara, osigurava da ima samo 
        1 instanca glave -> ne moze nitko napraviti 2+ glave
        a klasa sealed znaci da se ne moze naslijediti (radi se tako)*/
        public static Head Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Head();
                    }
                    return instance;
                }
            }
        }
        //-kraj singletona


        //prebaceno kretanje
        public bool SetPosition()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            //DELAY OD 500ms -> 0.5 sec -> treba isprobavat jel dosta i tweakat
            Thread.Sleep(500);


            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    this.positionX -= 1;
                    return true;
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    this.positionX += 1;
                    return true;
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    positionY += 1;
                    return true;
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    positionY -= 1;
                    return true;
                    break;
                case ConsoleKey.Escape:
                    return false;
                    break;
                default:
                    return true;
                    break;
            }
        }
    }

    public class Body : Segment
    {

        public Body(int positionX, int positionY):base(positionX, positionY)
        {

        }

        //hmhmhmhm vidi u Snake behaviour static klasu da vidis sta sam napravio s movementom
        public void SetPosition(Segment master)
        {
            this.positionX = master.positionX;
            this.positionY = master.positionY;
        }

    }
}