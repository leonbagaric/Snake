using SnakeBehaviour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeAnatomy
{

    public abstract class Segment
    {
        public int positionX, positionY;
        public int previousX, previousY;


        public Segment(int positionX, int positionY)
        {
            this.positionX = positionX;
            this.positionY = positionY;
        }
    }

    public class Head : Segment
    {
        public Head(int x, int y): base(x, y)
        {

        }

        public void SetPosition(int x, int y)
        {
            positionX = x;
            positionY = y;
        }

    }

    public class Body : Segment
    {

        public Body(int x, int y):base(x, y)
        {

        }

        public void SetPosition(Segment master)
        {
            positionX = master.previousX;
            positionY = master.previousY;
            master.previousX = master.positionX;
            master.previousY = master.positionY;
        }

    }

}