using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace XS_Utils
{
    public static class XS_InstantiatePool
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
}
