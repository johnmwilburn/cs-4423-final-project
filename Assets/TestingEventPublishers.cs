using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TestingEventPublishers : MonoBehaviour
{
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;

    public UnityEvent OnUnityEvent;
    private int spaceCount;

    public class OnSpacePressedEventArgs : EventArgs
    {
        public int spaceCount;
    }

    public event TestEventDelegate OnFloatEvent;
    public delegate void TestEventDelegate(float f);

    private void Start()
    {
        spaceCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // in order to avoid throwing errors
            // if (OnSpacePressed != null) OnSpacePressed(this, EventArgs.Empty);
            // This is EQUIVALENT TO
            spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs { spaceCount = spaceCount });

            OnFloatEvent?.Invoke(5.5f);

            OnUnityEvent?.Invoke();
        }
    }
}
