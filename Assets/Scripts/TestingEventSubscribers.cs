using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEventSubscribers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestingEventPublishers testingEventPublishers = GetComponent<TestingEventPublishers>();
        testingEventPublishers.OnSpacePressed += Testing_OnSpacePressedReceived;
        testingEventPublishers.OnFloatEvent += Testing_OnFloatEventReceived;

    }

    private void Testing_OnSpacePressedReceived(object sender, TestingEventPublishers.OnSpacePressedEventArgs e)
    {
        Debug.Log(String.Format("Space! {0}", e.spaceCount));
    }

    private void Testing_OnFloatEventReceived(float f){
        Debug.Log(String.Format("Float! {0}", f));
    }

    public void Testing_OnUnityEventReceived(){
        Debug.Log("Unity Event!");
    }

    // Need to look at unity event best practices now !!!!
}
