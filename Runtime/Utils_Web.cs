using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace XS_Utils
{
    public static class XS_Web
    {
        #region Get_Utils
        const string PNG = ".png";
        const string JPG = ".jpg";
        const string WEBP = ".webp";
        const string INICI_URL_IMATGE = "<img src=";

        class WebRequestMonoBehaviour : MonoBehaviour { }//Clase que deriva de monobehaviour per poder utiltizar corrutines aqui.
        static WebRequestMonoBehaviour webRequestMonoBehaviour; //Instancia de la class monobehaviour
        static void Init()//iniciar la classe, crea com un singleton.
        {
            if (webRequestMonoBehaviour == null)
            {
                GameObject gameObject = new GameObject("WebRequestMonoBehaviour");
                webRequestMonoBehaviour = gameObject.AddComponent<WebRequestMonoBehaviour>();
            }
        }

        public static void Get(string url, Action<string> onError, Action<string> onSuccess)
        {
            Init();
            webRequestMonoBehaviour.StartCoroutine(GetCorrutine(url, onError, onSuccess));
        }

        public static void GetTextura(string url, Action<string> onError, Action<Texture2D> onSuccess)
        {
            Init();
            webRequestMonoBehaviour.StartCoroutine(GetTextureCorrutine(url, onError, onSuccess));
        }

        static IEnumerator GetCorrutine(string url, Action<string> onError, Action<string> onSuccess)
        {
            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError || unityWebRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    onError(unityWebRequest.error);

                }
                else
                {
                    onSuccess(unityWebRequest.downloadHandler.text);

                }
            }
        }
        static IEnumerator GetTextureCorrutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
        {
            using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequest.result == UnityWebRequest.Result.ProtocolError || unityWebRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    onError(unityWebRequest.error);

                }
                else
                {
                    DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
                    onSuccess(downloadHandlerTexture?.texture);

                }
            }
        }
        static IEnumerator GetJPGsCorrutine(List<Sprite> sprites, Action enAcabat, Action<Sprite[]> enImatgesTrobades)
        {
            yield return new WaitForSeconds(0.5f);
            if (enAcabat != null) enAcabat.Invoke();
            Debugar.Log("He acabat");
            enImatgesTrobades.Invoke(sprites.ToArray());
        }
        static IEnumerator GetWEBPCorrutine(List<Sprite> sprites, Action enAcabat, Action<Sprite[]> enImatgesTrobades)
        {
            yield return new WaitForSeconds(0.5f);
            if (enAcabat != null) enAcabat.Invoke();
            Debugar.Log("He acabat");
            enImatgesTrobades.Invoke(sprites.ToArray());
        }
        #endregion


        public static void GetJPGs(string url, Action enAcabat, Action<Sprite[]> enImatgesTrobades)
        {
            List<Sprite> sprites = new List<Sprite>();
            XS_Web.Get(url,
                (string error) =>
                {
                    Debugar.Log("Error: " + error);
                }, (string text) =>
                {
                    Debugar.Log("Received: " + text);
                    foreach (var item in text.Split(new string[] { INICI_URL_IMATGE }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (item.IndexOf(JPG, StringComparison.Ordinal) > 0 && item.IndexOf(JPG, StringComparison.Ordinal) < 5000)
                        {
                            Debugar.Log(item.Length);
                            Debugar.Log(item.Substring(1, item.IndexOf(JPG, StringComparison.Ordinal)));

                            XS_Web.GetTextura(item.Substring(1, item.IndexOf(JPG, StringComparison.Ordinal) + 3), (string error) =>
                            {
                                Debugar.Log("Error: " + error);
                            }, texture2D =>
                            {
                                sprites.Add(Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f), 10));
                            });
                        }
                    }
                    Init();
                    webRequestMonoBehaviour.StartCoroutine(GetJPGsCorrutine(sprites, enAcabat, enImatgesTrobades));
                });
        }

        public static void GetWEBP(string url, Action enAcabat, Action<Sprite[]> enImatgesTrobades)
        {
            List<Sprite> sprites = new List<Sprite>();
            XS_Web.Get(url,
                (string error) =>
                {
                    Debugar.Log("Error: " + error);
                }, (string text) =>
                {
                    Debugar.Log("Received: " + text);
                    foreach (var item in text.Split(new string[] { INICI_URL_IMATGE }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (item.IndexOf(WEBP) > 0 && item.IndexOf(WEBP) < 5000)
                        {
                            Debugar.Log(item.Length);
                            Debugar.Log(item.Substring(1, item.IndexOf(WEBP)));

                            XS_Web.GetTextura(item.Substring(1, item.IndexOf(WEBP) + 3), (string error) =>
                            {
                                Debugar.Log("Error: " + error);
                            }, (Texture2D texture2D) =>
                            {
                                sprites.Add(Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(.5f, .5f), 10));
                            });
                        }
                    }
                    Init();
                    webRequestMonoBehaviour.StartCoroutine(GetWEBPCorrutine(sprites, enAcabat, enImatgesTrobades));
                });
        }
    }
}
