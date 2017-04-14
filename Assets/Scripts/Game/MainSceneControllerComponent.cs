using System;
using System.Collections.Generic;
using UnityEngine;
using TennisGame.Actors;
using TennisGame.Core;
using System.Collections;

namespace TennisGame.Game
{


    public class MainSceneControllerComponent : MonoBehaviour, IGameController
    {
        [SerializeField]
        private ActorSet actors;

        public Canvas FieldCanvas;
        public RectTransform FieldRect;
        public Camera FieldCamera;
        public float WallWidth = 100f;
        public float PlatformSpeed = 300f;
        public float BallSpeed = 300f;

        private Field field;

        private void Awake()
        {
        }

        private void Start()
        {
            InitComponents();
        }

        private void OnDestroy()
        {
        }

        private void InitComponents()
        {
            field = new Field(FieldCanvas, FieldRect, FieldCamera);

            actors.TopPlatform.MulLocalScale(field.WidthScale);
            actors.TopPlatform.SetLocalPosition(field.TopCenter.WithSubY(actors.TopPlatform.MiddleHeight));
            actors.TopPlatform.SetBorder(field.TopLeftCorner.x, field.TopRightCorner.x);
            actors.TopPlatform.Speed = PlatformSpeed;
            actors.TopPlatform.RegisterGameController(this);

            actors.BottomPlatform.MulLocalScale(field.WidthScale);
            actors.BottomPlatform.SetLocalPosition(field.BottomCenter.WithAddY(actors.BottomPlatform.MiddleHeight));
            actors.BottomPlatform.SetBorder(field.BottomLeftCorner.x, field.BottomRightCorner.x);
            actors.BottomPlatform.Speed = PlatformSpeed;
            actors.BottomPlatform.RegisterGameController(this);

            actors.Ball.MulLocalScale(field.WidthScale);
            actors.Ball.SetLocalPosition(field.Center);
            actors.Ball.Speed = BallSpeed;

            var middleWallWidth = WallWidth / 2f;
            var doubleWallWidth = field.Width + 2 * WallWidth;

            actors.WallSet.TopWall.SetSize(new Vector2(doubleWallWidth, WallWidth));
            actors.WallSet.TopWall.SetLocalPosition(field.TopCenter.WithAddY(middleWallWidth));
            actors.WallSet.TopWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);

            actors.WallSet.BottomWall.SetSize(new Vector2(doubleWallWidth, WallWidth));
            actors.WallSet.BottomWall.SetLocalPosition(field.BottomCenter.WithSubY(middleWallWidth));
            actors.WallSet.BottomWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);

            actors.WallSet.LeftWall.SetSize(new Vector2(WallWidth, field.Height));
            actors.WallSet.LeftWall.SetLocalPosition(field.LeftCenter.WithSubX(middleWallWidth));
            actors.WallSet.LeftWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);

            actors.WallSet.RightWall.SetSize(new Vector2(WallWidth, field.Height));
            actors.WallSet.RightWall.SetLocalPosition(field.RightCenter.WithAddX(middleWallWidth));
            actors.WallSet.RightWall.IgnoreCollisions(actors.TopPlatform.SelfCollider, actors.BottomPlatform.SelfCollider);

            foreach (var actor in actors)
                Debug.Log(actor.ActorName);
        }
    }
}
