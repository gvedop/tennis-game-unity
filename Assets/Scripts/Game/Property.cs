using UnityEngine;

namespace TennisGame.Game
{
    public class Property
    {
        public Property()
        {

        }

        public int TopPlayerScore { get; set; }

        public int BottomPlayerScore { get; set; }

        public void IncTopPlayerScore()
        {
            TopPlayerScore += 1;
        }

        public void IncBottomPlayerScore()
        {
            BottomPlayerScore += 1;
        }

        public void Clear()
        {
            TopPlayerScore = 0;
            BottomPlayerScore = 0;
        }
    }
}
