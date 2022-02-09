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

public class Countdown : MonoBehaviour
{
    [SerializeField]
    private float _timer = 3f;


    private Text counter;
    public event EventHandler TimerOverEvent;

    private Color _startCol = Color.red;
    private Color _endCol = Color.green;

    private void Start()
    {
        counter = GetComponent<Text>();
        counter.text = _timer.ToString();
        StartCoroutine(PrepareToRace());
    }

    private IEnumerator PrepareToRace()
    {
        while (_timer >= 0f)
        {
            _timer -= Time.deltaTime;
            counter.color = Color.Lerp(_endCol, _startCol, _timer);
            counter.text = Math.Round(_timer, 0).ToString();

            yield return new WaitForEndOfFrame();
        }
        TimerOverEvent?.Invoke(null,null);
        gameObject.SetActive(false);
        yield return null;
    }

}
