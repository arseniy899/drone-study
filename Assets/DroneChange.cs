using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneChange : MonoBehaviour
{
    public List<GameObject> drones;
    int curDrone = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SwitchDrone()
    {
        if( (curDrone+1) == drones.Count )
        {
            curDrone = 0;
        }
        else
        {
            curDrone++;
        }
        for(int i = 0; i < drones.Count; i++)
        {
            drones[i].SetActive(i == curDrone);
        }
    }
}
