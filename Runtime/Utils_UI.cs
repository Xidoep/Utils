using UnityEngine;
using UnityEngine.UI;

namespace XS_Utils
{
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

        /// <summary>
        /// Returns the canvas position of a given world element.
        /// This is perfect for positioning UI element over world objects without a floating canvases.
        /// </summary>
        //***********************************************FALTA! poder "clampejar" la posicio a dins de la pantalla perque no se surti del camp de visio/canvas.
        public static Vector3 ToCanvas(this Vector3 position) => MyCamera.Main.WorldToScreenPoint(position);

        public static bool IsOn(this Toggle toggle, float value) => toggle.isOn = !value.IsNear(0, 0.01f);

    }
}