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
        if (isPlaying && journeyLength != 0)
        {
            // Distance moved equals elapsed time times speed..
            float speed = journeyLength / timeDeltaForMove * 2;
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (Vector3.Distance(transform.position, endMarker) < 1f)
            {
                startTime = 0;
            }
        }
    }
    void applyDroneState(StaticClass.DroneState state, float timeDelta)
    {
        timeDeltaForMove = timeDelta;
        startMarker = transform.position;
        endMarker = new Vector3(state.lat, state.alt, state.lon);
        journeyLength = Vector3.Distance(startMarker, endMarker);
        
        startTime = Time.time;
       
        
        Quaternion targetAngle = Quaternion.Euler(state.row, state.yaw, state.pitch);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * smooth);
        StaticClass.droneState.droneOn = state.droneOn;
        StaticClass.droneState.enginesOn = state.enginesOn;
        droneController.switchPropellers();


    }
    bool isPlaying = false;
    public void StartPLay()
    {
        //drone.GetComponent<Rigidbody>().isKinematic = true;
        startMarker = transform.position;
        endMarker = startMarker;
        isPlaying = true;
        StartCoroutine(PlayerCoroutine());
        
        startPlay.gameObject.SetActive(false);
        stopPlay.gameObject.SetActive(true);
        MissionManager.showNextCheckpoint();
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
        if (droneStates.Count > 0)
        {
            double lastCommandTime = droneStates[0].time;
            //StaticClass.DroneState lastState = droneStates[0];
            transform.position = new Vector3(droneStates[0].lat, droneStates[0].alt, droneStates[0].lon);
            foreach (StaticClass.DroneState state in droneStates)
            {
                double timeDelta = state.time - lastCommandTime;
                yield return new WaitForSeconds((float)timeDelta*2);
                
                if (!isPlaying)
                {
                    break;
                }
                applyDroneState(state, (float)timeDelta);
                lastCommandTime = Time.realtimeSinceStartup;

            }
        }
        StopPlay();
    
    }
}
