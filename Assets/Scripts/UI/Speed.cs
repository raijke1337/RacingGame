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

public class Speed : MonoBehaviour
{
    private const float c_convertSpeed = 3.6f;
    private BaseCarUnit _target;

    private float _maxSpeed = 300f;
    [SerializeField]
    private Color _minColor;
    [SerializeField]
    private Color _maxColor;
    [SerializeField,Range(0.1f,1f)]
    private float _calcDelay = 0.3f;
    [SerializeField]
    private Text text;

    private void Start()
    {
        _target = GameManager.CallManager.GetPlayerCar();
        StartCoroutine(SpeedCalc());
    }

    private IEnumerator SpeedCalc()
    {
        var prevPos = _target.transform.position;
        while (true)
        {
            var dist = Vector3.Distance(_target.transform.position, prevPos);
            var speed = (float)System.Math.Round(dist / _calcDelay * c_convertSpeed, 1);

            text.color = Color.Lerp(_minColor, _maxColor, speed / _maxSpeed);   
            text.text = speed.ToString();

            prevPos = _target.transform.position;
            yield return new WaitForSeconds(_calcDelay);
        }
    }
}
