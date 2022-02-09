using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarPlayerInput : BaseCarUnit
{
    private Controls _controls;

    private void OnValidate()
    {
        GetControllerType = Tools.InputType.Player;
    }

    public override void EnableControls()
    {
        _controls.Default.Enable();
    }

    public override void DisableControls()
    {
        _controls.Default.Disable();
    }

    #region ControlsEnableDisable
    private void Awake()
    {
        _controls = new Controls();
    }

    private void OnDestroy()
    {
        _controls.Dispose();
    }
    #endregion
    protected override void FixedUpdate()
    {
        var steerDir = _controls.Default.Steer.ReadValue<float>();       
        // rotate back to neutral if no input
        if (steerDir == 0f && WheelRotation != 0f)
        {
            WheelRotation = WheelRotation > 0f ?
                WheelRotation -= Time.fixedDeltaTime :
                WheelRotation += Time.fixedDeltaTime ;
        }
        else
        {
            WheelRotation = Mathf.Clamp(WheelRotation + steerDir * Time.fixedDeltaTime, -1f, 1f);
        }


        Acceleration = _controls.Default.Accelerate.ReadValue<float>();
    }
    protected override void Start()
    {
        base.Start();
        _controls.Default.Handbrake.performed += (f) => CallHandBrake(true);
        _controls.Default.Handbrake.canceled += (f) => CallHandBrake(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        CallFinish();
    }

}
