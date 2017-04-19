using UnityEngine;
using TennisGame.Actors;
using TennisGame.Core;

namespace TennisGame.Game
{
    public class MainSceneControllerComponent : MonoBehaviour, IGameController
    {
        [SerializeField]
        private ActorSet actors;
        [SerializeField]
        private Canvas fieldCanvas;
        [SerializeField]
        private RectTransform fieldRect;
        [SerializeField]
        private Camera fieldCamera;
        [SerializeField]
        private float wallWidth = 100f;
        [SerializeField]
        private float platformSpeed = 300f;
        [SerializeField]
        private float ballSpeed = 300f;
        private Field field;

        public RaycastHit2D[] Hits
        {
            get { return actors.Ball.Hits; }
        }

        public bool IsItSelf(GameObject obj, GameObject target)
        {
            return obj.GetTransformInstanceID() == target.GetTransformInstanceID();
        }

        public bool IsSelfWall(GameObject obj, GameObject target)
        {
            if (obj.GetTransformInstanceID() == actors.TopPlatform.GetTransformInstanceID())
            {
                return target.GetTransformInstanceID() == actors.WallSet.TopWall.GetTransformInstanceID();
            }
            else if (obj.GetTransformInstanceID() == actors.BottomPlatform.GetTransformInstanceID())
            {
                return target.GetTransformInstanceID() == actors.WallSet.BottomWall.GetTransformInstanceID();
            }
            else
            {
                return false;
            }
        }

        public bool IsOppositeWall(GameObject obj, GameObject target)
        {
            if (obj.GetTransformInstanceID() == actors.TopPlatform.GetTransformInstanceID())
            {
                return target.GetTransformInstanceID() == actors.WallSet.BottomWall.GetTransformInstanceID();
            }
            else if (obj.GetTransformInstanceID() == actors.BottomPlatform.GetTransformInstanceID())
            {
                return target.GetTransformInstanceID() == actors.WallSet.TopWall.GetTransformInstanceID();
            }
            else
            {
                return false;
            }
        }

        private void Awake()
        {
        }

        private void Start()
        {
            InitComponents();
            StartPlay();
        }

        private void OnDestroy()
        {
            foreach (var actor in actors)
                actor.UnregisterGameController();
        }

        private void InitComponents()
        {
            field = new Field(fieldCanvas, fieldRect, fieldCamera);

            actors.TopPlatform.MulLocalScale(field.WidthScale);
            actors.TopPlatform.SetLocalPosition(field.TopCenter.WithSubY(actors.TopPlatform.MiddleHeight));
            actors.TopPlatform.SetBorder(field.TopLeftCorner.x, field.TopRightCorner.x);
            actors.TopPlatform.Speed = platformSpeed;

            actors.BottomPlatform.MulLocalScale(field.WidthScale);
            actors.BottomPlatform.SetLocalPosition(field.BottomCenter.WithAddY(actors.BottomPlatform.MiddleHeight));
            actors.BottomPlatform.SetBorder(field.BottomLeftCorner.x, field.BottomRightCorner.x);
            actors.BottomPlatform.Speed = platformSpeed;

            actors.Ball.MulLocalScale(field.WidthScale);
            actors.Ball.SetLocalPosition(field.Center);
            actors.Ball.Speed = ballSpeed;

            var middleWallWidth = wallWidth / 2f;
            var doubleWallWidth = field.Width + 2 * wallWidth;

            actors.WallSet.TopWall.SetSize(new Vector2(doubleWallWidth, wallWidth));
            actors.WallSet.TopWall.SetLocalPosition(field.TopCenter.WithAddY(middleWallWidth));
            actors.WallSet.TopWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);

            actors.WallSet.BottomWall.SetSize(new Vector2(doubleWallWidth, wallWidth));
            actors.WallSet.BottomWall.SetLocalPosition(field.BottomCenter.WithSubY(middleWallWidth));
            actors.WallSet.BottomWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);

            actors.WallSet.LeftWall.SetSize(new Vector2(wallWidth, field.Height));
            actors.WallSet.LeftWall.SetLocalPosition(field.LeftCenter.WithSubX(middleWallWidth));
            actors.WallSet.LeftWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);

            actors.WallSet.RightWall.SetSize(new Vector2(wallWidth, field.Height));
            actors.WallSet.RightWall.SetLocalPosition(field.RightCenter.WithAddX(middleWallWidth));
            actors.WallSet.RightWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);
            
            foreach (var actor in actors)
                actor.RegisterGameController(this);
        }

        private void StartPlay()
        {
            actors.Ball.StartMotion(Vector2.down);
        }
    }
}
