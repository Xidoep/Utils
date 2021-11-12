using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Xido Studio/Utils/Limitar FrameRate", fileName = "Limitar FrameRate")]
public class Utils_LimitarFrameRate : ScriptableObject
{
    [SerializeField] int frameRate;

    public void LimitarFrameRate()
    {
        LimitarFrameRate(frameRate);
    }
    public void LimitarFrameRate(int frameRate)
    {
        Application.targetFrameRate = frameRate;
    }

    void OnValidate()
    {
        frameRate = Mathf.Clamp(frameRate, 10, 200);
    }

}