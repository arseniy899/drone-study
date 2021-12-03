using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DroneControlC : MonoBehaviour {
	public Rigidbody Drone;
	public GameObject RButton;
	public GameObject LButton;
	public GameObject gameOverBanner;
	public GameObject explosionPrefab;


	/*Speed*/
	public int ForwardBackward = 50; 
	   /*Speed*/public int Tilt = 50; 
	   /*Speed*/public int FlyLeftRight = 50;  
	   /*Speed*/public int UpDown = 50; 
	   
	private Vector3 DroneRotation;
	public bool Mobile;
	private float Rx;
	private float Ry;
	private float Lx;
	private float Ly;
	public List<GameObject> propellers;
	public float lastY = 0f;
	public void switchPropellers()
    {
		foreach (GameObject propeller in propellers)
		{
			propeller.GetComponent<Animator>().speed = StaticClass.droneState.enginesOn ? 1 : 0;
		}
		MissionManager.reportAction(MissionManager.ItemType.DoAction, StaticClass.droneState.enginesOn ? 2 : 3);

	}
	void Start()
    {
		foreach (GameObject propeller in propellers)
		{
			propeller.GetComponent<Animator>().speed = 0;
		}
	}
	void Update () {
		if (Mobile)
		{
			Rx = RButton.GetComponent<DroneCanvasC>().Rx;
			Ry = RButton.GetComponent<DroneCanvasC>().Ry;
			Lx = LButton.GetComponent<DroneCanvasC>().Lx;
			Ly = LButton.GetComponent<DroneCanvasC>().Ly;
		}
	}

	void FixedUpdate () {
		DroneRotation=Drone.transform.localEulerAngles;
		if(DroneRotation.z>10 && DroneRotation.z<=180){Drone.AddRelativeTorque (0, 0, -10);}//if tilt too big(stabilizes drone on z-axis)
		if(DroneRotation.z>180 && DroneRotation.z<=350){Drone.AddRelativeTorque (0, 0, 10);}//if tilt too big(stabilizes drone on z-axis)
		if(DroneRotation.z>1 && DroneRotation.z<=10){Drone.AddRelativeTorque (0, 0, -3);}//if tilt not very big(stabilizes drone on z-axis)
		if(DroneRotation.z>350 && DroneRotation.z<359){Drone.AddRelativeTorque (0, 0, 3);}//if tilt not very big(stabilizes drone on z-axis)

		
		if(Mobile==false && StaticClass.droneState.droneOn)
		{
			if(Input.GetKey(KeyCode.A)) {Drone.AddRelativeTorque(0,Tilt/-1,0);}//tilt drone left
			if(Input.GetKey(KeyCode.D)){Drone.AddRelativeTorque(0,Tilt,0);}//tilt drone right
		}

		if(Mobile==true){
			Drone.AddRelativeTorque(0,Lx/5,0);//tilt drone left and right

		}

		if(DroneRotation.x>10 && DroneRotation.x<=180){Drone.AddRelativeTorque (-10, 0, 0);}//if tilt too big(stabilizes drone on x-axis)
		if(DroneRotation.x>180 && DroneRotation.x<=350){Drone.AddRelativeTorque (10, 0, 0);}//if tilt too big(stabilizes drone on x-axis)
		if(DroneRotation.x>1 && DroneRotation.x<=10){Drone.AddRelativeTorque (-3, 0, 0);}//if tilt not very big(stabilizes drone on x-axis)
		if(DroneRotation.x>350 && DroneRotation.x<359){Drone.AddRelativeTorque (3, 0, 0);}//if tilt not very big(stabilizes drone on x-axis)

		if (StaticClass.droneState.enginesOn && StaticClass.droneState.droneOn)
			Drone.AddForce(0, 9.80665f, 0);//drone not lose height very fast, if you want not to lose height al all change 9 into 9.80665 
		if(Mobile== false && StaticClass.droneState.droneOn)
		{
			if(Input.GetKeyDown("space")){
				StaticClass.droneState.enginesOn = !StaticClass.droneState.enginesOn;
				switchPropellers();
				if (StaticClass.droneState.enginesOn)
				{
					StaticClass.droneState.homeLat = Drone.transform.position.x;
					StaticClass.droneState.homeLon = Drone.transform.position.z;
					StaticClass.droneState.homeAlt = Drone.transform.position.y;
				}

			}//drone fly forward
			if (StaticClass.droneState.enginesOn)
			{
				if (Input.GetKey(KeyCode.W)) { 
					Drone.AddRelativeTorque(10, 0, 0); 
					Drone.AddRelativeForce(0, ForwardBackward / 3.846153846153846f, ForwardBackward); 
				}//drone fly forward

				if (Input.GetKey(KeyCode.LeftArrow)) { Drone.AddRelativeForce(FlyLeftRight / -1, FlyLeftRight / 3.846153846153846f, 0); Drone.AddRelativeTorque(0, 0, 10); }//rotate drone left

				if (Input.GetKey(KeyCode.RightArrow)) { Drone.AddRelativeForce(FlyLeftRight, FlyLeftRight / 3.846153846153846f, 0); Drone.AddRelativeTorque(0, 0, -10); }//rotate drone right

				if (Input.GetKey(KeyCode.S)) { Drone.AddRelativeTorque(-10, 0, 0); Drone.AddRelativeForce(0, ForwardBackward / 3.846153846153846f, -ForwardBackward); }// drone fly backward

				if (Input.GetKey(KeyCode.UpArrow)) { Drone.AddRelativeForce(0, UpDown, 0); }//drone fly up

				if (Input.GetKey(KeyCode.DownArrow)) { 
					Drone.AddRelativeForce(0, UpDown / -1, 0);
					if (lastY == Drone.transform.position.y)
					{
						StaticClass.droneState.enginesOn = false;
						switchPropellers();
					}
				}//drone fly down
			}
		}
		if (Drone.transform.hasChanged)
		{
			float distHor = Vector3.Distance(Drone.transform.position, new Vector3(StaticClass.droneState.lat, Drone.transform.position.y, StaticClass.droneState.lon));
			float distVer = Vector3.Distance(Drone.transform.position, new Vector3(Drone.transform.position.x, StaticClass.droneState.alt, Drone.transform.position.z));
			float timeDist = (float) (Time.realtimeSinceStartup - StaticClass.droneState.time);
			StaticClass.droneState.speedV = distVer / timeDist;
			StaticClass.droneState.speedH = distHor / timeDist;
			StaticClass.droneState.lat = Drone.transform.position.x;
			StaticClass.droneState.lon = Drone.transform.position.z;
			StaticClass.droneState.alt = Drone.transform.position.y;
			StaticClass.droneState.row = Drone.transform.localEulerAngles.x;
			StaticClass.droneState.pitch = Drone.transform.localEulerAngles.y;
			StaticClass.droneState.yaw = Drone.transform.localEulerAngles.z;
			StaticClass.droneState.time = Time.realtimeSinceStartup;
		}
		if (Mobile==true)
		{
			Drone.AddRelativeForce(0,0,Ly/2);if(Ly>5){Drone.AddRelativeTorque (10, 0, 0);};if(Ly<-5){Drone.AddRelativeTorque (-10, 0, 0);}//drone fly forward or backward

			Drone.AddRelativeForce(Rx,0,0);if(Rx>5){Drone.AddRelativeTorque (0, 0,-10);};if(Rx<-5){Drone.AddRelativeTorque (0, 0,10);}


			Drone.AddRelativeForce(0,Ry/2,0);//drone fly up or down
		}
	}
	public void dronePower(bool isOn)
    {
		Debug.Log("Drone power is " + isOn);
        StaticClass.droneState.droneOn = !StaticClass.droneState.droneOn;
		MissionManager.reportAction(MissionManager.ItemType.DoAction, StaticClass.droneState.droneOn ? 0 : 1);
		if (!StaticClass.droneState.droneOn)
        {
			StaticClass.droneState.enginesOn = false;
			switchPropellers();

		}
	}

	public void droneCrashed()
    {
		DroneStateLogger.sendRecordedData();
		Debug.Log("Drone smashed");
		Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		List <GameObject> destroyable = new List<GameObject>(GameObject.FindGameObjectsWithTag("Destroyable")).FindAll(g => g.transform.IsChildOf(this.transform));
		foreach (GameObject obj in destroyable)
		{
			Destroy(obj);
		}
		StartCoroutine(CoroutineForCrashAnim());

	}

	IEnumerator CoroutineForCrashAnim()
	{
		yield return new WaitForSeconds(2f);
		gameOverBanner.SetActive(true);
		
	}

}