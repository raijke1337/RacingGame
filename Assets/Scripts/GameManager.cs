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
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager CallManager { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    
    private Transform _carsPool;
    private LinkedList<BaseCarUnit> _cars = new LinkedList<BaseCarUnit>();

    private Timer _timer;
    private ScoreBoard _score;
    private Countdown _counter;

    private void OnEnable()
    {
        _carsPool = GameObject.Find("CarPool").GetComponent<Transform>();
#if UNITY_EDITOR
        if (_carsPool == null) Debug.LogError("no car pool found");
#endif
        var list = _carsPool.GetComponentsInChildren<BaseCarUnit>();
        int counter = 0;
        // test for multiple player comps
        foreach (var car in list)
        {
            _cars.AddLast(car);
            if (car.GetControllerType != Tools.InputType.Player) return;
            counter++;
            car.OnFinishEvent += Car_OnFinishEvent; ; 
        }
#if UNITY_EDITOR
        if (counter > 1) Debug.LogError("multiple player car components in scene");
#endif
    }
    private void Start()
    {
        _timer = GameObject.FindObjectOfType<Timer>();
        _score = GameObject.FindObjectOfType<ScoreBoard>();
        _timer.gameObject.SetActive(false);
        _score.gameObject.SetActive(false);
        _counter = FindObjectOfType<Countdown>();
        _counter.TimerOverEvent += _counter_TimerOverEvent;
    }

    private void _counter_TimerOverEvent(object sender, EventArgs e)
    {
        _timer.StartTimer();
        foreach (var c in _cars)
        {
            c.EnableControls();
        }
    }

    public BaseCarUnit GetPlayerCar()
    {
        return _cars.First(c => c.GetControllerType == Tools.InputType.Player);
    }


    private void Car_OnFinishEvent(object sender, BaseCarUnit e)
    {
        e.DisableControls();
        var result = _timer.EndTimer();
        _timer.IsActive = false;

        _score.gameObject.SetActive(true);
        _score.CurrentRunScore = result;
        _score.DisplayScores();
    }



}
