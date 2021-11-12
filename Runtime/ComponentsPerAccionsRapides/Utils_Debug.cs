using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XS_Utils;

public class Utils_Debug : MonoBehaviour
{
    public void Debug(string log) => Debugar.Log($"Utils_Debug(string): {log}");
    public void Debug(bool log) => Debugar.Log($"Utils_Debug(bool): {log}");
    public void Debug(int log) => Debugar.Log($"Utils_Debug(int): {log}");
    public void Debug(float log) => Debugar.Log($"Utils_Debug(float): {log}");
    public void Debug(Vector2 log) => Debugar.Log($"Utils_Debug(vector2): {log}");
    public void Debug(Vector3 log) => Debugar.Log($"Utils_Debug(vector3): {log}");
}
