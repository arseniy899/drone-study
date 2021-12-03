using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionSelection : MonoBehaviour
{
    public InputField missionReplayContent = null;
    // Start is called before the first frame update
    void GoToScene(string missionName)
    {
        StaticClass.crossSceneInformation = new StaticClass.CrossSceneInformation(missionName, 1);
        StaticClass.missionDataToReplay = missionReplayContent.text;
        SceneManager.LoadScene(missionName);
    }
    public void Mission1()
    {
        GoToScene("Simulator-Takeoff-land");
    }
    public void Mission2()
    {
        GoToScene("Simulator-First fly");
    }

    public void Mission3()
    {
        GoToScene("Simulator-Complex");
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}