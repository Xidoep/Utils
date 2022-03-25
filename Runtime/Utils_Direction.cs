using UnityEngine;

namespace XS_Utils
{
    public static class XS_Direction
    {
        /// <summary>
        /// It gets the direction to a given direction relative to the given transform.
        /// Transform it to a Quaternion using ToQuaternion() function.
        /// </summary>
        public static Vector3 GetDirectionRelative(this Transform transform, Vector3 relativeDirection) => (transform.right * relativeDirection.x + transform.up * relativeDirection.y + transform.forward * relativeDirection.z).normalized;
        public static Vector3 GetDirectionRelative_Debug(this Transform transform, Vector3 relativeDirection)
        {
            Debugar.DrawRay(transform.position, (transform.right * relativeDirection.x + transform.up * relativeDirection.y + transform.forward * relativeDirection.z).normalized, Color.red);
            return transform.GetDirectionRelative(relativeDirection);
        }


        /// <summary>
        /// It gets the normalized direction to a given target.
        /// If you need it you can transform it to a Quaternion using ToQuaternion().
        /// </summary>
        public static Vector3 GetDirectionToTarget(this Transform transform, Transform target) => (target.position - transform.position).normalized;
        public static Vector3 GetDirectionToTarget_Debug(this Transform transform, Transform target)
        {
            Debugar.DrawRay(transform.position, (target.position - transform.position).normalized, Color.red);
            return transform.GetDirectionToTarget(target);
        }

        /// <summary>
        /// Gets the direction to a target smoothly.
        /// </summary>
        public static Vector3 GetDirectionToTargetSmooth(this Transform transform, Transform target, float speed) => Vector3.RotateTowards(transform.forward, transform.GetDirectionToTarget(target), speed * Time.deltaTime, speed * Time.deltaTime);
        public static Vector3 GetDirectionToTargetSmooth_Debug(this Transform transform, Transform target, float speed)
        {
            Debugar.DrawRay(transform.position, transform.GetDirectionToTarget(target), Color.green);
            Debugar.DrawRay(transform.position, Vector3.RotateTowards(transform.forward, transform.GetDirectionToTarget(target), speed * Time.deltaTime, speed * Time.deltaTime), Color.yellow);

            return transform.GetDirectionToTargetSmooth(target, speed);
        }


        public static Vector3 GetDirectionAbsolute(this Transform transform, Vector3 position) => (position - transform.position).normalized;
        public static Vector3 GetDirectionAbsoluteSmooth(this Transform transform, Vector3 direccio, float speed) => Vector3.RotateTowards(transform.forward, direccio.normalized, speed * Time.deltaTime, speed * Time.deltaTime);
        public static Vector3 GetDirectionAbsoluteSmooth_Debug(this Transform transform, Vector3 direccio, float speed)
        {
            Debugar.DrawRay(transform.position, direccio, Color.green);
            Debugar.DrawRay(transform.position, Vector3.RotateTowards(transform.forward, direccio.normalized, speed * Time.deltaTime, speed * Time.deltaTime), Color.yellow);

            return transform.GetDirectionAbsoluteSmooth(direccio, speed);
        }

        //Canviar, no es a camara, es a un objecte qualsevol.
        public static Vector3 ACamara(GameObject camara) => camara.transform.forward;

        public static Vector3 ACamara()
        {
            return Camera.main != null ? Camera.main.transform.forward : Vector3.zero;
        }

        public static Vector3 ACamaraRelatiu(this Transform camara, Vector2 direccio)
        {
            Vector3 _forward = camara.forward;
            _forward.y = 0f;
            _forward.Normalize();

            return _forward * direccio.y + camara.right * direccio.x;
        }
    }

}
