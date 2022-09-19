using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviourSingleton<InputManager>
{
    Dictionary<string, float> axisValues = new Dictionary<string, float>();

    public void SetAxis(string axis, float value)
    {
        if (!axisValues.ContainsKey(axis))
        {
            axisValues.Add(axis, value);
        }
        axisValues[axis] = value;
    }

    float GetOrAddAxis(string axis)
    {
        if (!axisValues.ContainsKey(axis))
        {
            axisValues.Add(axis, 0);
        }

        return axisValues[axis];
    }

    public float GetAxis(string axis)
    {
#if UNITY_EDITOR
        return GetOrAddAxis(axis) + Input.GetAxis(axis);
#elif UNITY_ANDROID
        return GetOrAddAxis(axis);
#elif UNITY_STANDALONE
        return GetOrAddAxis(axis) + Input.GetAxis(axis);
#endif
    }

    public bool GetButton(string button)
    {
        return Input.GetButton(button);
    }
}
