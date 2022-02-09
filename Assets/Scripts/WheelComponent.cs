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

public class WheelComponent : MonoBehaviour
{
    private Transform[] _frontWheels;
    private Transform[] _rearWheels;

    private WheelCollider[] _frontColliders;
    private WheelCollider[] _rearColliders;
    private WheelCollider[] _allColliders;

    [SerializeField]    private Transform _rFrontM;
    [SerializeField]    private Transform _lFrontM;
    [SerializeField]    private Transform _rBackM;
    [SerializeField]    private Transform _lBackM;
    [SerializeField] private WheelCollider _rFrontC;
    [SerializeField] private WheelCollider _lFrontC;
    [SerializeField] private WheelCollider _rBackC;
    [SerializeField] private WheelCollider _lBackC;

    public WheelCollider[] GetFrontColliders => _frontColliders;
    public WheelCollider[] GetRearColliders => _rearColliders;
    public WheelCollider[] GetAllColliders => _allColliders;

    private void Start()
    {
        _frontWheels = new Transform[] { _rFrontM, _lFrontM };
        _rearWheels = new Transform[] { _rBackM, _lBackM };
        _frontColliders = new WheelCollider[] { _rFrontC, _lFrontC };
        _rearColliders = new WheelCollider[] { _rBackC, _lBackC };

        _allColliders = new WheelCollider[] { _rFrontC, _lFrontC, _rBackC, _lBackC };
    }
    /// <summary>
    /// to be called each frame
    /// </summary>
    /// <param name="yAngle"></param>
    public void UpdateWheelVisual(float yAngle)
    {
        
        for (var i =0; i < _frontColliders.Length; i++)
        { 
            _frontColliders[i].steerAngle = yAngle;
            _frontColliders[i].GetWorldPose(out var position, out var rotation);

            _frontWheels[i].position = position;
            _frontWheels[i].rotation = rotation;

            _rearColliders[i].GetWorldPose(out position, out rotation);
            _rearWheels[i].position = position;
            _rearWheels[i].rotation = rotation;
        }

    }


}
