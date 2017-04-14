using UnityEngine;
using TennisGame.Game;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class WallComponent: MonoBehaviour, IActor
    {
        private IGameController gamecontroller;
        private BoxCollider2D selfCollider;
        private IGameController gameController;

        public void SetSize(Vector2 size)
        {
            selfCollider.size = size;
        }
        
        public void IgnoreCollision(Collider2D collider)
        {
            Physics2D.IgnoreCollision(selfCollider, collider);
        }

        public void IgnoreCollisions(params Collider2D[] colliders)
        {
            foreach (var col in colliders)
                Physics2D.IgnoreCollision(selfCollider, col);
        }

        public string ActorName
        {
            get { return gameObject.name; }
        }

        public void RegisterGameController(IGameController controller)
        {
            gameController = controller;
        }

        public void UnregisterGameController()
        {
            gameController = null;
        }

        private void Awake()
        {
            selfCollider = GetComponent<BoxCollider2D>();
            if (!selfCollider)
                throw new UnassignedReferenceException("BoxCollider2D doesn't set.");
        }
    }
}
