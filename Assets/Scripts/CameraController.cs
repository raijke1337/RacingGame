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

public class CameraController : MonoBehaviour
{
    private Transform _carT;
    private Transform _basePoint;


    private void Start()
    {
        var car = GameManager.CallManager.GetPlayerCar();
        var point = new GameObject("camPoint");
        point.transform.position = car.transform.position;
        point.transform.rotation = car.transform.rotation;
        point.transform.parent = car.transform;
        _basePoint = point.transform;
    }

    private void Update()
    {
        transform.position = _basePoint.position + Vector3.back * 6 + Vector3.up * 5;
        transform.LookAt(_basePoint);
    }
    // to do smooth follow



}
