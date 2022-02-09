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
using static Tools;

public abstract class BaseCarUnit : MonoBehaviour
{

    public InputType GetControllerType { get; protected set; }


    public float Acceleration { get; protected set; }
    public float WheelRotation { get; protected set; }
    public event EventHandler<bool> OnBrakeEvent;
    public event EventHandler<BaseCarUnit> OnFinishEvent;

    protected virtual void Start()
    {
    }

    public abstract void EnableControls();
    public abstract void DisableControls();

    protected abstract void FixedUpdate();

    protected void CallHandBrake(bool value) => OnBrakeEvent?.Invoke(null, value);
    protected void CallFinish() => OnFinishEvent?.Invoke(null,this);

}
