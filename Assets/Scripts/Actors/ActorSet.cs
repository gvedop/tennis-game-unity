using System;
using System.Collections;
using System.Collections.Generic;

namespace TennisGame.Actors
{
    [Serializable]
    public class ActorSet : IEnumerable, IEnumerable<IActor>
    {
        public PlatformComponent TopPlatform;
        public PlatformComponent BottomPlatform;
        public BallComponent Ball;
        public WallSet WallSet;

        private IEnumerable<IActor> Actors()
        {
            yield return TopPlatform;
            yield return BottomPlatform;
            yield return Ball;
            foreach (var next in WallSet)
                yield return next;
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
