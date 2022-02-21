using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
class Utils_Version
{
    const string VERSION_KEY = "version";
    static Utils_Version()
    {
        if (!Application.isEditor)
            return;

        float version;
        if (!float.TryParse(PlayerSettings.bundleVersion, out float result))
        {
            Debug.Log("Version is not Parsable!!!");
            return;
        }

        version = result;
        version += 0.001f;

        PlayerSettings.bundleVersion = version.ToString();
    }

}
