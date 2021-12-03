using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public static class StaticClass : object
{
    public static CrossSceneInformation crossSceneInformation;
    public static string missionDataToReplay = "";
    public static DroneState droneState = new DroneState();
    public static List<DroneState> droneStates = new List<DroneState>();
    public struct CrossSceneInformation
    {
        public CrossSceneInformation(string name, int userID)
        {
            this.name = name;
            this.userID = userID;
        }

        public string name { get; }
        public int userID { get; }


    }
    [System.Serializable]
    public class DroneState
    {

        public DroneState()
        {
            
        }
        [SerializeField] public double time = 0d;
        [SerializeField] public float lat = 0f;
        [SerializeField] public float lon = 0f;
        [SerializeField] public float alt = 0f;
        [SerializeField] public float speedV = 0f;
        [SerializeField] public float speedH = 0f;
        [SerializeField] public float row = 0f;
        [SerializeField] public float pitch = 0f;
        [SerializeField] public float yaw = 0f;
        [SerializeField] public bool droneOn = false;
        [SerializeField] public bool enginesOn = false;

        public float homeLat = 0f;
        public float homeLon = 0f;
        public float homeAlt = 0f;
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public enum MissionsNames
    {
        TakeOff_Land,
        HomePoint_Choose,
        FirstFly,
        Complex
    };
}