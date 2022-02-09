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

public class CarNPCInput : BaseCarUnit
{
    private Stack<Transform> _points;

    private void OnValidate()
    {
        GetControllerType = Tools.InputType.NPC;
    }
    protected override void FixedUpdate()
    {

    }

    public override void EnableControls()
    {
    }

    public override void DisableControls()
    {
    }
}
