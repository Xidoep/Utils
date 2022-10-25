using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    public static class Debugar
    {
        /// <summary>
        /// Escriu un Log a la consola que no s'escriurà fora de l'editor. 
        /// Evitant problemes de rendiment per culpa dels logs durant el desenvolupament.
        /// </summary>
        /// <param name="missatge"></param>
        public static void Log(string missatge)
        {
#if UNITY_EDITOR
            Debug.Log(missatge);
#else
            if (Debug.isDebugBuild)
            {
                Debug.Log(missatge);
            }
#endif
        }
        public static void Log(object missatge) => Log(missatge.ToString());
        public static void LogError(object missatge) => LogError(missatge.ToString());
        public static void LogError(string missatge)
        {
#if UNITY_EDITOR
            Debug.LogError(missatge);
#else
            if (Debug.isDebugBuild)
            {
                Debug.LogError(missatge);
            }
#endif
        }
        public static void LogError(string missatge, UnityEngine.Object context)
        {
#if UNITY_EDITOR
            Debug.LogError(missatge, context);
#else
            if (Debug.isDebugBuild)
            {
                Debug.LogError(missatge, context);
            }
#endif
        }

        public static void DrawRay(Vector3 start, Vector3 dir)
        {
#if UNITY_EDITOR
            Debug.DrawRay(start, dir);
#endif
        }
        public static void DrawRay(Vector3 start, Vector3 dir, Color color)
        {
#if UNITY_EDITOR
            Debug.DrawRay(start, dir, color);
#endif
        }
        public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
        {
#if UNITY_EDITOR
            Debug.DrawRay(start, dir, color, duration);
#endif
        }

        public static void DrawLine(Vector3 inici, Vector3 final)
        {
#if UNITY_EDITOR
            Debug.DrawLine(inici, final);
#endif
        }
        public static void DrawLine(Vector3 inici, Vector3 final, Color color)
        {
#if UNITY_EDITOR
            Debug.DrawLine(inici, final, color);
#endif
        }

        static GameObject primitive;
        public static void Primitive(PrimitiveType primitiveType, Vector3 position, Quaternion rotation, Vector3 scale, Transform parent, float time = 0)
        {
#if UNITY_EDITOR
            primitive = GameObject.CreatePrimitive(primitiveType);
            primitive.transform.SetTransform(position, rotation.eulerAngles, scale, parent);
            if (time > 0) GameObject.Destroy(primitive, time);
#endif
        }
        public static void Primitive(PrimitiveType primitiveType, Vector3 position, float time = 0) => Primitive(primitiveType, position, Quaternion.identity, Vector3.one, null, time);
        public static void Primitive(PrimitiveType primitiveType, Vector3 position, Vector3 scale, float time = 0) => Primitive(primitiveType, position, Quaternion.identity, scale, null, time);
        public static void Primitive(PrimitiveType primitiveType, Vector3 position, Quaternion rotation, float time = 0) => Primitive(primitiveType, position, rotation, Vector3.one, null, time);
        public static void Primitive(PrimitiveType primitiveType, Vector3 position, Quaternion rotation, Vector3 scale, float time = 0) => Primitive(primitiveType, position, rotation, scale, null, time);
        public static void Primitive(PrimitiveType primitiveType, Vector3 position, Quaternion rotation, Transform parent, float time = 0) => Primitive(primitiveType, position, rotation, Vector3.one, parent, time);
        public static void Primitive(PrimitiveType primitiveType, Vector3 position, Transform parent, float time = 0) => Primitive(primitiveType, position, Quaternion.identity, Vector3.one, parent, time);
        public static void Primitive(PrimitiveType primitiveType, Transform transform, Transform parent, float time = 0) => Primitive(primitiveType, transform.position, transform.rotation, Vector3.one, parent, time);
        public static void Primitive(PrimitiveType primitiveType, Transform transform, float time = 0) => Primitive(primitiveType, transform.position, transform.rotation, Vector3.one, null, time);

        static TextMesh textMesh;
        public static TextMesh FloatingText(this string text, Vector3 position, float time = 0)
        {
#if UNITY_EDITOR
            textMesh = new GameObject(text, new Type[] { typeof(TextMesh), typeof(Utils_MirarCamara) }).GetComponent<TextMesh>();
            textMesh.transform.position = position;
            textMesh.text = text;
            if (time > 0) GameObject.Destroy(textMesh.gameObject, time);
#endif
            return textMesh;
        }
        public static void FloatingText(this string text, Transform transform, float time = 0) => text.FloatingText(transform.position, time);


        #region VisualitzarCollider_Utils
        static void Linia(Transform transform, Vector3 tamany, Vector3 inici, Vector3 final)
        {
#if UNITY_EDITOR
            Debug.DrawLine(
                transform.position + new Vector3(inici.x * tamany.x, inici.y * tamany.y, inici.z * tamany.z),
                transform.position + new Vector3(final.x * tamany.x, final.y * tamany.y, final.z * tamany.z),
                Color.red);
#endif
        }
        #endregion
        /// <summary>
        /// Dibuixa un cub a partir d'un transform un un tamany.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="tamany"></param>
        public static void VisualitzarCollider(Transform transform, Vector3 tamany)
        {
#if UNITY_EDITOR
            Linia(transform, tamany, transform.right + transform.up + transform.forward, transform.right + transform.up - transform.forward);
            Linia(transform, tamany, transform.right - transform.up + transform.forward, transform.right - transform.up - transform.forward);
            Linia(transform, tamany, -transform.right + transform.up + transform.forward, -transform.right + transform.up - transform.forward);
            Linia(transform, tamany, -transform.right - transform.up + transform.forward, -transform.right - transform.up - transform.forward);

            Linia(transform, tamany, transform.right + transform.up + transform.forward, -transform.right + transform.up + transform.forward);
            Linia(transform, tamany, transform.right + transform.up + transform.forward, transform.right - transform.up + transform.forward);
            Linia(transform, tamany, -transform.right - transform.up + transform.forward, transform.right - transform.up + transform.forward);
            Linia(transform, tamany, -transform.right - transform.up + transform.forward, -transform.right + transform.up + transform.forward);

            Linia(transform, tamany, transform.right + transform.up - transform.forward, -transform.right + transform.up - transform.forward);
            Linia(transform, tamany, transform.right + transform.up - transform.forward, transform.right - transform.up - transform.forward);
            Linia(transform, tamany, -transform.right - transform.up - transform.forward, transform.right - transform.up - transform.forward);
            Linia(transform, tamany, -transform.right - transform.up - transform.forward, -transform.right + transform.up - transform.forward);
#endif
        }
    }
}

