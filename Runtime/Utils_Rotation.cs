using UnityEngine;

namespace XS_Utils
{
    public static class XS_Rotation
    {
        /// <summary>
        /// It heads the forward axis of the given transform to the main camera.
        /// </summary>
        public static void LookAtCameraMain(this Transform transform) => transform.forward = XS_Direction.ACamara();

        /// <summary>
        /// It heads the forward axis of the given transform to the given gameObject.
        /// </summary>
        public static void LookAtTarget(this Transform transform, GameObject target) => transform.forward = XS_Direction.ACamara(target);

        /// <summary>
        /// Next 6 functions head the corresponding axis of the given transform to the given direction.
        /// </summary>
        public static void LookForwardAtDirection(this Transform transform, Vector3 direction) => transform.forward = direction.normalized;
        public static void LookBackwardAtDirection(this Transform transform, Vector3 direction) => transform.forward = -direction.normalized;
        public static void LookRightAtDirection(this Transform transform, Vector3 direction) => transform.right = direction.normalized;
        public static void LookLeftAtDirection(this Transform transform, Vector3 direction) => transform.right = -direction.normalized;
        public static void LookUpAtDirection(this Transform transform, Vector3 direction) => transform.up = direction.normalized;
        public static void LookDownAtDirection(this Transform transform, Vector3 direction) => transform.up = -direction.normalized;

        /// <summary>
        /// It heads the given transform smoothly to the given direction.
        /// </summary>
        public static void LookAtDirectionSmooth(this Transform transform, Vector3 directio, Vector3 head, float speed = 1) => transform.forward = Vector3.RotateTowards(head, directio.normalized, speed * Time.deltaTime, speed * Time.deltaTime);

        /// <summary>
        /// It heads the given transform smoothly to the given direction relative to the actual rotation of the transform.
        /// </summary>
        public static void LookAtRelativeDirectionSmooth(this Transform transform, Vector3 direccio, Vector3 head, float speed = 1, bool debug = false) => transform.forward = Vector3.RotateTowards(head, transform.GetDirectionRelative(direccio), speed * Time.deltaTime, speed * Time.deltaTime);

        /// <summary>
        /// It heads the given transform smoothly to a given target.
        /// </summary>
        public static void TookAtTargetSmooth(this Transform transform, Transform target, Vector3 head, float speed = 1, bool debug = false) => transform.forward = Vector3.RotateTowards(head, transform.GetDirectionToTarget(target), speed * Time.deltaTime, speed * Time.deltaTime);

        /// <summary>
        /// It smoothly rotates the given transform to math the given rotation
        /// </summary>
        public static void RotateToQuaternionSmooth(this Transform transform, Quaternion rotation, float speed = 1) => transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, speed);
    }
}
