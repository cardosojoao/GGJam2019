using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class CharacterPerspController : MonoBehaviour
    {
        public const string CHARACTER_TAG = "Character";

        private static Dictionary<string, Vector2> _directionVector = new Dictionary<string, Vector2>
        {
            { "Vertical", new Vector2(0,1) },
            { "Horizontal", new Vector2(1,0) },
        };

        public Transform CharacterTransform;
        public Collider2D CharacterBox;
        public Animator CharacterAnimator;
        public Vector3 LowerLeftBoundarie { get { return CharacterBox.bounds.min; } }
        public Vector3 LowerRightBoundarie
        {
            get
            {
                var min = CharacterBox.bounds.min;
                var max = CharacterBox.bounds.max;
                return new Vector2(max.x, min.y);
            }
        }

        public Collider2D ScenarioBoundaries;
        public SpriteRenderer SpriteRenderer;
        public float PerspectiveAngle = 45f;
        public float Speed = 10.0f;

        public float MinZ = 0f;
        public float MaxZ = 10f;

        private void Update()
        {
            Vector2 directionVector = Vector2.zero;
            foreach (KeyValuePair<string, Vector2> direction in _directionVector)
            {
                var axisInput = Input.GetAxisRaw(direction.Key);
                directionVector += axisInput * direction.Value;
            }

            Move(directionVector);
            SetSprite(directionVector);
            SetAnimation(directionVector);
        }

        private void SetAnimation(Vector2 directionVector)
        {
            CharacterAnimator.SetBool("Walking", directionVector != Vector2.zero);
        }

        private void SetSprite(Vector2 directionVector)
        {
            if (directionVector.x == 0)
                return;
            if (directionVector.x > 0)
                SpriteRenderer.flipX = false;
            else
                SpriteRenderer.flipX = true;
        }

        private void Move(Vector3 directionVector)
        {
            if (directionVector == Vector3.zero)
                return;

            var currentPosition = CharacterTransform.position;
            var zPosition = currentPosition.z;
            var movement = Time.deltaTime * Speed;
            var movementVector = directionVector * movement;
            currentPosition += movementVector;
            currentPosition = BoundaryFix(currentPosition, movementVector);

            var scenarioBounds = ScenarioBoundaries.bounds;
            var zDelta = (CharacterBox.bounds.min.y - scenarioBounds.min.y) / (scenarioBounds.max.y - scenarioBounds.min.y);
            currentPosition.z = Mathf.Lerp(MinZ, MaxZ, zDelta);
            CharacterTransform.position = currentPosition;
        }

        private Vector2 BoundaryFix(Vector2 position, Vector2 movementVector)
        {
            Vector2 llb = LowerLeftBoundarie;
            llb += movementVector;
            Vector2 lrb = LowerRightBoundarie;
            lrb += movementVector;

            var scenarioBounds = ScenarioBoundaries.bounds;

            var min = scenarioBounds.min;
            var max = scenarioBounds.max;
            var boundHeight = max.y - min.y;

            var tan = Mathf.Tan(PerspectiveAngle * Mathf.Deg2Rad);
            var boundX = boundHeight / tan;
            var leftXEdge = min.x + boundX;
            var rightXEdge = max.x - boundX;
            var extentX = CharacterBox.bounds.extents.x;
            var extentY = CharacterBox.bounds.extents.y;


            if (llb.x < leftXEdge && llb.y - min.y > tan * (llb.x - min.x))
            {
                var xEdge = ((llb.y - min.y) / tan) + min.x;
                position.x = Mathf.Max(position.x, xEdge + extentX);
            }
            else if (lrb.x > rightXEdge && lrb.y - min.y > tan * -(lrb.x - max.x))
            {
                var xEdge = max.x - ((lrb.y - min.y) / tan);
                position.x = Mathf.Min(position.x, xEdge - extentX);
            }

            position.y = Mathf.Clamp(position.y, min.y + extentY, max.y + extentY);

            return position;
        }
    }
}
