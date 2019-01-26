using UnityEngine;

namespace Assets.Scripts.Cam
{
    [ExecuteInEditMode]
    public class CameraController : MonoBehaviour
    {
        public Camera Camera;
        public SpriteRenderer Background;

        private void Update()
        {
            SetCamera();
        }

        public void SetCamera()
        {
            if (Background == null || Camera == null)
                return;
            var backgroundSize = Background.bounds.size;
            var cameraAspect = Camera.aspect;
            var backgroundAspect = backgroundSize.x / backgroundSize.y;

            float targetHeight;
            if (cameraAspect < backgroundAspect)
            {
                //Width
                targetHeight = backgroundSize.x / cameraAspect;

            }
            else
            {

                //Height
                targetHeight = backgroundSize.y;

            }



            Camera.orthographicSize = targetHeight / 2f;
        }
    }
}
