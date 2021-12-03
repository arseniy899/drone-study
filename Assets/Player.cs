using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections;
public class Player : MonoBehaviour
{
    public GameObject playControl;
    public Button startPlay;
    public Button stopPlay;

    public GameObject drone;
    public List<StaticClass.DroneState> droneStates = new List<StaticClass.DroneState>();

    DroneControlC droneController;

    // Start is called before the first frame update
    void Start()
    {
        droneStates = JsonConvert.DeserializeObject<List<StaticClass.DroneState>>(StaticClass.missionDataToReplay);
        droneController = drone.GetComponent<DroneControlC>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowPLayControl()
    {
        playControl.SetActive(true);
    }
    float smooth = 5.0f;
    void applyDroneState(StaticClass.DroneState state)
    {
        drone.transform.position = new Vector3(state.lat, state.alt, state.lon);
        Quaternion target = Quaternion.Euler(state.row, state.pitch, state.yaw);
        drone.transform.rotation = Quaternion.Slerp(drone.transform.rotation, target, Time.deltaTime * smooth);
        StaticClass.droneState.droneOn = state.droneOn;
        StaticClass.droneState.enginesOn = state.enginesOn;
        droneController.switchPropellers();


    }
    bool isPlaying = true;
    public void StartPLay()
    {
        //drone.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(PlayerCoroutine());
        isPlaying = true;
        startPlay.gameObject.SetActive(false);
        stopPlay.gameObject.SetActive(true);
    }
    public void StopPlay()
    {
        isPlaying = false;
        drone.GetComponent<Rigidbody>().isKinematic = false;
        startPlay.gameObject.SetActive(true);
        stopPlay.gameObject.SetActive(false);
    }
    IEnumerator PlayerCoroutine()
    {
        double lastCommandTime = droneStates[0].time;
        foreach (StaticClass.DroneState state in droneStates)
        {
            double timeDelta = state.time - lastCommandTime;
            yield return new WaitForSeconds((float)timeDelta);
            if (!isPlaying)
            {
                break;
            }
            applyDroneState(state);
            lastCommandTime = Time.realtimeSinceStartup;

        }
        StopPlay();
    }
}
