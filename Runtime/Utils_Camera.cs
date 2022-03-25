using UnityEngine;

namespace XS_Utils
{
    public static class MyCamera
    {
        static Camera camera;
        public static Camera Main
        {
            set => camera = value;
            get
            {
                if (camera == null) camera = Camera.main;
                return camera;
            }
        }
        public static Transform Transform => camera.transform;
    }
}
