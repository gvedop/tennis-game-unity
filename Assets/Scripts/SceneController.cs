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
        public RectTransform field;

        private void Awake()
        {
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var cornersPosition = new Vector3[4];
            field.GetWorldCorners(cornersPosition);
            for (int i = 0; i < cornersPosition.Length; i++)
                cornersPosition[i] = Camera.main.ScreenToWorldPoint(cornersPosition[i]);

            var width = Mathf.Abs(cornersPosition[0].x - cornersPosition[3].x);
            var height = Mathf.Abs(cornersPosition[0].y - cornersPosition[1].y);
            var widthScale = width / Screen.width;
            var heightScale = height / Screen.height;

            player.transform.localScale = player.transform.localScale * widthScale;
            adversary.transform.localScale = adversary.transform.localScale * widthScale;
            ball.transform.localScale = ball.transform.localScale * widthScale;

            var playerMiddleSize = player.GetComponent<SpriteRenderer>().bounds.size / 2f;
            var playerPosition = new Vector3((cornersPosition[0].x + cornersPosition[3].x) / 2f, cornersPosition[0].y + playerMiddleSize.y, 0f);
            player.transform.localPosition = playerPosition;
            player.xMin = cornersPosition[0].x;
            player.xMax = cornersPosition[3].x;
            player.yPosition = playerPosition.y;
            player.speed = player.speed * heightScale;

            var adversaryMiddleSize = adversary.GetComponent<SpriteRenderer>().bounds.size / 2f;
            var adversaryPosition = new Vector3((cornersPosition[1].x + cornersPosition[2].x) / 2f, cornersPosition[1].y - adversaryMiddleSize.y, 0f);
            adversary.transform.localPosition = adversaryPosition;
            adversary.xMin = cornersPosition[1].x;
            adversary.xMax = cornersPosition[2].x;
            adversary.yPosition = adversaryPosition.y;
            adversary.speed = adversary.speed * heightScale;

            ball.transform.localPosition = new Vector3((cornersPosition[0].x + cornersPosition[3].x) / 2f, (cornersPosition[0].y + cornersPosition[1].y) / 2, 0f);
            ball.scale = widthScale;

            var wallSize = 50f;
            bottomWall.GetComponent<BoxCollider2D>().size = new Vector2(width, wallSize);
            bottomWall.transform.localPosition = new Vector3((cornersPosition[0].x + cornersPosition[3].x) / 2f, cornersPosition[0].y - wallSize / 2f, 0f);
            topWall.GetComponent<BoxCollider2D>().size = new Vector2(width, wallSize);
            topWall.transform.localPosition = new Vector3((cornersPosition[0].x + cornersPosition[3].x) / 2f, cornersPosition[1].y + wallSize / 2f, 0f);
            leftWall.GetComponent<BoxCollider2D>().size = new Vector2(wallSize, height);
            leftWall.transform.localPosition = new Vector3(cornersPosition[0].x - wallSize / 2f, (cornersPosition[0].y + cornersPosition[1].y) / 2, 0f);
            rightWall.GetComponent<BoxCollider2D>().size = new Vector2(wallSize, height);
            rightWall.transform.localPosition = new Vector3(cornersPosition[3].x + wallSize / 2f, (cornersPosition[0].y + cornersPosition[1].y) / 2, 0f);

            var playerCollider = player.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(leftWall.GetComponent<Collider2D>(), playerCollider);
            Physics2D.IgnoreCollision(topWall.GetComponent<Collider2D>(), playerCollider);
            Physics2D.IgnoreCollision(rightWall.GetComponent<Collider2D>(), playerCollider);
            Physics2D.IgnoreCollision(bottomWall.GetComponent<Collider2D>(), playerCollider);

            var adversaryCollider = adversary.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(leftWall.GetComponent<Collider2D>(), adversaryCollider);
            Physics2D.IgnoreCollision(topWall.GetComponent<Collider2D>(), adversaryCollider);
            Physics2D.IgnoreCollision(rightWall.GetComponent<Collider2D>(), adversaryCollider);
            Physics2D.IgnoreCollision(bottomWall.GetComponent<Collider2D>(), adversaryCollider);
        }
    }
}
