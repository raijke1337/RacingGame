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

public class InterfaceControl : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowTuningMenu()
    {
        Debug.Log("Showing tuning menu placeholder");
    }
    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
    }
}
