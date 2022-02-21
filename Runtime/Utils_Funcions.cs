using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Pool;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Settings;

namespace XS_Utils
{
    namespace XS_Singleton
    {
        public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
        {
            public static T Instance { get; private set; }
            protected virtual void Awake() => Instance = this as T;

            protected virtual void OnApplicationQuit()
            {
                Instance = null;
                Destroy(gameObject);
            }
        }
        public abstract class SingletonDontDestroyOnLoad<T> : Singleton<T> where T : MonoBehaviour
        {
            protected override void Awake()
            {
                if (Instance != null) Destroy(gameObject);
                DontDestroyOnLoad(gameObject);
                base.Awake();
            }
        }
    }
   


    public static class XS_Transform
    {
        /// <summary>
        /// Setup the transform with given information.
        /// It's useful when you want to position a transform like when you Instantiate it, but you can't do it directly.
        /// </summary>
        public static Transform SetTransform(this Transform transform, Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Transform parent = null)
        {
            transform.SetParent(parent);
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
            transform.localScale = localScale;
            return transform;
        }
        public static Transform SetTransform(this Transform transform, Vector3 localPosition, Vector3 localEulerAngles, Vector3 localScale, Transform parent = null)
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
        public static Transform Equalize(this Transform transform, Transform other)
        {
            return transform.SetTransform(other.localPosition, other.localEulerAngles, other.localScale, other.parent);
        }



        public static float Distance(this Vector3 position, Vector3 posicio, bool debug = false)
        {
            if (debug) Debugar.DrawLine(position, posicio, Color.yellow);
            return (posicio - position).magnitude;
        }
        public static float Distance(this Transform transform, Vector3 posicio, bool debug = false)
        {
            if (debug) Debugar.DrawLine(transform.position, posicio, Color.yellow);
            return (posicio - transform.position).magnitude;
        }
        public static float Distance(this Transform transform, Transform altre, bool debug = false)
        {
            if (debug) Debugar.DrawLine(transform.position, altre.position, Color.yellow);
            return (altre.position - transform.position).magnitude;
        }
    }


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

    public static class XS_Rotation
    {
        /// <summary>
        /// It heads the forward axis of the given transform to the main camera.
        /// </summary>
        public static void LookAtCameraMain(this Transform transform) => transform.forward = XS_Direction.ACamara();

        /// <summary>
        /// It heads the forward axis of the given transform to the given gameObject.
        /// </summary>
        public static void LookAtTarget(this Transform transform, GameObject target) => transform.forward = XS_Direction.ACamara(target);

        /// <summary>
        /// Next 6 functions head the corresponding axis of the given transform to the given direction.
        /// </summary>
        public static void LookForwardAtDirection(this Transform transform, Vector3 direction) => transform.forward = direction.normalized;
        public static void LookBackwardAtDirection(this Transform transform, Vector3 direction) => transform.forward = -direction.normalized;
        public static void LookRightAtDirection(this Transform transform, Vector3 direction) => transform.right = direction.normalized;
        public static void LookLeftAtDirection(this Transform transform, Vector3 direction) => transform.right = -direction.normalized;
        public static void LookUpAtDirection(this Transform transform, Vector3 direction) => transform.up = direction.normalized;
        public static void LookDownAtDirection(this Transform transform, Vector3 direction) => transform.up = -direction.normalized;

        /// <summary>
        /// It heads the given transform smoothly to the given direction.
        /// </summary>
        public static void LookAtDirectionSmooth(this Transform transform, Vector3 directio, Vector3 head, float speed = 1) => transform.forward = Vector3.RotateTowards(head, directio.normalized, speed * Time.deltaTime, speed * Time.deltaTime);
        
        /// <summary>
        /// It heads the given transform smoothly to the given direction relative to the actual rotation of the transform.
        /// </summary>
        public static void LookAtRelativeDirectionSmooth(this Transform transform, Vector3 direccio, Vector3 head, float speed = 1, bool debug = false) => transform.forward = Vector3.RotateTowards(head, transform.GetDirectionRelative(direccio), speed * Time.deltaTime, speed * Time.deltaTime);
        
