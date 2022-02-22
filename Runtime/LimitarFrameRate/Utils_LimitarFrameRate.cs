using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-8)]
[CreateAssetMenu(menuName = "Xido Studio/Utils/Limitar FrameRate", fileName = "Limitar FrameRate")]
public class Utils_LimitarFrameRate : ScriptableObject
{
    [SerializeField] int frameRate;

    private void OnEnable()
    {
        LimitarFrameRate();
    }

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