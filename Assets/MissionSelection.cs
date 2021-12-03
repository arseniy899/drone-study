using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.IO;
//using UnityEngine.WSA;

using SimpleFileBrowser;
public class MissionSelection : MonoBehaviour
{
    public Text status;
    // Start is called before the first frame update
    void GoToScene(string missionName)
    {
        StaticClass.crossSceneInformation = new StaticClass.CrossSceneInformation(missionName, 1);
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
    public void ShowOpenPanel()
    {
        status.text = "Открывается проводник....";
       
        ReadFile("C:\\Dev\\LD\\drone-study\\облёт объектов.json");
        return;


        FileBrowser.SetFilters(true, new FileBrowser.Filter("Запись Миссии", ".json"));

        FileBrowser.ShowLoadDialog((paths) => { 
            Debug.Log("Selected: " + paths[0]);
            status.text = "Файл загружен";
            ReadFile(paths[0]);
        },
        	() => { Debug.Log( "Canceled" );
                status.text = "Выберите файл";
            },
        FileBrowser.PickMode.Files, false, "./", null, "Select Route", "Select" );

    }
    public static void ReadFile(string file)
    {
        

        StreamReader reader = new StreamReader(file);
        string content = reader.ReadToEnd();
        Debug.Log(content);
        StaticClass.missionDataToReplay = content;
        reader.Close();

    }
    public void Mission3()
    {
        GoToScene("Simulator-Complex");
    }
    void Start()
    {
        StaticClass.missionDataToReplay = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}