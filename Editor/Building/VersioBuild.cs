using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
class VersioBuild
{
    static VersioBuild() 
    {
        BuildingExtraCheckings.checkings -= Versio;
        BuildingExtraCheckings.checkings += Versio;
    }
    static float version;
    static float previous;
    static void Versio()
    {
        Debug.Log("...start checking [VERSION]");

        if (!Application.isEditor)
            return;

        
        if (!float.TryParse(PlayerSettings.bundleVersion, out previous))
        {
            throw new System.NotImplementedException($"[VERSION] Versio is no parsable!!!");
        }

        version = previous;
        version += 0.001f;

        PlayerSettings.bundleVersion = version.ToString();

        Debug.Log($"...increased bundleVersion from {previous} to {PlayerSettings.bundleVersion}");

        Debug.Log("...end checking [VERSION]");
        Debug.Log("-----------------------------------------------");
    }

}
