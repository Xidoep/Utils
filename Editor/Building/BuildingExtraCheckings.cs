using System;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class BuildingExtraCheckings
{
    static BuildingExtraCheckings() => BuildPlayerWindow.RegisterBuildPlayerHandler(Versio);

    public static Action checkings;
    // Start is called before the first frame update
    static void Versio(BuildPlayerOptions options)
    {
        checkings?.Invoke();

        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
    }
}
