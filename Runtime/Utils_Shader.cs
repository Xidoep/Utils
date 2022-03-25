using UnityEngine;

namespace XS_Utils
{
    public static class XS_Shader
    {
        public static void SetGlobal(this Vector4 vector, string propietat) => Shader.SetGlobalVector(propietat, vector);
        public static void SetGlobal(this Vector3 vector, string propietat) => Shader.SetGlobalVector(propietat, vector);
        public static void SetGlobal(this Vector2 vector, string propietat) => Shader.SetGlobalVector(propietat, vector);
        public static void SetGlobal(this float vector, string propietat) => Shader.SetGlobalFloat(propietat, vector);
        public static void SetGlobal(this int vector, string propietat) => Shader.SetGlobalInt(propietat, vector);
        public static void SetGlobal(this Color color, string propietat) => Shader.SetGlobalColor(propietat, color);
        public static Vector4 GetGlobalVector(string propietat) => Shader.GetGlobalVector(propietat);
        public static Vector4 GetlgobalColor(string propietat) => Shader.GetGlobalColor(propietat);

    }
}