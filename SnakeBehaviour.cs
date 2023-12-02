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
        public List<Body> wholeBody;

        public char currentlyFacing='E';

        //-------i dalje ne znam zasto imamo ovo i zasto je bitna strana svijeta--------
        // pogledaj SnakeBehaviour 43-44 i Program 90-109

        public char direction;
        Dictionary<char, Tuple<int, int>> map = new Dictionary<char, Tuple<int, int>>()
        { {'N',new (0,-1)},
          {'E',new (1,0)},
          {'W',new (-1,0)},
          {'S',new (0,1)},
        };

        public Snake()
        {
            this.head = new Head(10,10);
            Body currentBody = new Body(9, 10);
            wholeBody = new List<Body>();
            AddBodySegment(currentBody);
        }

        public void AddBodySegment(Body body)
        {
            wholeBody.Add(body);
        }

        public void MoveSnake()
        {
            this.head.positionX += map[currentlyFacing].Item1;
            this.head.positionY += map[currentlyFacing].Item2;
            wholeBody[0].SetPosition(head);
            if (wholeBody.Count > 1)
                for (int i = 1; i < wholeBody.Count; i++)
                {
                    wholeBody[i].SetPosition(wholeBody[i-1]);
                }
        }
    }
}