using UnityEngine;

namespace XS_Utils
{
    public static class XS_Physics
    {
        static RaycastHit hit;
        static Collider[] results;

        public static RaycastHit RayDebug(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask, float temps = 0)
        {
            if (Physics.Raycast(origin, direction, out hit, distance, layerMask))
            {
                Debugar.DrawRay(origin, (direction).normalized * distance, Color.green, temps);
            }
            else
            {
                Debugar.DrawRay(origin, (direction).normalized * distance, Color.red, temps);
            }
            return hit;
        }
        public static RaycastHit Ray(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask)
        {
            Physics.Raycast(origin, direction, out hit, distance, layerMask);
            return hit;
        }

        public static RaycastHit RaySphereDebug(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask, float radius, float temps = 0)
        {
            if (Physics.SphereCast(origin, radius, direction, out hit, distance, layerMask))
            {
                Debugar.DrawRay(origin, (direction).normalized * distance, Color.green, temps);
            }
            else
            {
                Debugar.DrawRay(origin, (direction).normalized * distance, Color.red, temps);
            }
            return hit;
        }
        public static RaycastHit RaySphere(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask, float radius)
        {
            Physics.SphereCast(origin, radius, direction, out hit, distance, layerMask);
            return hit;
        }

        public static bool Hitted(this RaycastHit raycastHit) => raycastHit.collider != null;

        public static float RayDistance(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask, float temps = 0)
        {
            if (RayDebug(origin, direction, distance, layerMask).Hitted())
            {
                return Vector3.Distance(origin, RayDebug(origin, direction, distance, layerMask).point);
            }
            else
            {
                return distance;
            }
        }

        public static bool IsAtDirectionOfTarget(this Rigidbody rigidbody, Collider me)
        {
            //Debugar.DrawRay(rigidbody.position, rigidbody.velocity.normalized, Color.red);
            //Debugar.DrawRay(rigidbody.position, rigidbody.transform.GetDirectionToTarget(target.transform), Color.blue);
            Debugar.DrawLine(rigidbody.position, me.ClosestPoint(rigidbody.position), Color.yellow);
            //me.ClosestPoint(transform.position);
            return Vector3.Dot(rigidbody.transform.GetDirectionAbsolute(me.ClosestPoint(rigidbody.position)), rigidbody.linearVelocity.normalized) > 0.1f;
        }
        //public static bool IsAt





        public static Collider[] CollidersBox(Vector3 centre, Vector3 tamany, Quaternion orientacio, LayerMask layerMask)
        {
            return Physics.OverlapBox(centre, tamany / 2f, orientacio, layerMask);
        }
        public static bool Impactat(this Collider[] colliders) => colliders.Length > 0;
        public static bool ColisionatBox(Vector3 centre, Vector3 tamany, Quaternion orientacio, LayerMask layerMask)
        {
            if (results == null) results = new Collider[10];

            return Physics.OverlapBoxNonAlloc(centre, tamany / 2f, results, orientacio, layerMask) > 0;
        }
        public static Collider[] CollidersSphere(Vector3 centre, float radi)
        {
            return Physics.OverlapSphere(centre, radi);
        }

        public static bool Capsule(Vector3 point1, Vector3 point2, float radius, LayerMask layerMask)
        {
            if (results == null) results = new Collider[10];
            return Physics.OverlapCapsuleNonAlloc(point1, point2, radius, results, layerMask) > 0;
        }

        public static int Capsule(ref Collider[] colliders, Vector3 point1, Vector3 point2, float radius, LayerMask layerMask)
        {
            return Physics.OverlapCapsuleNonAlloc(point1, point2, radius, colliders, layerMask);
        }
    }
}