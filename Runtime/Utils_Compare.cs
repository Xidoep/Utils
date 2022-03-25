using UnityEngine;

namespace XS_Utils
{
    public static class XS_Compare
    {
        /// <summary>
        /// Comprova si un float esta aprop d'un altre (dins un rang).
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="altre"></param>
        /// <param name="rang"></param>
        /// <returns></returns>
        public static bool IsNear(this float valor, float altre, float rang) => valor == Mathf.Clamp(valor, altre + rang, altre - rang);
        public static bool IsNear(this Vector3 valor, Vector3 altre, float rang) => Vector3.Distance(valor, altre) < rang;

    }
}

