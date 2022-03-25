using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_Movement
    {
        /// <summary>
        /// Move the given transform to an absolute direction
        /// </summary>
        public static void MoveToDirection(this Transform transform, Vector3 direction, float speed) => transform.localPosition += direction * speed;

        /// <summary>
        /// Move the given transform to an direction, relative to actual heading of the object.
        /// </summary>
        public static void MoveToRelativeDirection(this Transform transform, Vector3 direction, float speed) => transform.localPosition += transform.GetDirectionRelative(direction) * speed;

        /// <summary>
        /// Move the given transform to a target on the world.
        /// </summary>
        public static void MoveToTarget(this Transform transform, Transform objectiu, float speed) => transform.localPosition += transform.GetDirectionToTarget(objectiu) * speed;
    }
}
