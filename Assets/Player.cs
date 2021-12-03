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

    
    public void ShowPLayControl()
    {
        playControl.SetActive(true);
    }
    float smooth = 5.0f;
    public Vector3 startMarker;
    public Vector3 endMarker;
    // Movement speed in units per second.
    //public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;
    private float timeDeltaForMove;

    // Total distance between the markers.
    private float journeyLength;

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            // Distance moved equals elapsed time times speed..
            float speed = journeyLength / timeDeltaForMove;
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);
        }
    }
    void applyDroneState(StaticClass.DroneState state, float timeDelta)
    {
        timeDeltaForMove = timeDelta;
        startMarker = drone.transform.position;
        endMarker = new Vector3(state.lat, state.alt, state.lon);
        journeyLength = Vector3.Distance(startMarker, endMarker);
        
        startTime = Time.time;
       
        
        Quaternion targetAngle = Quaternion.Euler(0, -state.yaw, state.pitch);
        drone.transform.rotation = Quaternion.Slerp(drone.transform.rotation, targetAngle, Time.deltaTime * smooth);
        StaticClass.droneState.droneOn = state.droneOn;
        StaticClass.droneState.enginesOn = state.enginesOn;
        droneController.switchPropellers();


    }
    bool isPlaying = false;
    public void StartPLay()
    {
        //drone.GetComponent<Rigidbody>().isKinematic = true;
        startMarker = drone.transform.position;
        endMarker = startMarker;
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
            applyDroneState(state, (float)timeDelta);
            lastCommandTime = Time.realtimeSinceStartup;

        }
        StopPlay();
    }
}
