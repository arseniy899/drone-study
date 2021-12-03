using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;
public class DroneStateLogger : MonoBehaviour
{
    public Text status;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CollectorCoroutine());
    }

    IEnumerator CollectorCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            StaticClass.droneStates.Add((StaticClass.DroneState)StaticClass.droneState.Clone());
            
        }
    }
        // Update is called once per frame
    void Update()
    {
       
        string data = string.Format("Локация: {0:0.00000}/{1:0.00000}. Выс.: {2:0.0}\n", StaticClass.droneState.lat,
        StaticClass.droneState.lon,StaticClass.droneState.alt- StaticClass.droneState.homeAlt);
        data += string.Format("Скорость: г={0:0.000}, в={1:0.000}\n", StaticClass.droneState.speedH, StaticClass.droneState.speedV);
        data += string.Format("Питание: {0}\n", StaticClass.droneState.droneOn ? "ВКЛ" : "ВЫКЛ");
        data += string.Format("Двигатели: {0}\n", StaticClass.droneState.enginesOn ? "ВКЛ" : "ВЫКЛ");

        status.text = data;
    }
    public static void save(string json)
    {
        using (StreamWriter writetext = File.AppendText("log_"+Time.realtimeSinceStartup+".json"))
        {
            writetext.Write(json);
            writetext.Close();
        }
    }
    public static void sendRecordedData()
    {
        string json = JsonConvert.SerializeObject(StaticClass.droneStates);
        Debug.Log("Sending data: \n" + json);
        save(json);
    }
}
