using UnityEngine;

namespace Game.Utils
{
    public static class CameraUtils
    {
        static CameraUtils()
        {
            MainCamera = Camera.main;
        }
        public static Camera MainCamera { get; private set; }
        
        public static void SetOrthographicSize(this Camera camera, Bounds targetBounds)
        {
            float screenRatio = Screen.width / (float)Screen.height;
            float targetRatio = targetBounds.size.x / targetBounds.size.y;
            if (screenRatio >= targetRatio)
            {
                camera.orthographicSize = targetBounds.size.y / 2;
            }
            else
            {
                float diffrences = targetRatio / screenRatio;
                camera.orthographicSize = targetBounds.size.y / 2 * diffrences;
            }

            camera.transform.position =
                new Vector3(targetBounds.center.x, targetBounds.center.y, camera.transform.position.z);
        }
    }
}
