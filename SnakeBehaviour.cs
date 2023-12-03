using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeAnatomy;

namespace SnakeBehaviour
{
    public class Snake
    {
        public Head head;
        public Body currentBody;
        public List<Body> wholeBody;

        public Dictionary<char, Tuple<int, int>> map = new Dictionary<char, Tuple<int, int>>() 
        { {'N',new (0,-1)},
          {'E',new (1,0)},
          {'W',new (-1,0)},
          {'S',new (0,1)},
        };
        
        public char currentlyFacing='E';
        public Snake()
        {
            this.head = new Head(10,10);
            this.currentBody = new Body(9, 10);
            wholeBody = new List<Body>();
            AddBodySegment(currentBody);
        }

        public void AddBodySegment(Body body)
        {
            wholeBody.Add(body);
        }

        public void MoveSnake(int width, int height)
        {
            if(this.head.positionX + map[currentlyFacing].Item1 <= 0)
            {
                this.head.positionX = width - 1;
            }
            else if (this.head.positionX + map[currentlyFacing].Item1 >= width)
            {
                this.head.positionX = 1;
            }
            else if (this.head.positionY + map[currentlyFacing].Item2 <= 0)
            {
                this.head.positionY = height - 1;
            }
            else if (this.head.positionY + map[currentlyFacing].Item2 >= height)
            {
                this.head.positionY = 1;
            }
            else
            {
                this.head.positionX += map[currentlyFacing].Item1;
                this.head.positionY += map[currentlyFacing].Item2;
            }
            wholeBody[0].SetPosition(head);
            if (wholeBody.Count > 1)
                for (int i = 1; i < wholeBody.Count; i++)
                {
                    wholeBody[i].SetPosition(wholeBody[i-1]);
                }
        }
    }
}