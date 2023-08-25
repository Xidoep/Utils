using System;
using System.Collections;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_Coroutine
    {
        class CorrutinaEstaticaMonoBehavior : MonoBehaviour
        {
            private void OnDisable() => Destroy(this.gameObject);
        }
        static CorrutinaEstaticaMonoBehavior corrutinaEstaticaMonoBehavior;
        static void Init()
        {
            if (corrutinaEstaticaMonoBehavior == null)
            {
                GameObject gameObject = new GameObject("CorrutinaEstatica");
                corrutinaEstaticaMonoBehavior = gameObject.AddComponent<CorrutinaEstaticaMonoBehavior>();
            }
        }
        static WaitForSecondsRealtime waitForSecondsRealtime;
        static WaitForSeconds waitForSeconds;



        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(coroutine);
        }
        /// <summary>
        /// Starts ans infinite loop with a function that calls every frame.
        /// </summary>
        public static Coroutine StartCoroutine_Update(Action update)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopCondition_Update(InfiniteLoop, update));
        }
        /// <summary>
        /// Stats an infonite loop with a function that calls every fixed update
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public static Coroutine StartCoroutine_FixedUpdate(Action update)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopCondition_Update(InfiniteLoop, update));
        }


        /// <summary>
        /// Starts a loop with a function as an exit condition and with a function that calls every frame.
        /// </summary>
        public static Coroutine StartCoroutine_Update(Func<bool> sortida, Action update)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopCondition_Update(sortida, update));
        }
        /// <summary>
        /// Starts a loop with a function as an exit condition and with a function that calls at the end.
        /// </summary>
        public static Coroutine StartCoroutine_Ending(Func<bool> sortida, Action ending)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopCondition_Ending(sortida, ending));
        }
        /// <summary>
        /// Works like a regular corrutine with a function that calls at the end.
        /// </summary>
        public static Coroutine StartCoroutine_Ending(float time, Action ending)
        {
            Init();
            waitForSecondsRealtime = new WaitForSecondsRealtime(time);
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopTime(ending));
        }
        public static Coroutine StartCoroutine_Ending<T>(float time, Action<T> ending, T arg)
        {
            Init();
            waitForSecondsRealtime = new WaitForSecondsRealtime(time);
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopTime_FrameDependant(ending, arg));
        }
        public static Coroutine StartCoroutine_Ending_FrameDependant(float time, Action ending)
        {
            Init();
            waitForSeconds = new WaitForSeconds(time);
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopTime_FrameDependant(ending));
        }
        public static Coroutine StartCoroutine_Ending_FrameDependant<T>(float time, Action<T> ending, T arg)
        {
            Init();
            waitForSeconds = new WaitForSeconds(time);
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopTime_FrameDependant(ending, arg));
        }
        public static Coroutine StartCoroutine_EndFrame(Action ending)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopEndOfFrame(ending));
        }


        static IEnumerator LoopCondition_Update(Func<bool> sortida, Action update)
        {
            while (!sortida.Invoke())
            {
                update.Invoke();
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
        static IEnumerator LoopCondition_FixedUpdate(Func<bool> sortida, Action update)
        {
            while (!sortida.Invoke())
            {
                update.Invoke();
                yield return new WaitForFixedUpdate();
            }
            yield return null;
        }
        static IEnumerator LoopCondition_Ending(Func<bool> sortida, Action ending)
        {
            while (!sortida.Invoke())
            {
                yield return new WaitForEndOfFrame();
            }
            ending.Invoke();
            yield return null;
        }
        static IEnumerator LoopTime(Action ending)
        {
            yield return waitForSecondsRealtime;
            ending.Invoke();
        }
        static IEnumerator LoopTime_FrameDependant(Action ending)
        {
            yield return waitForSeconds;
            ending.Invoke();
        }
        static IEnumerator LoopTime_FrameDependant<T>(Action<T> ending, T arg)
        {
            yield return waitForSeconds;
            ending.Invoke(arg);
        }
        static IEnumerator LoopEndOfFrame(Action ending)
        {
            yield return new WaitForEndOfFrame();
            ending.Invoke();
        }


        public static void StopCoroutine(this Coroutine coroutine, bool destroyCoroutine = true)
        {
            if (coroutine == null)
                return;

            corrutinaEstaticaMonoBehavior.StopCoroutine(coroutine);

            if (destroyCoroutine) coroutine = null;
        }

        static bool InfiniteLoop() => false;
    }

}
