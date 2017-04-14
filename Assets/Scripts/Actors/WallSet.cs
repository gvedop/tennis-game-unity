using System;
using System.Collections;
using System.Collections.Generic;

namespace TennisGame.Actors
{
    [Serializable]
    public class WallSet : IEnumerable, IEnumerable<IActor>
    {
        public WallComponent TopWall;
        public WallComponent BottomWall;
        public WallComponent LeftWall;
        public WallComponent RightWall;

        private IEnumerable<IActor> Actors()
        {
            yield return TopWall;
            yield return BottomWall;
            yield return LeftWall;
            yield return RightWall;
        }

        public IEnumerator<IActor> GetEnumerator()
        {
            return Actors().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
