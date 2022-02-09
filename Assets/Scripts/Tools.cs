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

public class Tools : MonoBehaviour
{

    public enum InputType
    {
        Player,
        NPC
    }


    [Serializable]
    public class SteeringBehavior
    {
        public float EngineTorque = 2500f;
        public float MaxSteerAngle = 40f;
        public float BrakingForce = float.MaxValue;
        public Vector3 CenterOfMass = Vector3.zero;
        public float MaxSpeed = 300f;

    }

}

public class ScoreRecord
{
    public string Name;
    public float Time;
    public ScoreRecord()
    {

    }
    public ScoreRecord(string name,float time)
    {
        Name = name;Time = time;
    }
}
