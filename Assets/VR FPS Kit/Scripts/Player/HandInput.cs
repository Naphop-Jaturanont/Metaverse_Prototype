using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandInput : MonoBehaviour
{
    private InputDevice leftHand, rightHand;

    private bool a, aDown, aTracker,
                leftGrip, leftGripDown, leftGripTracker,
                leftTrigger, leftTriggerDown, leftTriggerTracker,
                rightGrip, rightGripDown, rightGripTracker,
                rightTrigger, rightTriggerDown, rightTriggerTracker,
                rightStickRight, rightStickRightSnap, rightStickRightSnapTracker,
                rightStickLeft, rightStickLeftSnap, rightStickLeftSnapTracker;
    private Vector2 leftStick, rightStick;
    // Start is called before the first frame update
    void Start()
    {
        //VR Inputs
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, inputDevices);
        leftHand = inputDevices[0];
        inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);
        rightHand = inputDevices[0];  
    }

    //ONLY CALL INPUT from fixedUpdate functions
    private void FixedUpdate() {
         rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out a);
         rightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightStick);
         rightHand.TryGetFeatureValue(CommonUsages.gripButton, out rightGrip);
         rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out rightTrigger);
         leftHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftStick);
         leftHand.TryGetFeatureValue(CommonUsages.gripButton, out leftGrip);
         leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out leftTrigger);
         aDown = GetIsDown(ref aTracker, a);
         leftGripDown = GetIsDown(ref leftGripTracker, leftGrip);
         rightGripDown = GetIsDown(ref rightGripTracker, rightGrip);
         leftTriggerDown = GetIsDown(ref leftTriggerTracker, leftTrigger);
         rightTriggerDown = GetIsDown(ref rightTriggerTracker, rightTrigger);
         rightStickLeftSnap = GetIsDown(ref rightStickLeftSnapTracker, rightStick.x < -.9);
         rightStickRightSnap = GetIsDown(ref rightStickRightSnapTracker, rightStick.x > .9);
    }

    public bool GetAButton()
    {
        return a;
    }
    public bool GetAButtonDown()
    {
        return aDown;
    }

    //Left Inputs
    
    public Vector2 GetLeftStick()
    {
        return leftStick;
    }
    public bool GetLeftGrip()
    {
        return leftGrip;
    }
    public bool GetLeftGripDown()
    {
        return leftGripDown;   
    }
    public bool GetLeftTrigger()
    {
        return leftTrigger;
    }
    public bool GetLeftTriggerDown()
    {
        return leftTriggerDown;
    }
    
    //Right Inputs
    
    public Vector2 GetRightStick()
    {
        return rightStick;
    }
    public bool GetRightStickLeftSnap()
    {
        return rightStickLeftSnap;
    }
    public bool GetRightStickRightSnap()
    {
        return rightStickRightSnap;
    }
    public bool GetRightGrip()
    {
        return rightGrip;
    }
    public bool GetRightGripDown()
    {
        return rightGripDown;
    }
    public bool GetRightTrigger()
    {
        return rightTrigger;
    }
    public bool GetRightTriggerDown()
    {
        return rightTriggerDown;
    }
    
    bool GetIsDown(ref bool downVariable, bool isDown)
    {
        if(downVariable != isDown)
        {
            downVariable = isDown;  
            if(isDown == true)
            {
                return true;
            }   
        }
        return false;
        
    }
}
