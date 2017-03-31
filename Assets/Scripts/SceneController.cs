using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class SceneController : MonoBehaviour
    {
        public PlayerController player;
        public AdversaryController adversary;
        public BallController ball;
        public WallController leftWall;
        public WallController topWall;
        public WallController rightWall;
        public WallController bottomWall;
        public RectTransform rect;

        private Field field;
        private float _widthScale;
        private float _heightScale;

        private void Awake()
        {
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            field = new Field(rect, Camera.main);

            player.transform.localScale = player.transform.localScale * field.WidthScale;
            adversary.transform.localScale = adversary.transform.localScale * field.WidthScale;
            ball.transform.localScale = ball.transform.localScale * field.WidthScale;

            var playerMiddleSizeY = player.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
            player.transform.localPosition = field.BottomCenter.WithAddY(playerMiddleSizeY);
            player.xMin = field.BottomLeftCorner.x;
            player.xMax = field.BottomRightCorner.x;
            player.yPosition = field.BottomCenter.y;
            player.speed = player.speed * field.HeightScale;

            var adversaryMiddleSizeY = adversary.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
            adversary.transform.localPosition = field.TopCenter.WithSubY(adversaryMiddleSizeY);
            adversary.xMin = field.TopLeftCorner.x;
            adversary.xMax = field.TopRightCorner.x;
            adversary.yPosition = field.TopCenter.y;
            adversary.speed = adversary.speed * field.HeightScale;

            ball.transform.localPosition = field.Center;
            ball.scale = field.WidthScale;

            var wallSize = 50f;
            var midWallSize = wallSize / 2f;
            bottomWall.Init(new Vector2(field.Width, wallSize), field.BottomCenter.WithSubY(midWallSize));
            topWall.Init(new Vector2(field.Width, wallSize), field.TopCenter.WithAddY(midWallSize));
            leftWall.Init(new Vector2(wallSize, field.Height), field.LeftCenter.WithSubX(midWallSize));
            rightWall.Init(new Vector2(wallSize, field.Height), field.RightCenter.WithAddX(midWallSize));

            var playerCollider = player.GetComponent<Collider2D>();
            var adversaryCollider = adversary.GetComponent<Collider2D>();
            bottomWall.IgnoreCollisions(playerCollider, adversaryCollider);
            topWall.IgnoreCollisions(playerCollider, adversaryCollider);
            leftWall.IgnoreCollisions(playerCollider, adversaryCollider);
            rightWall.IgnoreCollisions(playerCollider, adversaryCollider);
        }
    }
}
