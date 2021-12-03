using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int missionItemId = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (missionItemId != 0)
        {
            gameObject.SetActive(false);
        }
        MissionManager.assignCheckpoint(missionItemId, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        bool isConfirmed = MissionManager.reportCheckpointEnter(missionItemId);
        if (isConfirmed)
        {
            gameObject.SetActive(false);
        }
    }
}
