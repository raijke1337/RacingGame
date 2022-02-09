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

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Button _save;

    public float CurrentRunScore { get; set; }
    public string Name { get; set; }

    private Text[] _scoresTexts;
    private List<ScoreRecord> _scores;


    private void OnEnable()
    {
        var fields = GetComponentsInChildren<Text>();
        var list = new List<Text>();

        foreach (var f in fields)
        {
            if (f.name.Contains("Score"))
            {
                list.Add(f);
            }
        }
        list.TrimExcess();
        _scoresTexts = new Text[list.Capacity];
        for (int i = 0; i < list.Capacity; i++)
        {
            _scoresTexts[i] = list[i];
        }
        // add all mathcing text fields to the array to avoid serialization

    }

    public void AddRecord()
    {
        Name = GetComponentInChildren<InputField>().textComponent.text;

        _scores.Add(new ScoreRecord(Name, CurrentRunScore));
        XMLManager.instance.SaveScores(_scores);

        SceneManager.LoadScene(0);
    }

    public void DisplayScores()
    {
        _scores = XMLManager.instance.LoadScores();

        _scores.Sort((ScoreRecord x, ScoreRecord y) => x.Time.CompareTo(y.Time));
        for (int i = 0; i < _scoresTexts.Length; i++)
        {
            if (_scores[i] == null) return;
            _scoresTexts[i].text = _scores[i].Name + " " + _scores[i].Time;
        }

    }


    // that's a lot of for loops huh




}
