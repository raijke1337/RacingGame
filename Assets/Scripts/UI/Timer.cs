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

public class Timer : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public bool IsActive { get; set; }

    private float _resultTime = 0f;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        IsActive = true;
        StartCoroutine(TimerCoroutine());
    }
    public float EndTimer()
    {
        StopCoroutine(TimerCoroutine());
        return _resultTime;
    }

    private IEnumerator TimerCoroutine()
    {
        while (IsActive)
        {
            _resultTime += Time.deltaTime;
            _text.text = TimeSpan.FromSeconds(_resultTime).ToString();

            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

}
