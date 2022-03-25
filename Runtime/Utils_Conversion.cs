using System;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_Conversion
    {
        static List<Color> colors;

        /// <summary>
        /// Retorna (X,Z);
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Vector3 vector3) => new Vector2(vector3.x, vector3.z);

        /// <summary>
        /// Retorna (X,0,Y)
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static Vector3 ToVector3_Pla(this Vector2 vector2) => new Vector3(vector2.x, 0, vector2.y);

        /// <summary>
        /// Retorna (X,Y,0)
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        public static Vector3 ToVector3_Vertical(this Vector2 vector2) => new Vector3(vector2.x, vector2.y, 0);

        /// <summary>
        /// Retorna Quaternion
        /// </summary>
        /// <param name="direccio"></param>
        /// <param name="upwards"></param>
        /// <returns></returns>
        public static Quaternion ToQuaternion(this Vector3 direccio, Vector3 upwards) => Quaternion.LookRotation(direccio, upwards);
        public static Quaternion ToQuaternion(this Vector3 direccio) => Quaternion.LookRotation(direccio);

        public static float ToFloat(this string s)
        {
            if (float.TryParse(s.Replace('.', ','), out float result)) return result;
            else return 000111000f;
        }

        public static int ToInt(this string i)
        {
            if (int.TryParse(i, out int result)) return result;
            else return 000111000;
        }

        /// <summary>
        /// Return Color from Vector3 with alfa = 1
        /// </summary>
        /// <param name="vector3"></param>
        /// <returns></returns>
        public static Color ToColor(this Vector3 vector3) => vector3.ToColor(1);

        /// <summary>
        /// Return Color fomr Vector3 plus a value for the Alfa.
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="alfa"></param>
        /// <returns></returns>
        public static Color ToColor(this Vector3 vector3, float alfa) => new Color(vector3.x, vector3.y, vector3.z, alfa);


        static System.Text.StringBuilder stringBuilder;

        public static string ToTime(this float seconds) => TimeSpan.FromSeconds((double)seconds).ToString("mm':'ss");
        public static string ToTime(this int seconds) => TimeSpan.FromSeconds((double)seconds).ToString("mm':'ss");
    }

}
