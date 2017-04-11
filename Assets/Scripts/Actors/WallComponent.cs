using UnityEngine;

namespace TennisGame.Actors
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class WallComponent: MonoBehaviour
    {
        private BoxCollider2D selfCollider;

        private void Awake()
        {
            selfCollider = GetComponent<BoxCollider2D>();
            if (selfCollider == null)
                throw new UnassignedReferenceException("BoxCollider2D doesn't set.");
        }
    }
}