        /// <summary>
        /// It heads the given transform smoothly to a given target.
        /// </summary>
        public static void TookAtTargetSmooth(this Transform transform, Transform target, Vector3 head, float speed = 1, bool debug = false) => transform.forward = Vector3.RotateTowards(head, transform.GetDirectionToTarget(target), speed * Time.deltaTime, speed * Time.deltaTime);
        
        /// <summary>
        /// It smoothly rotates the given transform to math the given rotation
        /// </summary>
        public static void RotateToQuaternionSmooth(this Transform transform, Quaternion rotation, float speed = 1) => transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, speed);
    }

    public static class XS_Direction
    {
        /// <summary>
        /// It gets the direction to a given direction relative to the given transform.
        /// Transform it to a Quaternion using ToQuaternion() function.
        /// </summary>
        public static Vector3 GetDirectionRelative(this Transform transform, Vector3 relativeDirection) => (transform.right * relativeDirection.x + transform.up * relativeDirection.y + transform.forward * relativeDirection.z).normalized;
        public static Vector3 GetDirectionRelative_Debug(this Transform transform, Vector3 relativeDirection)
        {
            Debugar.DrawRay(transform.position, (transform.right * relativeDirection.x + transform.up * relativeDirection.y + transform.forward * relativeDirection.z).normalized, Color.red);
            return transform.GetDirectionRelative(relativeDirection);
        }


        /// <summary>
        /// It gets the normalized direction to a given target.
        /// If you need it you can transform it to a Quaternion using ToQuaternion().
        /// </summary>
        public static Vector3 GetDirectionToTarget(this Transform transform, Transform target) => (target.position - transform.position).normalized;
        public static Vector3 GetDirectionToTarget_Debug(this Transform transform, Transform target)
        {
            Debugar.DrawRay(transform.position, (target.position - transform.position).normalized, Color.red);
            return transform.GetDirectionToTarget(target);
        }

        /// <summary>
        /// Gets the direction to a target smoothly.
        /// </summary>
        public static Vector3 GetDirectionToTargetSmooth(this Transform transform, Transform target, float speed) => Vector3.RotateTowards(transform.forward, transform.GetDirectionToTarget(target), speed * Time.deltaTime, speed * Time.deltaTime);
        public static Vector3 GetDirectionToTargetSmooth_Debug(this Transform transform, Transform target, float speed) 
        {
            Debugar.DrawRay(transform.position, transform.GetDirectionToTarget(target), Color.green);
            Debugar.DrawRay(transform.position, Vector3.RotateTowards(transform.forward, transform.GetDirectionToTarget(target), speed * Time.deltaTime, speed * Time.deltaTime), Color.yellow);

            return transform.GetDirectionToTargetSmooth(target, speed);
        } 


        public static Vector3 GetDirectionAbsolute(this Transform transform, Vector3 position) => (position - transform.position).normalized;
        public static Vector3 GetDirectionAbsoluteSmooth(this Transform transform, Vector3 direccio, float speed) => Vector3.RotateTowards(transform.forward, direccio.normalized, speed * Time.deltaTime, speed * Time.deltaTime);
        public static Vector3 GetDirectionAbsoluteSmooth_Debug(this Transform transform, Vector3 direccio, float speed)
        {
            Debugar.DrawRay(transform.position, direccio, Color.green);
            Debugar.DrawRay(transform.position, Vector3.RotateTowards(transform.forward, direccio.normalized, speed * Time.deltaTime, speed * Time.deltaTime), Color.yellow);

            return transform.GetDirectionAbsoluteSmooth(direccio, speed);
        }

        //Canviar, no es a camara, es a un objecte qualsevol.
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

    public static class XS_Input
    {
        /// <summary>
        /// Is listening the given key of the InputSystem and returns TRUE at the frame it is pressed. Otherwise it returns FALSE.
        /// It needs "using UnityEngine.InputSystem;" to refere to Key.
        /// </summary>
        public static bool OnPress(this Key key) => Keyboard.current[key].wasPressedThisFrame;
        public static bool GetBool(this InputActionReference inputActionReference) => inputActionReference.action.ReadValue<float>() > 0.1f;

        public static float GetFloat(this InputActionReference inputActionReference) => inputActionReference.action.ReadValue<float>();
        public static Vector2 GetVector2(this InputActionReference inputActionReference) => inputActionReference.action.ReadValue<Vector2>();
        public static void OnPerformedAdd(this InputActionReference inputActionReference, Action<InputAction.CallbackContext> action) => inputActionReference.action.performed += action;
        public static void OnPerformedRemove(this InputActionReference inputActionReference, Action<InputAction.CallbackContext> action) => inputActionReference.action.performed -= action;
        public static bool ComparePath(this InputBinding inputBinding, string path) => inputBinding.PathOrOverridePath() == path;

        public static string PathOrOverridePath(this InputBinding inputBinding)
        {
            if (string.IsNullOrEmpty(inputBinding.overridePath))
                return inputBinding.path;
            else return inputBinding.overridePath;
        }

        /// <summary>
        /// 
        /// </summary>
        public static InputDevice GetDevice() => PlayerInput.GetPlayerByIndex(0).devices[0];
        public static InputDevice GetDevice(int playerIndex) => PlayerInput.GetPlayerByIndex(playerIndex).devices[0];

        public static Vector3 MouseRayCastFromCamera_Point() => MouseRayCastFromCamera_Point(MyCamera.Main, XS_Layers.Everything);
        public static Vector3 MouseRayCastFromCamera_Point(Camera camera) => MouseRayCastFromCamera_Point(camera, XS_Layers.Everything);
        public static Vector3 MouseRayCastFromCamera_Point(LayerMask layerMask) => MouseRayCastFromCamera_Point(MyCamera.Main, layerMask);
        public static Vector3 MouseRayCastFromCamera_Point(Camera camera, LayerMask layerMask)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 300, layerMask))
            {
#if UNITY_EDITOR
                Debugar.Primitive(PrimitiveType.Sphere, hit.point, Vector3.one * 0.1f, 1);
#endif
                return hit.point;
            }
            else
            {
#if UNITY_EDITOR
                Debugar.Primitive(PrimitiveType.Sphere, camera.ScreenToWorldPoint(camera.ScreenPointToRay(Input.mousePosition).origin + Vector3.forward * 300), Vector3.one * 0.1f, 1);
#endif
                return camera.ScreenToWorldPoint(camera.ScreenPointToRay(Input.mousePosition).origin + Vector3.forward * 300);
            }
        }


        public static GameObject MouseRayCastFromCamera() => MouseRayCastFromCamera(MyCamera.Main, XS_Layers.Everything);
        public static GameObject MouseRayCastFromCamera(Camera camera) => MouseRayCastFromCamera(camera, XS_Layers.Everything);
        public static GameObject MouseRayCastFromCamera(LayerMask layerMask) => MouseRayCastFromCamera(MyCamera.Main, layerMask);
        public static GameObject MouseRayCastFromCamera(Camera camera, LayerMask layerMask)
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 300, layerMask))
            {
#if UNITY_EDITOR
                Debugar.Primitive(PrimitiveType.Sphere, hit.point, Vector3.one * 0.1f, 1);
#endif
                return hit.collider.gameObject;
            }
            else
            {
#if UNITY_EDITOR
                Debugar.Primitive(PrimitiveType.Sphere, camera.ScreenToWorldPoint(camera.ScreenPointToRay(Input.mousePosition).origin + Vector3.forward * 300), Vector3.one * 0.1f, 1);
#endif
                return null;
            }
        }
    }


    public static class XS_Layers
    {
        public static LayerMask Everything => -1;
        public static int GetLayer(string name) => LayerMask.NameToLayer(name);
        public static bool Contains(this LayerMask layerMask, int layer) => (layerMask.value & (1 << layer)) > 0;
    }

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

    public static class XS_Instantiate
    {
        /// <summary>
        /// Instantiate objects using the Pooling system
        /// </summary>
#region Instantiate Tools
        //When an object is instantiated and item of the dictionary is created, with the Prefab as key and the Pool used for this prefab.
        //If the dictionary already contains de prefab we want to create, it takes the Pool refered instead of creating a new one
        static Dictionary<GameObject, Pool> pools;

        public class Pool
        {
            bool initializated = false;
            ObjectPool<GameObject> pool;

            GameObject original;
            GameObject tmp;

            Func<Vector3> position;
            Func<Quaternion> rotation;
            Action<GameObject> onRelease;
            Transform parent;

            Vector3 Position
            {
                get
                {
                    if (position != null) return position.Invoke();
                    else return Vector3.zero;
                }
            }
            Quaternion Rotation
            {
                get
                {
                    if (rotation != null) return rotation.Invoke();
                    else return Quaternion.identity;
                }
            }

            /// <summary>
            /// It creates the Pool passing the functions needed to polled from pool.
            /// It is called ones when we try to instantiate the object of first time. 
            /// One the Pool is created is sets "initializated" to TRUE to prevent to call it multiple times.
            /// </summary>
            void Initialize(GameObject _original, Func<Vector3> _position, Func<Quaternion> _rotation, Transform _parent)
            {
                original = _original;
                position = _position;
                rotation = _rotation;
                onRelease += Release;
                parent = _parent;
                pool = new ObjectPool<GameObject>(Crear, OnPoolGet, OnPoolRelease);
                initializated = true;
            }

            /// <summary>
            /// It's the main function to pull (or create) objects from Pool.
            /// It calls Initialize if it needs to don't care do it form outside.
            /// </summary>
            public GameObject Get(GameObject _original, Func<Vector3> _position, Func<Quaternion> _rotation, Transform _parent)
            {
                if (!initializated) Initialize(_original, _position, _rotation, _parent);
                return pool.Get();
            }
            /// <summary>
            /// The same as before, but thsi one passes an specific function to call when the object is released.
            /// </summary>
            public GameObject Get(GameObject _original, Func<Vector3> _position, Func<Quaternion> _rotation, Action<GameObject> _onRelease, Transform _parent)
            {
                if (!initializated) 
                {
                    onRelease += _onRelease;
                    Initialize(_original, _position, _rotation, _parent);
                }
                return pool.Get();
            }

            /// <summary>
            /// It's responsable to actually "Create" the object in the scene.
            /// It is just actually called when the Pool is empty and needs to add more objects in the scene.
            /// When there are objects in the scene the system doesn't use this functions, just sets active them.
            /// It also adds a script to the gameobject that calls the function OnPoolRelease then gameobject sets inactive.
            /// </summary>
            GameObject Crear()
            {
                tmp = UnityEngine.Object.Instantiate(original);
                tmp.AddComponent<PooledObject>().Iniciar(onRelease);
                return tmp;
            }

            /// <summary>
            /// This is called when an object is pulled form the Pool whenever is created or set actived.
            /// </summary>
            void OnPoolGet(GameObject _pooledObject)
            {
                _pooledObject.transform.position = Position;
                _pooledObject.transform.rotation = Rotation;
                _pooledObject.transform.SetParent(parent);
                _pooledObject.gameObject.SetActive(true);
            }

            /// <summary>
            /// This is called when and object of the Pool is release. It means that it can be Pulled again.
            /// It is usefull if you want to release the object without disabling it.
            /// I just set the gameobject inactive. 
            /// </summary>
            void OnPoolRelease(GameObject _pooledObject) => _pooledObject.gameObject.SetActive(false);

            /// <summary>
            /// It releases the object to be able to be pulled again.
            /// It is called from the PooledObject scripts when the gameobjects gets disabled.
            /// </summary>
            void Release(GameObject _pooledObject) => pool.Release(_pooledObject);
        }


        /// <summary>
        /// The script added to all pulled objects. I just release the gameobject when it gets disabled to be able to be pulled again.
        /// </summary>
        public class PooledObject : MonoBehaviour
        {
            Action<GameObject> enRelease;
            public void Iniciar(Action<GameObject> enRelease) => this.enRelease = enRelease;
            private void OnDisable() => enRelease.Invoke(gameObject);
        }
#endregion

        public static GameObject Instantiate(this GameObject original) => Instantiate(original, null, null, null);
        public static GameObject Instantiate(this GameObject original, Func<Vector3> position) => original.Instantiate(position, null, null);
        public static GameObject Instantiate(this GameObject original, Func<Vector3> position, Func<Quaternion> rotation) => original.Instantiate(position, rotation, null);
        public static GameObject Instantiate(this GameObject original, Func<Vector3> position, Func<Quaternion> rotation, Transform parent)
        {
            if (pools == null) pools = new Dictionary<GameObject, Pool>();
            if (!pools.ContainsKey(original)) pools.Add(original, new Pool());
            return pools[original].Get(original, position, rotation, parent).gameObject;
        }
        public static GameObject Instantiate(this GameObject original, Func<Vector3> position, Func<Quaternion> rotation, Action<GameObject> onRelease, Transform parent)
        {
            if (pools == null) pools = new Dictionary<GameObject, Pool>();
            if (!pools.ContainsKey(original)) pools.Add(original, new Pool());
            return pools[original].Get(original, position, rotation, parent).gameObject;
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

    public static class XS_Coroutine
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
        static WaitForSecondsRealtime waitForSecondsRealtime;





        public static Coroutine StartCoroutine(Action update)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopCondition(InfiniteLoop, update));
        }
        public static Coroutine StartCoroutine(Func<bool> sortida, Action update)
        {
            Init();
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopCondition(sortida, update));
        }
        public static Coroutine StartCoroutine(float time, Action ending)
        {
            Init();
            waitForSecondsRealtime = new WaitForSecondsRealtime(time);
            return corrutinaEstaticaMonoBehavior.StartCoroutine(LoopTime(ending));
        }



        static IEnumerator LoopCondition(Func<bool> sortida, Action update)
        {
            while (!sortida.Invoke())
            {
                update.Invoke();
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
        static IEnumerator LoopTime(Action ending)
        {
            yield return waitForSecondsRealtime;
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

    public static class MyCamera
    {
        static Camera camera;
        public static Camera Main 
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

    public static class XS_Animation
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

    public static class XS_ParticleSystem
    {
        static ParticleSystem.MainModule mainModule;
        static ParticleSystem.EmissionModule emissionModule;
        public static void Emit(this ParticleSystem particleSystem, int count)
        {
            ParticleSystem.EmitParams emitParams =  new ParticleSystem.EmitParams();
            particleSystem.Emit(emitParams, count);
        }
        public static void Emit(this ParticleSystem particleSystem, int count, float time)
        {
            //particleSystem.Params
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            mainModule = particleSystem.main;
            mainModule.startLifetime = time;
            particleSystem.Emit(emitParams, count);
        }
        public static void EmisionRateOverTime(this ParticleSystem particleSystem, float count)
        {
            emissionModule = particleSystem.emission;
            emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(count);
        }
        public static void StopAction(this ParticleSystem particleSystem, ParticleSystemStopAction action)
        {
            mainModule = particleSystem.main;
            mainModule.stopAction = action;
        }
        public static void Stop(this ParticleSystem particleSystem) => particleSystem.Stop();

        public static bool IsEmitting(this ParticleSystem particleSystem) => particleSystem.isEmitting;
    }

    public static class XS_Localization
    {
        public static void SelectLanguage(this int localeIndex)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
        }
        public static int Languages => LocalizationSettings.AvailableLocales.Locales.Count;
    }

    public static class XS_UI
    {
        /// <summary>
        /// Returns the world space point of an UI element.
        /// This is perfect to place objectes or efectes in the user interface.
        /// </summary>
        public static Vector3 ToWorldPosition(this RectTransform element)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, MyCamera.Main, out Vector3 result);
            return result;
        }
            
        public static Vector3 ToCanvas(this Vector3 position) => MyCamera.Main.WorldToScreenPoint(position);
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

        public static bool IsAtDirectionOfTarget(this Rigidbody rigidbody, Collider me)
        {
            //Debugar.DrawRay(rigidbody.position, rigidbody.velocity.normalized, Color.red);
            //Debugar.DrawRay(rigidbody.position, rigidbody.transform.GetDirectionToTarget(target.transform), Color.blue);
            Debugar.DrawLine(rigidbody.position, me.ClosestPoint(rigidbody.position), Color.yellow);
            //me.ClosestPoint(transform.position);
            return Vector3.Dot(rigidbody.transform.GetDirectionAbsolute(me.ClosestPoint(rigidbody.position)), rigidbody.velocity.normalized) > 0.1f;
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

    public static class XS_Vectors
    {

        public static Vector3 Dot1 => Vector3.one * 0.1f;
        public static Vector3 Half => Vector3.one * 0.5f; 
    }
}
