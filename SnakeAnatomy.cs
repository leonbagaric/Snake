using SnakeBehaviour;
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

        public Head()
        {

        }

        public Head(int x, int y)
        {
            positionX = x;
            positionY = y;
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

        public void SetPosition(int x, int y)
        {
            this.positionX = x;
            this.positionY = y;
        }

    }

    public class Body : Segment
    {

        public Body(int positionX, int positionY)
        {

        }

        public void SetPosition(Segment master)
        {
            this.positionX = master.previousX;
            this.positionY = master.previousY;
            master.previousX = master.positionX;
            master.previousY = master.positionY;
        }

    }
}