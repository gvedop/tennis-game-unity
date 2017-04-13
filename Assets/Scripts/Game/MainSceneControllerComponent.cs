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

        private Field field;

        private void Awake()
        {
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
            if (!FieldCanvas || !FieldRect || !FieldCamera)
                return;
            field = new Field(FieldCanvas, FieldRect, FieldCamera);

            //TopPlatform.MulLocalScale(field.WidthScale);


            //BottomPlatform.MulLocalScale(field.WidthScale);

            


            //player.transform.localScale = player.transform.localScale * field.WidthScale;
            //adversary.transform.localScale = adversary.transform.localScale * field.WidthScale;
            //ball.transform.localScale = ball.transform.localScale * field.WidthScale;

            //var playerMiddleSizeY = player.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
            //player.transform.localPosition = field.BottomCenter.WithAddY(playerMiddleSizeY);
            //player.xMin = field.BottomLeftCorner.x;
            //player.xMax = field.BottomRightCorner.x;
            //player.yPosition = field.BottomCenter.y;
            ////player.speed = player.speed * field.HeightScale;

            //var adversaryMiddleSizeY = adversary.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
            //adversary.transform.localPosition = field.TopCenter.WithSubY(adversaryMiddleSizeY);
            //adversary.xMin = field.TopLeftCorner.x;
            //adversary.xMax = field.TopRightCorner.x;
            //adversary.yPosition = field.TopCenter.y;
            ////adversary.speed = adversary.speed * field.HeightScale;

            //ball.transform.localPosition = field.Center;
            //ball.scale = field.WidthScale;
        }
    }
}
