using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_GameObject
    {
        #region SetActive Tools
        class ControlTempsMonoBehavior : MonoBehaviour { }
        static ControlTempsMonoBehavior controlTempsMonoBehavior;
        static void Init()
        {
            if (controlTempsMonoBehavior == null)
            {
                GameObject gameObject = new GameObject("ControlTempsMonoBehavior");
                controlTempsMonoBehavior = gameObject.AddComponent<ControlTempsMonoBehavior>();
            }
        }

        static IEnumerator SetActivaCorrutine(GameObject gameObject, bool value, WaitForSecondsRealtime waitForSeconds)
        {
            yield return waitForSeconds;
            gameObject.SetActive(value);
        }
        #endregion
        public static Coroutine SetActive(this GameObject gameObject, bool value, float temps)
        {
            Init();
            WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(temps);
            return controlTempsMonoBehavior.StartCoroutine(SetActivaCorrutine(gameObject, value, waitForSeconds));
        }
    }
}

