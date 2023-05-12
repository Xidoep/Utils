using UnityEngine;

namespace XS_Utils
{
    public static class XS_Transform
    {
        static Transform _t;
        /// <summary>
        /// Setup the transform with given information.
        /// It's useful when you want to position a transform like when you Instantiate it, but you can't do it directly.
        /// </summary>

        public static Component SetTransform(this Component component, Vector3 localPosition, Vector3 localEulerAngles, Vector3 localScale, Component parent = null)
        {
            _t = component.transform;
            _t.SetParent(parent.transform);
            _t.localPosition = localPosition;
            _t.localEulerAngles = localEulerAngles;
            _t.localScale = localScale;
            return component;
        }
        public static Component SetTransform(this Component component, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Component parent = null)
        {
            _t = component.transform;
            _t.SetParent(parent.transform);
            _t.localPosition = localPosition;
            _t.localRotation = localRotation;
            _t.localScale = localScale;
            return component;
        }
        public static Component SetTransform(this Component component, Vector3 localPosition, Quaternion localRotation, Component parent = null)
        {
            _t = component.transform;
            _t.SetParent(parent.transform);
            _t.localPosition = localPosition;
            _t.localRotation = localRotation;
            return component;
        }
        public static Component SetTransform(this Component component, Vector3 localPosition, Vector3 localEulerAngles, Component parent = null)
        {
            _t = component.transform;
            _t.SetParent(parent.transform);
            _t.localPosition = localPosition;
            _t.localEulerAngles = localEulerAngles;
            return component;
        }
        /// <summary>
        /// Iguala la posicio, rot, escalat d'un transform a un altre.
        /// </summary>
        public static Transform Copiar(this Transform transform, Transform other)
        {
            return transform.SetTransform(other.localPosition, other.localEulerAngles, other.localScale, other.parent).transform;
        }



        public static float Distance(this Vector3 position, Vector3 posicio, bool debug = false)
        {
            if (debug) Debugar.DrawLine(position, posicio, Color.yellow);
            return (posicio - position).magnitude;
        }
        public static float Distance(this Transform transform, Vector3 posicio, bool debug = false)
        {
            if (debug) Debugar.DrawLine(transform.position, posicio, Color.yellow);
            return (posicio - transform.position).magnitude;
        }
        public static float Distance(this Transform transform, Transform altre, bool debug = false)
        {
            if (debug) Debugar.DrawLine(transform.position, altre.position, Color.yellow);
            return (altre.position - transform.position).magnitude;
        }
    }
}

