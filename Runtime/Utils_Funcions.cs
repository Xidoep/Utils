using TMPro;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace XS_Utils
{
    public static class Instanciar
    {
        /// <summary>
        /// Crea una primitica Esfera
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="temps"></param>
        /// <returns></returns>
        public static GameObject Sphere(Transform transform, float temps = 0) => Sphere(transform, transform.localPosition, transform.localEulerAngles, Vector3.one, temps);
        public static GameObject Sphere(Vector3 localPosition, Vector3 localRotation, float temps = 0) => Sphere(null, localPosition, localRotation, Vector3.one, temps);
        public static GameObject Sphere(Transform parent, Vector3 localPosition, Vector3 localRotation, Vector3 localScale, float temps = 0)
        {
            GameObject _tmp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            _tmp.transform.SetTransform(parent, localPosition, localRotation, localScale);
            if (temps > 0) GameObject.Destroy(_tmp, temps);
            return _tmp;
        }

        /// <summary>
        /// Crea una primitiva Quad
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="localPosition"></param>
        /// <param name="localRotation"></param>
        /// <param name="localScale"></param>
        /// <param name="temps"></param>
        /// <returns></returns>
        public static GameObject Quad(Transform parent, Vector3 localPosition, Vector3 localRotation, Vector3 localScale, float temps = 0)
        {
            GameObject _tmp = GameObject.CreatePrimitive(PrimitiveType.Quad);
            _tmp.transform.SetTransform(parent, localPosition, localRotation, localScale);
            if (temps > 0) GameObject.Destroy(_tmp, temps);
            return _tmp;
        }

        /// <summary>
        /// Crea textMesh 
        /// </summary>
        /// <param name="texte"></param>
        /// <param name="parent"></param>
        /// <param name="localPosition"></param>
        /// <param name="localRotation"></param>
        /// <param name="localScale"></param>
        /// <param name="temps"></param>
        /// <returns></returns>
        public static TextMesh Texte(string texte, Transform parent, Vector3 localPosition, Vector3 localRotation, Vector3 localScale, float temps = 0)
        {
            TextMesh textMesh = new GameObject(texte, new System.Type[] { typeof(TextMesh) }).transform.SetTransform(parent, localPosition, localRotation, localScale).GetComponent<TextMesh>();
            textMesh.text = texte;
            if (temps > 0) GameObject.Destroy(textMesh.gameObject, temps);
            return textMesh;
        }

        /// <summary>
        /// Crea texte flotant que dura x segons
        /// </summary>
        /// <param name="texte"></param>
        /// <param name="parent"></param>
        /// <param name="localPosition"></param>
        /// <returns></returns>
        public static TextMeshPro TexteFlotant(string texte, Transform parent, Vector3 localPosition)
        {
            TextMeshPro textMeshPro = new GameObject(texte, new System.Type[] { typeof(TextMeshPro) }).GetComponent<TextMeshPro>();
            textMeshPro.transform.SetTransform(parent, localPosition, Direccio.ACamara(), Vector3.one);
            textMeshPro.text = texte;
            textMeshPro.alignment = TextAlignmentOptions.Center;
            return textMeshPro;
        }
        public static TextMeshPro TexteFlotant(string texte, Transform parent, Vector3 localPosition, float temps)
        {
            TextMeshPro textMeshPro = TexteFlotant(texte, parent, localPosition);
            GameObject.Destroy(textMeshPro.gameObject, temps);
            return textMeshPro;
        }
    }

    public static class Transformacions
    {
        /// <summary>
        /// Posiciona un transform a una posicio, rotacio, escalat i parent.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="parent"></param>
        /// <param name="localPosition"></param>
        /// <param name="localEulerAngles"></param>
        /// <param name="localScale"></param>
        /// <returns></returns>
        public static Transform SetTransform(this Transform transform, Transform parent, Vector3 localPosition, Vector3 localEulerAngles, Vector3 localScale)
        {
            transform.SetParent(parent);
            transform.localPosition = localPosition;
            transform.localEulerAngles = localEulerAngles;
            transform.localScale = localScale;
            return transform;
        }

        /// <summary>
        /// Iguala la posicio, rot, escalat d'un transform a un altre.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="altre"></param>
        /// <returns></returns>
        public static Transform Igualar(this Transform transform, Transform altre)
        {
            return transform.SetTransform(altre.parent, altre.localPosition, altre.localEulerAngles, altre.localScale);
        }
    }

    public static class Moviment
    {
        public static void MovimentDireccio(this Transform transform, Vector3 direccio, float velocitat) => transform.localPosition += direccio * velocitat;
        public static void MovimentDireccioRelativa(this Transform transform, Vector3 direccio, float velocitat) => transform.localPosition += transform.GetDireccioRelativa(direccio) * velocitat;
        public static void MovimentObjectiu(this Transform transform, Transform objectiu, float velocitat) => transform.localPosition += transform.GetDireccioObjectiu(objectiu) * velocitat;
    }

    public static class Orientacio
    {
        public static void MirarACamara(this Transform transform) => transform.forward = Direccio.ACamara();
        public static void MirarACamara(this Transform transform, GameObject camara) => transform.forward = Direccio.ACamara(camara);

        public static void MirarDireccio(this Transform transform, Vector3 direccio, Vector3 transformForward, float velocitat = 1) => transform.forward = Vector3.RotateTowards(transformForward, direccio.normalized, velocitat * Time.deltaTime, velocitat * Time.deltaTime);
        public static void MirarDireccioRelativa(this Transform transform, Vector3 direccio, Vector3 transformForward, float velocitat = 1, bool debug = false) => transform.forward = Vector3.RotateTowards(transformForward, transform.GetDireccioRelativa(direccio, debug), velocitat * Time.deltaTime, velocitat * Time.deltaTime);
        public static void MirarObjectiu(this Transform transform, Transform objectiu, Vector3 transformForward, float velocitat = 1, bool debug = false) => transform.forward = Vector3.RotateTowards(transformForward, transform.GetDireccioObjectiu(objectiu, debug), velocitat * Time.deltaTime, velocitat * Time.deltaTime);
        public static void MirarObjectiu(this Transform transform, Quaternion rotacio, float velocitat = 1) => transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacio, velocitat);
    }

    public static class Direccio
    {
        public static Vector3 GetDireccioRelativa(this Transform transform, Vector3 direccio, bool debug = false)
        {
            if (debug) Debugar.DrawRay(transform.position, (transform.right * direccio.x + transform.up * direccio.y + transform.forward * direccio.z).normalized, Color.red);
            return (transform.right * direccio.x + transform.up * direccio.y + transform.forward * direccio.z).normalized;
        }
        public static Quaternion GetDireccioRelativaToQuaterinon(this Transform transform, Vector3 direccio, bool debug = false) => GetDireccioRelativa(transform, direccio, debug).ToQuaternion();

        public static Vector3 GetDireccioObjectiu(this Transform transform, Transform objectiu, bool debug = false)
        {
            if (debug) Debugar.DrawRay(transform.position, (objectiu.position - transform.position).normalized, Color.red);
            return (objectiu.position - transform.position).normalized;
        }
        public static Quaternion GetDireccioObjectiuToQuaternion(this Transform transform, Transform objectiu, bool debug = false) => GetDireccioObjectiu(transform, objectiu, debug).ToQuaternion();

        public static Vector3 GetDireccio(this Transform transform, Vector3 direccio, float quantitat, bool debug = false)
        {
            if (debug)
            {
                Debugar.DrawRay(transform.position, direccio);
                Debugar.DrawRay(transform.position, Vector3.RotateTowards(transform.forward, direccio.normalized, quantitat * Time.deltaTime, quantitat * Time.deltaTime));
            }
            return Vector3.RotateTowards(transform.forward, direccio.normalized, quantitat * Time.deltaTime, quantitat * Time.deltaTime);
        }
        public static Quaternion GetDireccioToQuaternion(this Transform transform, Vector3 direccio, float quantitat, bool debug = false) => transform.GetDireccio(direccio, quantitat, debug).ToQuaternion();

        public static Vector3 ACamara(GameObject camara) => camara.transform.forward;

        public static Vector3 ACamara()
        {
            return Camera.main != null ? Camera.main.transform.forward : Vector3.zero;
        }
        
        public static Vector3 ACamaraRelatiu(this Transform camara, Vector2 direccio)
        {
            Vector3 _forward = camara.forward;
            _forward.y = 0f;
            _forward.Normalize();

            return _forward * direccio.y + camara.right * direccio.x;
        }
    }

    public static class Distancia
    {
        public static float GetDistancia(this Transform transform, Vector3 posicio, bool debug = false)
        {
            if (debug) Debugar.DrawLine(transform.position, posicio, Color.yellow);
            return (posicio - transform.position).magnitude;
        }
        public static float GetDistancia(this Transform transform, Transform altre, bool debug = false)
        {
            if (debug) Debugar.DrawLine(transform.position, altre.position, Color.yellow);
            return (altre.position - transform.position).magnitude;
        }
    }

    public static class Inputs
    {
    }

    public static class Mouse
    {
        /// <summary>
        /// Throw a ray from mouse position and return de Vector3 where it collides.
        /// </summary>
        /// <param name="debug">Instanciate a sphere where it collides</param>
        /// <returns></returns>
        public static Vector3 HitRaig_DesdeCamara(bool debug = false) => HitRaig_DesdeCamara(Camera.main, Capes.Everything, debug);
        public static Vector3 HitRaig_DesdeCamara(Camera camera, bool debug = false) => HitRaig_DesdeCamara(camera, Capes.Everything, debug);
        public static Vector3 HitRaig_DesdeCamara(Camera camera, LayerMask layerMask, bool debug = false)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 300, layerMask))
            {
                if (debug) Instanciar.Sphere(null, hit.point, Vector3.zero, Vector3.one * 0.1f);
                return hit.point;
            }
            else
            {
                if (debug) Instanciar.Sphere(null, camera.ScreenToWorldPoint(camera.ScreenPointToRay(Input.mousePosition).origin + Vector3.forward * 300), Vector3.zero, Vector3.one * 0.1f);
                return camera.ScreenToWorldPoint(camera.ScreenPointToRay(Input.mousePosition).origin + Vector3.forward * 300);
            }
        }

        public static GameObject HitRay_DesdeCamara(Camera camera, LayerMask layerMask, bool debug = false)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 300, layerMask))
            {
                if (debug) Instanciar.Sphere(null, hit.point, Vector3.zero, Vector3.one * 0.1f);
                return hit.collider.gameObject;
            }
            else
            {
                if (debug) Instanciar.Sphere(null, camera.ScreenToWorldPoint(camera.ScreenPointToRay(Input.mousePosition).origin + Vector3.forward * 300), Vector3.zero, Vector3.one * 0.1f);
                return null;
            }
        }
    }

    public static class Capes
    {
        public static LayerMask Everything => -1;
    }

    public static class GameObjects
    {
        #region SetActiva Utils
        class ControlTempsMonoBehavior : MonoBehaviour { }
        static ControlTempsMonoBehavior controlTempsMonoBehavior;
        static void Init()
        {
            if(controlTempsMonoBehavior == null)
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

    /// <summary>
    /// Non-static class made to have a "time" with all possible functionalities.
    /// It have to be initated with the builder, with a time and a funtions to call on end.
    /// </summary>
    public class Countdown
    {
        float time;
        Action onEnd;
        bool active;
        float currentTime;
        bool Ended => currentTime <= 0;

        public Countdown(float time, Action onEnd)
        {
            active = false;
            SetCurrentTime(time);
            this.time = time;
            this.onEnd = onEnd;
        }

        /// <summary>
        /// Sets the time if you want to set it after create it.
        /// </summary>
        void SetCurrentTime(float time)
        {
            currentTime = time;
        }


        /// <summary>
        /// Start the countdown
        /// </summary>
        public void Start()
        {
            active = true;
            SetCurrentTime(time);
        }

        /// <summary>
        /// Start the countdown setting the time before start.
        /// </summary>
        /// <param name="time"></param>
        public void Start(float time)
        {
            this.time = time;
            Start();
        }
        /// <summary>
        /// Activate the countdown without resestart the current time. 
        /// </summary>
        public void Continue()
        {
            active = true;
        }
        /// <summary>
        /// It must to be called every frame on yout Update main class, to be sure the countdown start right after it activates.
        /// </summary>
        public void Update()
        {
            if (!active)
                return;

            currentTime -= Time.unscaledDeltaTime;

            if (Ended) 
            {
                onEnd.Invoke();
                active = false;
            } 
        }
        /// <summary>
        /// Stopts the countdown
        /// </summary>
        public void Stop(bool restart = false)
        {
            active = false;
        }
    }

    public static class Corrutina
    {
        class CorrutinaEstaticaMonoBehavior : MonoBehaviour { }
        static CorrutinaEstaticaMonoBehavior corrutinaEstaticaMonoBehavior;
        static void Init()
        {
            if (corrutinaEstaticaMonoBehavior == null)
            {
                GameObject gameObject = new GameObject("CorrutinaEstatica");
                corrutinaEstaticaMonoBehavior = gameObject.AddComponent<CorrutinaEstaticaMonoBehavior>();
            }
        }

        public static Coroutine Iniciar(float temps, Action action)
        {
            Init();
            WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(temps);

            return corrutinaEstaticaMonoBehavior.StartCoroutine(FuncioFinalCorrutine(action, waitForSeconds));
        }

        static IEnumerator FuncioFinalCorrutine(Action action, WaitForSecondsRealtime waitForSeconds)
        {
            yield return waitForSeconds;
            action.Invoke();
        }

        public static void Aturar(this Coroutine coroutine)
        {
            Init();
            corrutinaEstaticaMonoBehavior.StopCoroutine(coroutine);
        }


        public static Coroutine While(Func<bool> sortida, Action update)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(WhileCorrutine(sortida, update));
        }
        static IEnumerator WhileCorrutine(Func<bool> sortida, Action update)
        {
            Debug.Log("ei");
            while (!sortida.Invoke())
            {
                Debug.Log("oho");
                update.Invoke();
                yield return new WaitForEndOfFrame();
            }
            update.Invoke();
            yield return null;
        }
    }

    public static class MyCamera
    {
        static Camera camera;
        public static Camera CameraMain 
        {
            set => camera = value;
            get 
            {
                if (camera == null) camera = Camera.main;
                return camera;
            }
        }
        public static Transform Transform => camera.transform;
    }

    public static class To
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

        /// <summary>
        /// Return a string in format 00:00 from an integrer.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToRellotge(this int number) => $"{(((int)number) / 60).ToString("00")}:{(((int)number)%60).ToString("00")}";
    }

    public static class Animacio
    {
        /// <summary>
        /// Retorna una extrapolacio de en quina direccio s'hauria d'orientar l'animacio, 
        /// en base al la direccio del moviment i la orientacio a la que mira. 
        /// (Especial per personatges que es poden moure i mirar en direccions diferents).
        /// </summary>
        /// <param name="transform">El transform que es mou, (només s'utilitza per treuren el transform.right)</param>
        /// <param name="orientacio">Cap a on mira</param>
        /// <param name="direccio">Cap a on es mou</param>
        /// <param name="debug">Debugs. blau = Direccio. Vermell = Orientacio, Groc = Animacio</param>
        /// <returns></returns>
        public static Vector2 ExtrapolarDireccioAnimacioSegonsOrientacio(this Transform transform, Vector2 orientacio, 
            Vector2 direccio, bool debug = false)
        {
            Vector2 _tmp;
            if (direccio != Vector2.zero)
            {
                _tmp = Vector3.forward.ToVector2() * (Vector3.Dot(orientacio, direccio)) +
                    Vector3.right.ToVector2() * (Vector3.Dot(transform.right.ToVector2(), direccio));
            }
            else
            {
                _tmp = new Vector2(0, 1);
            }


            if (debug)
            {
                Debugar.DrawRay(transform.position, direccio.ToVector3_Pla(), Color.blue, 0.01f);

                if (orientacio != Vector2.zero)
                {
                    Debugar.DrawRay(transform.position, orientacio.ToVector3_Pla(), Color.red, 0.01f);
                }

                Debugar.DrawRay(transform.position, _tmp.ToVector3_Pla(), Color.yellow, 0.01f);
            }

            return _tmp;

        }

        ///LLEGIR CUSTOM CURVE
        ///Si crees un parametre al animator amb el mateix nom que la corva, ho captura automaticament.
        ///Aixì ja ho pots agafar amb animator.GetFloat(nom);
    }

    public static class Particules
    {
        public static void Emetre(this ParticleSystem particleSystem, int count, float time)
        {
            //particleSystem.Params
            ParticleSystem.EmitParams emitParams =  new ParticleSystem.EmitParams();
            var mainModule = particleSystem.main;
            mainModule.startLifetime = time;
            particleSystem.Emit(emitParams, count);
        }
    }

    public static class Traduccio
    {
        /// <summary>
        /// Rep la traduccio.
        /// </summary>
        /// <param name="localizedString"></param>
        /// <param name="funcioAssincrone_EnCompletat"></param>
        //public static void GetTraduccio(this LocalizedString localizedString, Action<AsyncOperationHandle<string>> funcioAssincrone_EnCompletat) => 
        //    localizedString.GetLocalizedString().Completed += funcioAssincrone_EnCompletat;
    }


    //FALTA:
    //Colliders in sphere i box.
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
            if(Physics.SphereCast(origin, radius,direction, out hit, distance, layerMask))
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
            if (RayDebug(origin,direction,distance,layerMask).Hitted())
            {
                return Vector3.Distance(origin, RayDebug(origin, direction, distance, layerMask).point);
            }
            else
            {
                return distance;
            }
        }

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
    }

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

    public static class Web
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
            if(webRequestMonoBehaviour == null)
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
            Web.Get(url,
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

                            Web.GetTextura(item.Substring(1, item.IndexOf(JPG, StringComparison.Ordinal) + 3), (string error) =>
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
            Web.Get(url,
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

                            Web.GetTextura(item.Substring(1, item.IndexOf(WEBP) + 3), (string error) =>
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
                Debug.LogError(missatge);
            }
#endif
        }
        public static void Log(object missatge) => Log(missatge.ToString());

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

        #region VisualitzarCollider_Utils
        static void Linia(Transform transform, Vector3 tamany, Vector3 inici, Vector3 final)
        {
            Debug.DrawLine(
                transform.position + new Vector3(inici.x * tamany.x, inici.y * tamany.y, inici.z * tamany.z),
                transform.position + new Vector3(final.x * tamany.x, final.y * tamany.y, final.z * tamany.z),
                Color.red);
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

    public static class Shaders
    {
        public static void SetVector(string propietat, Vector4 vector) => Shader.SetGlobalVector(propietat, vector);
        public static Vector4 GetVector(string propietat) => Shader.GetGlobalVector(propietat);
        public static void SetColor(string propietat, Color color) => Shader.SetGlobalColor(propietat, color);
        public static void SetColor(string propietat, Vector4 vector) => Shader.SetGlobalColor(propietat, vector);
        public static Vector4 GetColor(string propietat) => Shader.GetGlobalColor(propietat);
    }


}