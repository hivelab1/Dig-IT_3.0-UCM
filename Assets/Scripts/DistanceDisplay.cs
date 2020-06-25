using UnityEngine;
using System.Collections;

public class DistanceDisplay : MonoBehaviour {

	private GameObject point1;
	private GameObject point2;

	private HUDControl hudControl;
	public bool distanceOn;
	private GameObject measureLight;
	private GameObject distanceBackground;
	//private Server server;
	public bool utilizeHUD;

	// Use this for initialization
	void Awake () {
		point1 = GameObject.Find ("Point1");
		point2 = GameObject.Find ("Point2");
		distanceBackground = GameObject.Find ("DistanceHUDPlane");

		GameObject servObj=GameObject.Find ("AppRoot");
		//if(servObj!=null) //more gracefully handle server being turned off - DJZ
		//	server = servObj.GetComponent<Server> ();
		//else
		//	server=null;
		
		distanceOn = false;
		if(utilizeHUD)
			hudControl = GameObject.Find ("NormalHUD").GetComponent<HUDControl>();

	}

	float getDistance() {
		return (point1.transform.position - point2.transform.position).sqrMagnitude;
	}

	public void setOn(bool flag){
		distanceOn = flag;
	}

	void displayDistance(bool on){
		if(distanceOn){
			distanceBackground.GetComponent<Renderer>().enabled = true;
			if(utilizeHUD) hudControl.setDistanceDisplay("Distance: " + getDistance ().ToString() + " m");
			//if(server!=null) server.setDistance (getDistance ().ToString () + " m");
		} else {
			distanceBackground.GetComponent<Renderer>().enabled = false;
			if(utilizeHUD) hudControl.setDistanceDisplay(" ");
			//if(server!=null) server.setDistance ("Not measuring");
		}

	}
	// Update is called once per frame
	void Update () {
		displayDistance (distanceOn);
	}
}
