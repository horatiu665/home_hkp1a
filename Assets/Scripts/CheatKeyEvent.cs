using System;
using System.Collections;
using System.Collections.Generic;
using ToyBoxHHH;
using UnityEngine;
using UnityEngine.Events;

public class CheatKeyEvent : MonoBehaviour
{
    public bool onlyEditor = false;
    public KeyCode key = KeyCode.H;

    public UnityEvent OnCheat;

    private void OnEnable()
    {
        if (onlyEditor && !Application.isEditor)
        {
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (enabled)
        {
            if (Input.GetKeyDown(key))
            {
                DoItNow();
            }
        }
    }

    [DebugButton]
    private void DoItNow()
    {
        OnCheat?.Invoke();
    }
}
