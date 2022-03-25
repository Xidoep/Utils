using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace XS_Utils
{
    public static class XS_Layers
    {
        public static LayerMask Everything => -1;
        public static int GetLayer(string name) => LayerMask.NameToLayer(name);
        public static bool Contains(this LayerMask layerMask, int layer) => (layerMask.value & (1 << layer)) > 0;
    }
}
