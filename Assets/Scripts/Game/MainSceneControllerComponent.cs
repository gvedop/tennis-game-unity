using UnityEngine;
using TennisGame.Actors;
using TennisGame.Core;

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
        public Canvas FieldCanvas;
        public RectTransform FieldRect;
        public Camera FieldCamera;
        public float WallWidth = 100f;
        public float PlatformSpeed = 300f;
        public float BallSpeed = 300f;

        private Field field;

        private void Awake()
        {
            if (!TopPlatform)
                throw new UnassignedReferenceException("TopPlatform doesn't set.");
            if (!BottomPlatform)
                throw new UnassignedReferenceException("BottomPlatform doesn't set.");
            if (!Ball)
                throw new UnassignedReferenceException("Ball doesn't set.");
            if (!TopWall)
                throw new UnassignedReferenceException("TopWall doesn't set.");
            if (!BottomWall)
                throw new UnassignedReferenceException("BottomWall doesn't set.");
            if (!LeftWall)
                throw new UnassignedReferenceException("LeftWall doesn't set.");
            if (!RightWall)
                throw new UnassignedReferenceException("RightWall doesn't set.");
            if (!FieldCanvas)
                throw new UnassignedReferenceException("FieldCanvas doesn't set.");
            if (!FieldRect)
                throw new UnassignedReferenceException("FieldRect doesn't set.");
            if (!FieldCamera)
                throw new UnassignedReferenceException("FieldCamera doesn't set.");
        }

        private void Start()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            field = new Field(FieldCanvas, FieldRect, FieldCamera);

            TopPlatform.MulLocalScale(field.WidthScale);
            TopPlatform.SetLocalPosition(field.TopCenter.WithSubY(TopPlatform.SelfSpriteRenderer.bounds.size.y / 2f));
            TopPlatform.SetBorder(field.TopLeftCorner.x, field.TopRightCorner.x);
            TopPlatform.Speed = PlatformSpeed;

            BottomPlatform.MulLocalScale(field.WidthScale);
            BottomPlatform.SetLocalPosition(field.BottomCenter.WithAddY(BottomPlatform.SelfSpriteRenderer.bounds.size.y / 2f));
            BottomPlatform.SetBorder(field.BottomLeftCorner.x, field.BottomRightCorner.x);
            BottomPlatform.Speed = PlatformSpeed;

            Ball.MulLocalScale(field.WidthScale);
            Ball.SetLocalPosition(field.Center);
            Ball.Speed = BallSpeed;

            var middleWallWidth = WallWidth / 2f;
            TopWall.Init(new Vector2(field.Width + 2 * WallWidth, WallWidth), field.TopCenter.WithAddY(middleWallWidth));
            BottomWall.Init(new Vector2(field.Width + 2 * WallWidth, WallWidth), field.BottomCenter.WithSubY(middleWallWidth));
            LeftWall.Init(new Vector2(WallWidth, field.Height), field.LeftCenter.WithSubX(middleWallWidth));
            RightWall.Init(new Vector2(WallWidth, field.Height), field.RightCenter.WithAddX(middleWallWidth));

            TopWall.IgnoreCollisions(TopPlatform.SelfCollider, BottomPlatform.SelfCollider);
            BottomWall.IgnoreCollisions(TopPlatform.SelfCollider, BottomPlatform.SelfCollider);
            LeftWall.IgnoreCollisions(TopPlatform.SelfCollider, BottomPlatform.SelfCollider);
            RightWall.IgnoreCollisions(TopPlatform.SelfCollider, BottomPlatform.SelfCollider);
        }
    }
}
