using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
public class MissionManager : MonoBehaviour
{
    public enum ItemType
    {
        ReachCheckPoint,
        DoAction
    }
    public class MissionItem
    {
        public bool isDone = false;
        public ItemType type = 0;
        public int val1 = 0;
        public int val2 = 0;
        public int wait = 0;
        public double doneAt= 0;
        public GameObject checkpoint= null;
        public MissionItem(ItemType type, int val1, int val2, int wait)
        {
            this.type = type;
            this.val1 = val1;
            this.val2 = val2;
            this.wait = wait;
        }
        public MissionItem(ItemType type, int val1, int wait)
        {
            this.type = type;
            this.val1 = val1;
            this.wait = wait;
        }
        public MissionItem(ItemType type, int val1)
        {
            this.type = type;
            this.val1 = val1;
        }
        public MissionItem(ItemType type)
        {
            this.type = type;
        }
    }
    
    public MissionItem[] missionTakeOff = new MissionItem[] {
        new MissionItem(ItemType.DoAction, 0), //0 - Power on
        new MissionItem(ItemType.DoAction, 2, 2), // 1 - Engine start
        new MissionItem(ItemType.ReachCheckPoint,5), // 2 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 3 - wait
        new MissionItem(ItemType.ReachCheckPoint), // 4 - on ground
        new MissionItem(ItemType.DoAction, 3), // 6 - engine off
        new MissionItem(ItemType.DoAction, 1), // 7 - power off
    };
    public MissionItem[] missionFirstFly = new MissionItem[] {
        new MissionItem(ItemType.DoAction, 0), //0 - Power on
        new MissionItem(ItemType.DoAction, 2, 2), // 1 - Engine start
        
        new MissionItem(ItemType.ReachCheckPoint), // 2 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 3 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 4 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 5 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 6 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 7 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 8 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 9 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 10 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 11 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 12 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 13 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 14 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 15 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 16 - above ground
        
        new MissionItem(ItemType.DoAction, 3), // 6 - engine off
        new MissionItem(ItemType.DoAction, 1), // 7 - power off

    };
    public MissionItem[] missionComplex = new MissionItem[] {
        new MissionItem(ItemType.DoAction, 0), //0 - Power on
        new MissionItem(ItemType.DoAction, 2, 2), // 1 - Engine start
        
        new MissionItem(ItemType.ReachCheckPoint), // 2 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 3 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 4 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 5 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 6 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 7 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 8 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 9 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 10 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 11 - above ground
        new MissionItem(ItemType.ReachCheckPoint), // 12 - above ground
        
        new MissionItem(ItemType.DoAction, 3), // 6 - engine off
        new MissionItem(ItemType.DoAction, 1), // 7 - power off

    };
    public StaticClass.MissionsNames currentMissionSelected = StaticClass.MissionsNames.TakeOff_Land;
    public static MissionItem[] currentMission = null;

    public Text status;
    public Button replayMission;
    // Start is called before the first frame update
    void Awake()
    {
        
        switch (currentMissionSelected)
        {
            case StaticClass.MissionsNames.TakeOff_Land:
                currentMission = missionTakeOff;
            break;
            case StaticClass.MissionsNames.HomePoint_Choose:
                currentMission = missionTakeOff;
            break;
            case StaticClass.MissionsNames.FirstFly:
                currentMission = missionFirstFly;
            break;
            case StaticClass.MissionsNames.Complex:
                currentMission = missionComplex;
            break;
            default:
                currentMission = missionTakeOff;
            break;
        }
        if (StaticClass.missionDataToReplay.Length > 0)
        {
            replayMission.gameObject.SetActive(true);
            Debug.Log(StaticClass.missionDataToReplay);
        }
    }
    
    string getTaskDesc(MissionItem item, int index)
    {
        string desc = "";
        switch (item.type)
        {
            case ItemType.ReachCheckPoint:
                desc = "Достичь цели №"+index;
            break;
            case ItemType.DoAction:
                desc = string.Format("Действие:");
                switch(item.val1)
                {
                    case 0:
                        desc += "Вкл. питание дрона";
                    break;
                    case 1:
                        desc += "Выкл. питание дрона";
                    break;
                    case 2:
                        desc += "Вкл. двигатели";
                    break;
                    case 3:
                        desc += "Выкл. двигатели";
                    break;
                }
                
            break;
        }
        if (item.wait > 0)
        {
            desc += "[ Пауза " + item.wait +" с]";
        }
        desc += "("+ (item.isDone ? "+" : "-") +")";
        return desc;
    }
    // Update is called once per frame
    void Update()
    {
        string descAll = "";
        if (currentMission != null)
        {
            int index = 0;
            foreach (MissionItem item in currentMission)
            {
                if(!item.isDone)
                    descAll += getTaskDesc(item, index) + "\n";
                index++;
            }
        }
        else
        {
            descAll = "Не задана";
        }
        status.text = "Выполнить: \n"+descAll;
    }
    public static void reportAction(ItemType type, int value = -1) {
        MissionItem lastItem = null;
        foreach(MissionItem item in currentMission)
        {
            if (!item.isDone)
            {
                if (item.type == type && (value == -1 || item.val1 == value))
                {
                    
                    if (lastItem != null && lastItem.wait > 0 )
                    {
                        if ((Time.realtimeSinceStartup - lastItem.doneAt) > lastItem.wait)
                        {
                            item.isDone = true;
                            item.doneAt = Time.realtimeSinceStartup;
                            showNextCheckpoint();

                        }
                    } 
                    else
                    {
                        item.isDone = true;
                        item.doneAt = Time.realtimeSinceStartup;
                        showNextCheckpoint();

                    }
                }
                break;
            }
            lastItem = item;
        }
    }
    public static bool reportCheckpointEnter(int itemId) {
        MissionItem item = currentMission[itemId];
        if (item == null)
        {
            return false;
        }
        MissionItem lastItem = null;
        if (itemId >= 1)
        {
            lastItem = currentMission[itemId-1];
            if (lastItem.wait > 0)
            {
                if ((Time.realtimeSinceStartup - lastItem.doneAt) > lastItem.wait)
                {
                    item.isDone = true;
                    item.doneAt = Time.realtimeSinceStartup;
                    showNextCheckpoint();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                item.isDone = true;
                item.doneAt = Time.realtimeSinceStartup;
                showNextCheckpoint();
                return true;
            }
        }
        return true;
        
    }
    public static void assignCheckpoint(int itemId, GameObject obj) {
        MissionItem item = currentMission[itemId];
        if (item != null) {
            item.checkpoint = obj;
        }

    }
    public static void showNextCheckpoint() {
        checkMissionComplete();
        foreach (MissionItem item in currentMission)
        {
            if (item.checkpoint != null && !item.isDone) {
                item.checkpoint.SetActive(!item.isDone);
                break;
            }
        }
    }
    private static void checkMissionComplete()
    {
        bool complete = true;
        foreach (MissionItem item in currentMission)
        {
            if (!item.isDone)
            {
                complete = false;
                break;
            }
        }
        if (complete)
        {
            DroneStateLogger.sendRecordedData();
        }
    }
    public void finishMission()
    {
        SceneManager.LoadScene("Login");
    }
    public void missionReset()
    {
        //Awake();
        //showNextCheckpoint();
        StaticClass.droneState = new StaticClass.DroneState();
        StaticClass.droneStates = new List<StaticClass.DroneState>();
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
