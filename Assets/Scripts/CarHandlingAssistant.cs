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

[RequireComponent(typeof(BaseCarUnit)),RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(WheelComponent))]
public class CarHandlingAssistant : MonoBehaviour
{

    private Rigidbody _rb;
    private BaseCarUnit _car;
    private WheelComponent _wheels;
    [SerializeField]
    private SteeringBehavior _steeringData;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _car = GetComponent<BaseCarUnit>();
        _wheels = GetComponent<WheelComponent>();
        _car.OnBrakeEvent += (f,ff) => OnBrake(ff);

        _rb.centerOfMass = _steeringData.CenterOfMass;
    }
    private void OnBrake (bool val)
    {
        if (val)
        {
            foreach (var w in _wheels.GetRearColliders)
            {
                w.brakeTorque = _steeringData.BrakingForce;
            }
        }
        else
        {
            foreach (var w in _wheels.GetRearColliders)
            {
                w.brakeTorque = 0f;
            }
        }

    }
    private void OnAcceleration()
    {
        // driving
        //2f for 2 wheels
        var torque = (_steeringData.EngineTorque * _car.Acceleration) / 2f;
        foreach (var w in _wheels.GetFrontColliders)
        {
            w.motorTorque = torque;
        }
    }

    private void FixedUpdate()
    {
        // wheel rotation
        _wheels.UpdateWheelVisual(_car.WheelRotation * _steeringData.MaxSteerAngle);

        OnAcceleration();
        //Debug.Log($"{_car.Acceleration} {_car.WheelRotation}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(_steeringData.CenterOfMass),1f);
    }


}
