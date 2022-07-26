using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XS_Utils
{
    public static class XS_Input
    {
        /// <summary>
        /// Is listening the given key of the InputSystem and returns TRUE at the frame it is pressed. Otherwise it returns FALSE.
        /// It needs "using UnityEngine.InputSystem;" to refere to Key.
        /// </summary>
        public static bool OnPress(this Key key) => Keyboard.current[key].wasPressedThisFrame;
        public static bool GetBool(this InputActionReference inputActionReference) => inputActionReference.action.ReadValue<float>() > 0.1f;

        public static bool IsFloatZero(this InputActionReference inputActionReference) => inputActionReference.action.ReadValue<float>() == 0;
        public static float GetFloat(this InputActionReference inputActionReference) => inputActionReference.action.ReadValue<float>();
        
        public static bool IsVector2Zero(this InputActionReference inputActionReference) => inputActionReference.action.ReadValue<Vector2>() == Vector2.zero;
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

}
