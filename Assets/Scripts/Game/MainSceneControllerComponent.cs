using UnityEngine;
using TennisGame.Actors;

namespace TennisGame.Game
{
    public class MainSceneControllerComponent: MonoBehaviour
    {
        public PlatformComponent TopPlatform;
        public PlatformComponent BottomPlatform;
        public WallComponent TopWall;
        public WallComponent BottomWall;
        public WallComponent LeftWall;
        public WallComponent RightWall;
        public BallComponent Ball;

        private void Awake()
        {

        }
    }
}
