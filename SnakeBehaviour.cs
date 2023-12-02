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
        public Body body;
        public List<Body> wholeBody;

        //i dalje ne znam zasto imamo ovo i zasto je bitna strana svijeta
        public char direction;
        Dictionary<char, Tuple<int, int>> map = new Dictionary<char, Tuple<int, int>>()
        { {'N',new (0,1)},
          {'E',new (1,0)},
          {'W',new (-1,0)},
          {'S',new (0,-1)},
        };

        public Snake()
        {
            this.head = new Head();
            this.body = new Body(-1, 0);
            wholeBody = new List<Body>();
            AddBodySegment(body);
        }

        public void AddBodySegment(Body body)
        {
            wholeBody.Add(body);
        }
    }

    public static class SnakeBehavior
    {
        //nadam se da static varijable se mogu mijenjat jer po logici static != readonly
        private static int i;

        public static void MoveHead(Head head, List<Body> wholeBody)
        {
            head.SetPosition();
            int tempPositionX = head.positionX;
            if(head.positionX != tempPositionX)
            {
                MovingBody(head, wholeBody);
            }
        }

        public static void MovingBody(Head head, List<Body> wholeBody)
        {
            for (i = 1; i < wholeBody.Count; i++)
            {
                wholeBody[0].SetPosition(head);
                wholeBody[i].SetPosition(wholeBody[i - 1]);
            }
        }
    }
}